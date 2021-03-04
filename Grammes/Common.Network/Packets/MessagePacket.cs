namespace Common.Network.Packets
{
  using System.Text;

  using Messages;

  using Newtonsoft.Json;

  public class MessagePacket
  {
    #region Properties

    public byte Id => 0x04;

    public string Message { get; }

    #endregion

    #region Constructors

    public MessagePacket(byte[] packet)
    {
      int offset = 1;
      Message = Encoding.UTF8.GetString(packet, offset, packet.Length - offset);
    }

    public MessagePacket(string message)
    {
      Message = message;
    }

    #endregion

    #region Methods

    public byte[] GetBytes()
    {
      if (string.IsNullOrEmpty(Message))
      {
        return new[] { Id };
      }

      byte[] message = Encoding.UTF8.GetBytes(Message);
      byte[] packet = new byte[message.Length + 1];

      int offset = 0;
      BufferPrimitives.SetUint8(packet, ref offset, Id);
      BufferPrimitives.SetBytes(packet, ref offset, message, 0, message.Length);

      return packet;
    }

    #endregion
  }
}
