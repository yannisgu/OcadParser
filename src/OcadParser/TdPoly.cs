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
            var metaXByte = bytes.Skip(3).First();
            this.IsFirstBezierCurvePoint = (metaXByte & 1) == 1;
            this.IsSecondBezierCurvePoint = (metaXByte & 2) == 2;
            this.IsLeftLineHiddenUntillNextPoint = (metaXByte & 4) == 4;
            this.IsAreaBorderOrVirtualGapLine = (metaXByte & 8) == 8;

            var metaYByte = bytes.Skip(7).First();
            this.IsCornerPoint = (metaYByte & 1) == 1;
            this.IsFirstPointInAreaHole = (metaYByte & 2) == 2;
            this.IsRightLineHiddenUntilNextPoint = (metaYByte & 4) == 4;
            this.IsPointDashLine = (metaYByte & 8) == 8;

            this.X = new TdPolyPoint(bytes.Take(3));
            this.Y = new TdPolyPoint(bytes.Skip(4).Take(3));
        }

        public TdPolyPoint X { get; set; }
        public TdPolyPoint Y { get; set; }
    }
}