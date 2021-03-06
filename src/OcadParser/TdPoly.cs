namespace OcadParser
{
    using System.Linq;

    public class TdPoly
    {
        private TdPoly tdPoly;

            public bool IsFirstBezierCurvePoint { get; set; }
            public bool IsSecondBezierCurvePoint { get; set; }
            public bool IsLeftLineHiddenUntillNextPoint { get; set; }
            public bool IsAreaBorderOrVirtualGapLine { get; set; }

            public bool IsCornerPoint { get; set; }
            public bool IsFirstPointInAreaHole { get; set; }
            public bool IsRightLineHiddenUntilNextPoint { get; set; }
            public bool IsPointDashLine { get; set; }

        public TdPoly(byte[] bytes)
        {
            var metaXByte = bytes.First();
            this.IsFirstBezierCurvePoint = (metaXByte & 1) == 1;
            this.IsSecondBezierCurvePoint = (metaXByte & 2) == 2;
            this.IsLeftLineHiddenUntillNextPoint = (metaXByte & 4) == 4;
            this.IsAreaBorderOrVirtualGapLine = (metaXByte & 8) == 8;

            var metaYByte = bytes.Skip(4).First();
            this.IsCornerPoint = (metaYByte & 1) == 1;
            this.IsFirstPointInAreaHole = (metaYByte & 2) == 2;
            this.IsRightLineHiddenUntilNextPoint = (metaYByte & 4) == 4;
            this.IsPointDashLine = (metaYByte & 8) == 8;

            this.X = new TdPolyPoint(bytes.Skip(1).Take(3).ToArray());
            this.Y = new TdPolyPoint(bytes.Skip(5).Take(3).ToArray());
        }

        public TdPoly(TdPolyPoint x, TdPolyPoint y)
        {
            X = x;
            Y = y;
        }

        public TdPoly(TdPolyPoint x, TdPolyPoint y, TdPoly basePoly) : this(x, y)
        {
            IsAreaBorderOrVirtualGapLine = basePoly.IsAreaBorderOrVirtualGapLine;
            IsCornerPoint = basePoly.IsCornerPoint;
            IsFirstBezierCurvePoint = basePoly.IsFirstBezierCurvePoint;
            IsFirstPointInAreaHole = basePoly.IsFirstPointInAreaHole;
            IsLeftLineHiddenUntillNextPoint = basePoly.IsLeftLineHiddenUntillNextPoint;
            IsPointDashLine = basePoly.IsPointDashLine;
            IsRightLineHiddenUntilNextPoint = basePoly.IsRightLineHiddenUntilNextPoint;
            IsSecondBezierCurvePoint = basePoly.IsSecondBezierCurvePoint;
        }

        public TdPolyPoint X { get; private set; }
        public TdPolyPoint Y { get; private set; }

        public TdPoly MoveBy(TdPoly point)
        {
            return new TdPoly(X.Coordinate + point.X.Coordinate, Y.Coordinate + point.Y.Coordinate, this);
        }

        public override bool Equals(object polyObject)
        {
            var poly = polyObject as TdPoly;
            if (poly == null)
            {
                return false;
            }

            return X.Equals(poly.X) && Y.Equals(poly.Y) && IsFirstBezierCurvePoint == poly.IsFirstBezierCurvePoint &&
                   IsSecondBezierCurvePoint == poly.IsSecondBezierCurvePoint &&
                   IsLeftLineHiddenUntillNextPoint == poly.IsLeftLineHiddenUntillNextPoint &&
                   IsAreaBorderOrVirtualGapLine == poly.IsAreaBorderOrVirtualGapLine &&
                   IsCornerPoint == poly.IsCornerPoint &&
                   IsFirstPointInAreaHole == poly.IsFirstPointInAreaHole &&
                   IsRightLineHiddenUntilNextPoint == poly.IsRightLineHiddenUntilNextPoint &&
                   IsPointDashLine == poly.IsPointDashLine;
        }
    }
}