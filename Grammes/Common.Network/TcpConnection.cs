﻿namespace Common.Network
{
  using System;
  using System.Collections.Concurrent;
  using System.Net;
  using System.Net.Sockets;
  using System.Threading;

  using Packets;

  public class TcpConnection : IClosable
  {
    #region Constants

    private const int BUFFER_SIZE = ushort.MaxValue * 3;
    private const int SIZE_LENGTH = 2;

    #endregion

    #region Fields

    private readonly SocketAsyncEventArgs _receiveEvent;
    private readonly SocketAsyncEventArgs _sendEvent;

    private readonly ConcurrentQueue<byte[]> _sendQueue;

    private readonly IPEndPoint _remoteEndpoint;
    private readonly Socket _socket;
    private readonly TcpServer _server;

    private int _disposed;
    private int _sending;

    #endregion

    #region Properties

    public string Login { get; set; }

    #endregion

    #region Constructors

    public TcpConnection(IPEndPoint remoteEndpoint, Socket socket, TcpServer server)
    {
      _remoteEndpoint = remoteEndpoint;
      _socket = socket;
      _server = server;

      _receiveEvent = new SocketAsyncEventArgs();
      _receiveEvent.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
      _receiveEvent.Completed += ReceiveCompleted;

      _sendEvent = new SocketAsyncEventArgs();
      _sendEvent.SetBuffer(new byte[BUFFER_SIZE], 0, BUFFER_SIZE);
      _sendEvent.Completed += SendCompleted;

      _sendQueue = new ConcurrentQueue<byte[]>();

      _disposed = 0;
      _sending = 0;
    }

    #endregion

    #region Methods

    public void Open()
    {
      Receive();
    }

    public void Close()
    {
      if (Interlocked.CompareExchange(ref _disposed, 1, 0) == 1)
      {
        return;
      }

      _server.FreeConnection(_remoteEndpoint);

      Safe(() => _socket.Dispose());
      Safe(() => _sendEvent.Dispose());
      Safe(() => _receiveEvent.Dispose());
    }

    public void Send(byte[] packet)
    {
      _sendQueue.Enqueue(packet);
      if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
      {
        SendImpl();
      }
    }

    private void SendImpl()
    {
      if (_disposed == 1)
      {
        return;
      }

      if (!_sendQueue.TryDequeue(out byte[] packet) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
      {
        return;
      }

      // Добавить размер в начало пакета.
      Array.Copy(packet, 0, _sendEvent.Buffer, SIZE_LENGTH, packet.Length);
      BufferPrimitives.SetUint16(_sendEvent.Buffer, 0, (ushort)packet.Length);

      _sendEvent.SetBuffer(0, packet.Length + SIZE_LENGTH);

      if (!_socket.SendAsync(_sendEvent))
      {
        SendCompleted(_socket, _sendEvent);
      }
    }

    private void SendCompleted(object sender, SocketAsyncEventArgs eventArgs)
    {
      if (eventArgs.BytesTransferred != eventArgs.Count || eventArgs.SocketError != SocketError.Success)
      {
        Close();
        return;
      }

      SendImpl();
    }

    private void Receive()
    {
      if (_disposed == 1)
      {
        return;
      }

      if (!_socket.ReceiveAsync(_receiveEvent))
      {
        ReceiveCompleted(_socket, _receiveEvent);
      }
    }

    private void ReceiveCompleted(object sender, SocketAsyncEventArgs eventArgs)
    {
      if (eventArgs.BytesTransferred == 0 || eventArgs.SocketError != SocketError.Success)
      {
        Close();
        return;
      }

      int available = eventArgs.Offset + eventArgs.BytesTransferred;
      for (;;)
      {
        if (available < SIZE_LENGTH)
        {
          // WE NEED MORE DATA
          break;
        }

        int offset = 0;
        ushort length = BufferPrimitives.GetUint16(eventArgs.Buffer, ref offset);
        if (length + SIZE_LENGTH > available)
        {
          // WE NEED MORE DATA
          break;
        }

        _server.HandlePacket(_remoteEndpoint, BufferPrimitives.GetBytes(eventArgs.Buffer, ref offset, length));

        available = available - length - SIZE_LENGTH;
        if (available > 0)
        {
          Array.Copy(eventArgs.Buffer, length + SIZE_LENGTH, eventArgs.Buffer, 0, available);
        }
      }

      eventArgs.SetBuffer(available, BUFFER_SIZE - available);
      Receive();
    }

    private void Safe(Action callback)
    {
      try
      {
        callback();
      }
      catch
      {
      }
    }

    #endregion
  }
}
