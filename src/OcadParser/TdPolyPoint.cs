namespace OcadParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TdPolyPoint
    {

        public int Coordinate { get; set; }

        public TdPolyPoint(IEnumerable<byte> value)
        {
            var numberBytes = new List<byte>() { new byte() };
            numberBytes.AddRange(value.Take(3));
            this.Coordinate = BitConverter.ToInt32(numberBytes.ToArray(), 0);
            

        }
    }
}