using System;
using System.Linq;

namespace OcadParser.Models
{
    public class LineSymbol : Symbol
    {
        protected OcadBaseProject file;
        protected OcadFileLineSymbol symbol;

        public LineSymbol(OcadBaseProject file, OcadFileLineSymbol symbol)
        {
            this.file = file;
            this.symbol = symbol;
        }

        public OcadColor LineColor
        {
            get
            {
                return file.Colors.FirstOrDefault(_ => _.Number == symbol.LineColor);
            }
            set { symbol.LineColor = value.Number; }
        } 

        public Int16 LineWidth { get { return symbol.LineWidth; } set { symbol.LineWidth = value; } } 

        public OcadLineStyle LineStyle { get { return (OcadLineStyle) symbol.LineStyle; } set { symbol.LineStyle = (short)value; } }
        public Int16 DistFromStart { get { return symbol.DistFromStart; } set { symbol.DistFromStart = value; } } // Distance from start

        public Int16 DistToEnd { get { return symbol.DistToEnd; } set { symbol.DistToEnd = value; } } // Distance to the end

        public Int16 MainLength { get { return symbol.MainLength; } set { symbol.MainLength = value; } } // Main length a

        public Int16 EndLength { get { return symbol.EndLength; } set { symbol.EndLength = value; } } // End length b

        public Int16 MainGap { get { return symbol.MainGap; } set { symbol.MainGap = value; } } // Main gap C

        public Int16 SecGap { get { return symbol.SecGap; } set { symbol.SecGap = value; } } // Gap D

        public Int16 EndGap { get { return symbol.EndGap; } set { symbol.EndGap = value; } } // Gap E

        public Int16 MinSym { get { return symbol.MinSym; } set { symbol.MinSym = value; } } // -1: at least 0 gaps/symbols
        //  0: at least 1 gap/symbol
        //  1: at least 2 gaps/symbols
        //  etc.
        public Int16 NumberOfMainSymbols { get { return symbol.nPrimSym; } set { symbol.nPrimSym = value; } } // No. of symbols

        public Int16 MainSymbolDistanceDist { get { return symbol.PrimSymDist; } set { symbol.PrimSymDist = value; } } // Distance

        public bool DoubleLineOn { get { return symbol.DblMode == 0; } set
        {
                symbol.DblMode = (uint)(value ? 1 : 0);
        } } // Mode (Double line page)

        public LineSymbolDoubleLineFlag DoubleLineFlags { get { return (LineSymbolDoubleLineFlag) symbol.DblFlags; } set
        {
            symbol.DblFlags = (uint) value;
        } } // Double line flags
        //    1: Fill color on
        //    2: Background color on
        public OcadColor DoubleLineFillColor
        {
            get { return file.Colors.FirstOrDefault(_ => _.Number == symbol.DblFillColor); }
            set { symbol.DblFillColor = value.Number; }
        } // Fill color

        public OcadColor DoubleLineLeftColor
        {
            get { return file.Colors.FirstOrDefault(_ => _.Number == symbol.DblLeftColor); }
            set { symbol.DblLeftColor = value.Number; }
        }// Left line/Color

        public OcadColor DoubleLineRightColor
        {
            get { return file.Colors.FirstOrDefault(_ => _.Number == symbol.DblRightColor); }
            set { symbol.DblRightColor = value.Number; }
        } // Right line/Color

        public Int16 DoubleLineWidth { get { return symbol.DblWidth; } set { symbol.DblWidth = value; } } // Width

        public Int16 DoubleLineLeftWidth { get { return symbol.DblLeftWidth; } set { symbol.DblLeftWidth = value; } } // Left line/Line width

        public Int16 DoubleLineRightWidth { get { return symbol.DblRightWidth; } set { symbol.DblRightWidth = value; } } // Right line/Line width

        public Int16 DoubleLineDashDistance { get { return symbol.DblLength; } set { symbol.DblLength = value; } } // Dashed/Distance a

        public Int16 DoubleLineDashGap { get { return symbol.DblGap; } set { symbol.DblGap = value; } } // Dashed/Gap

        public uint DecreaseMode { get { return symbol.DecMode; } set { symbol.DecMode = value; } } // Decrease mode
        //   0: off
        //   1: decreasing towards the end
        //   2: decreasing towards both ends
        public Int16 DecLast { get; set; } // Last symbol
        

        public Int16 FrColor { get; set; } // Color of the framing line

        public Int16 FrWidth { get; set; } // Line width of the framing line

        public Int16 FrStyle { get; set; } // Line style of the framing line
        //   0: bevel joins/flat caps
        //   1: round joins/round caps
        //   4: miter joins/flat caps
        //  PointedEnd := LineStyle and 2 > 0;
        public uint PrimDSize { get; set; }

        // number or coordinates (8 bytes) for the Main symbol A which follow this structure.
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint SecDSize { get; set; }

        // number or coordinates (8 bytes) for the Secondary symbol which follow the Main symbol A
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint CornerDSize { get; set; }

        // number or coordinates (8 bytes) for the Corner symbol which follow the Secondary symbol
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint StartDSize { get; set; }

        // number or coordinates (8 bytes) for the Start symbol C which follow the Corner symbol
        // Each symbol header counts as 2 coordinates (16 bytes).
        public uint EndDSize { get; set; }

        // number or coordinates (8 bytes) for the End symbol D which follow the Start symbol C
        // Each symbol header counts as 2 coordinates (16 bytes).
        public Byte UseSymbolFlags { get; set; }
    }

    public enum LineSymbolDoubleLineFlag
    {
        Fill = 1,
        Background = 2,
    }

    public enum OcadLineStyle
    {
        Bevel = 0,
        Round = 1,
        Miter = 4
    }
}