namespace OcadParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TdPolyPoint
    {
        public static implicit operator TdPolyPoint(int x)
        {
            return new TdPolyPoint(x);
        }


        public int Coordinate { get; private set; }

        public TdPolyPoint(byte[] value)
        {
            var num = (value[0] << 8) + (value[0 + 1] << 16) + (value[0 + 2] << 24);
            Coordinate = num >> 8;
        }

        public TdPolyPoint(int coordinate)
        {
            Coordinate = coordinate;
        }
    }
}