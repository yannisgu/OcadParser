namespace OcadParser
{
    using System.Linq;

    public class TdPoly
    {
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

        public TdPolyPoint X { get; private set; }
        public TdPolyPoint Y { get; private set; }

        public TdPoly MoveBy(TdPoly point)
        {
            return new TdPoly(X.Coordinate + point.X.Coordinate, Y.Coordinate+  point.Y.Coordinate)
            {
                IsAreaBorderOrVirtualGapLine = IsAreaBorderOrVirtualGapLine,
                IsCornerPoint = IsCornerPoint,
                IsFirstBezierCurvePoint = IsFirstBezierCurvePoint,
                IsFirstPointInAreaHole = IsFirstPointInAreaHole,
                IsLeftLineHiddenUntillNextPoint = IsLeftLineHiddenUntillNextPoint,
                IsPointDashLine = IsPointDashLine,
                IsRightLineHiddenUntilNextPoint = IsRightLineHiddenUntilNextPoint,
                IsSecondBezierCurvePoint = IsSecondBezierCurvePoint
            };
        }
    }
}