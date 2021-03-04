namespace Common.Network.Packets
{
  public static class BufferPrimitives
  {
    #region Methods

    public static byte[] GetBytes(byte[] buffer, ref int offset, int count)
    {
      var temp = new byte[count];
      for (var i = 0; i < count; ++i) temp[i] = buffer[offset++];
      return temp;
    }

    public static ushort GetUint16(byte[] buffer, ref int offset)
    {
      return (ushort)GetVarious(buffer, ref offset, sizeof(ushort));
    }
    

    public static ulong GetVarious(byte[] buffer, ref int offset, int count)
    {
      ulong result = 0;
      for (var i = 0; i < count; ++i) result = (result << 8) | buffer[offset++];
      return result;
    }

    public static void SetBytes(byte[] buffer, ref int offset, byte[] data, int index, int count)
    {
      for (var i = 0; i < count; ++i) buffer[offset++] = data[index + i];
    }
    
    public static void SetUint16(byte[] buffer, int offset, ushort data)
    {
      SetVarious(buffer, ref offset, data, sizeof(ushort));
    }
    
    public static void SetUint8(byte[] buffer, ref int offset, byte data)
    {
      SetVarious(buffer, ref offset, data, sizeof(byte));
    }

    public static void SetVarious(byte[] buffer, ref int offset, long data, int count)
    {
      for (var i = 0; i < count; ++i) buffer[offset++] = (byte)(data >> (8 * (count - i - 1)));
    }

    #endregion
  }
}
