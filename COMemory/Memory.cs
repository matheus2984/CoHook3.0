using System;
using System.Text;

namespace COAPI
{
    public class Memory
    {
        private Conquer Process { get; set; }

        private IntPtr ProcessPtr
        {
            get { return Process.ProcessPtr; }
        }

        public Memory(Conquer process)
        {
            Process = process;
        }

        public void Write(int address, byte[] buffer)
        {
            var bytesWritten = 0;
            NativeMethods.WriteProcessMemory((int) ProcessPtr, address, buffer, buffer.Length, ref bytesWritten);
        }

        public void Write(int address, byte value)
        {
            byte[] buffer = {value};
            Write(address, buffer);
        }

        public byte[] Read(int address, int size)
        {
            var bytesRead = 0;
            var buffer = new byte[size];
            NativeMethods.ReadProcessMemory((int) ProcessPtr, address, buffer, size, ref bytesRead);
            return buffer;
        }

        public string ReadString(int address, int size)
        {
            return Encoding.ASCII.GetString(Read(address, size));
        }

        public byte ReadByte(int address)
        {
            return Read(address, sizeof (byte))[0];
        }

        public short ReadShort(int address)
        {
            return BitConverter.ToInt16(Read(address, sizeof (short)), 0);
        }

        public int ReadInt(int address)
        {
            return BitConverter.ToInt32(Read(address, sizeof (int)), 0);
        }

        public long ReadLong(int address)
        {
            return BitConverter.ToInt64(Read(address, sizeof (long)), 0);
        }

        public int ReadPointerChain(int baseAddress, params int[] offsets)
        {
            var current = ReadInt(baseAddress);
            for (int index = 0; index < offsets.Length -1; index++)
            {
                var offset = offsets[index];
                current = ReadInt(current + offset);
            }
            return current + offsets[offsets.Length-1];
        }
    }
}