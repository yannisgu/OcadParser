using System.Collections.Generic;

namespace OcadParser
{
    using System;
    using System.IO;
    using System.Linq;

    public class OcadStreamReader
    {


        public FileStream Stream { get; set; }

        public OcadStreamReader(FileStream stream)
        {
            this.Stream = stream;
        }

        public void Read()
        {
            
        }

        public virtual byte ReadByte()
        {
            byte[] buffer = new byte[1];
            this.Stream.Read(buffer,0,1);
            return buffer[0];
        }

        public virtual int ReadInt()
        {
            byte[] buffer = new byte[4];
            this.Stream.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public virtual Int16 ReadSmallInt()
        {
            byte[] buffer = new byte[2];
            this.Stream.Read(buffer, 0, 2);
            return BitConverter.ToInt16(buffer, 0);
        }
        public virtual ushort ReadWord()
        {
            byte[] buffer = new byte[2];
            this.Stream.Read(buffer, 0, 2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public virtual char ReadChar()
        {
            var value = ReadSmallInt();
            return (char) value;
        }

        public virtual bool ReadWordBool()
        {
            byte[] buffer = new byte[2];
            this.Stream.Read(buffer, 0, 2);
            return BitConverter.ToBoolean(buffer, 0);
        }

        public virtual string ReadString()
        {
            byte[] bufferNumber = new byte[1];
            this.Stream.Read(bufferNumber, 0, 1);
            int number = (int)bufferNumber[0];
            byte[] buffer = new byte[number];
            this.Stream.Read(buffer, 0, number);
            return string.Join("", buffer.Select(_ => (char)_));
        }

        public virtual long ReadLong()
        {
            byte[] buffer = new byte[8];
            this.Stream.Read(buffer, 0, 8);
            return BitConverter.ToInt64(buffer, 0);
        }

        public virtual double ReadDouble()
        {
            byte[] buffer = new byte[8];
            this.Stream.Read(buffer, 0, 8);
            return BitConverter.ToDouble(buffer, 0);
        }

        public virtual TdPoly ReadTdPoly()
        {
            byte[] buffer = new byte[8];
            this.Stream.Read(buffer, 0, 8);
            return new TdPoly(buffer);
        }

        public void ReadUntil(int position)
        {
            this.Stream.Position = position;
        }

        public IEnumerable<byte> ReadBytes(int len)
        {
            for (var i = 0; i < len; i++)
            {
                yield return ReadByte();
            }
        }
    }
}