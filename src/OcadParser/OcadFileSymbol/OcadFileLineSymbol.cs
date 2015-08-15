using System.Collections.Generic;
using System.Linq;

namespace OcadParser
{
    using System;

    public class OcadFileLineSymbol : OcadFileBaseSymbol, IBinaryParsable<OcadFileLineSymbol>
    {
        public Int16 LineColor { get; set; } // Line color

        public Int16 LineWidth { get; set; } // Line width

        public Int16 LineStyle { get; set; } // Line style
        //   0: bevel joins/flat caps
        //   1: round joins/round caps
        //   4: miter joins/flat caps
        public Int16 DistFromStart { get; set; } // Distance from start

        public Int16 DistToEnd { get; set; } // Distance to the end

        public Int16 MainLength { get; set; } // Main length a

        public Int16 EndLength { get; set; } // End length b

        public Int16 MainGap { get; set; } // Main gap C

        public Int16 SecGap { get; set; } // Gap D

        public Int16 EndGap { get; set; } // Gap E

        public Int16 MinSym { get; set; } // -1: at least 0 gaps/symbols
        //  0: at least 1 gap/symbol
        //  1: at least 2 gaps/symbols
        //  etc.
        public Int16 nPrimSym { get; set; } // No. of symbols

        public Int16 PrimSymDist { get; set; } // Distance

        public uint DblMode { get; set; } // Mode (Double line page)

        public uint DblFlags { get; set; } // Double line flags
        //    1: Fill color on
        //    2: Background color on
        public Int16 DblFillColor { get; set; } // Fill color

        public Int16 DblLeftColor { get; set; } // Left line/Color

        public Int16 DblRightColor { get; set; } // Right line/Color

        public Int16 DblWidth { get; set; } // Width

        public Int16 DblLeftWidth { get; set; } // Left line/Line width

        public Int16 DblRightWidth { get; set; } // Right line/Line width

        public Int16 DblLength { get; set; } // Dashed/Distance a

        public Int16 DblGap { get; set; } // Dashed/Gap

        public Int16 DblBackgroundColor { get; set; } // Reserved

        public Int16 DblRes1 { get; set; } // Reserved

        public Int16 DblRes2 { get; set; } // Reserved

        public uint DecMode { get; set; } // Decrease mode
        //   0: off
        //   1: decreasing towards the end
        //   2: decreasing towards both ends
        public Int16 DecLast { get; set; } // Last symbol

        public Int16 DecRes { get; set; } // Reserved

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

        public Byte Reserved { get; set; }
        // 1 = end symbol, 2 = start symbol, 4 = corner symbol, 8 = secondary symbol   

        public List<OcadFileSymbolElement> MainSymbols { get; set; } = new List<OcadFileSymbolElement>();
        public List<OcadFileSymbolElement> SecondarySymbols { get; set; } = new List<OcadFileSymbolElement>();
        public List<OcadFileSymbolElement> CornerSymbols { get; set; } = new List<OcadFileSymbolElement>();
        public List<OcadFileSymbolElement> StartSymbols { get; set; } = new List<OcadFileSymbolElement>();
        public List<OcadFileSymbolElement> EndSymbols { get; set; } = new List<OcadFileSymbolElement>();

        public void SetupBinaryParser(BinaryParser<OcadFileLineSymbol> parser)
        {
            parser.SetPropertyOrder(
                _ => _.Size,
                _ => _.SymNum,
                _ => _.Otp,
                _ => _.Flags,
                _ => _.Selected,
                _ => _.Status,
                _ => _.PreferredDrawingTool,
                _ => _.CsMode,
                _ => _.CsObjType,
                _ => _.CsCdFlags,
                _ => _.Extent,
                _ => _.FilePos,
                _ => _.Group,
                _ => _.nColors,
                _ => _.Colors,
                _ => _.Description,
                _ => _.IconBits,
                _ => _.SymbolTreeGroup,
                _ => _.LineColor,
                _ => _.LineWidth,
                _ => _.LineStyle,
                _ => _.DistFromStart,
                _ => _.DistToEnd,
                _ => _.MainLength,
                _ => _.EndLength,
                _ => _.MainGap,
                _ => _.SecGap,
                _ => _.EndGap,
                _ => _.MinSym,
                _ => _.nPrimSym,
                _ => _.PrimSymDist,
                _ => _.DblMode,
                _ => _.DblFlags,
                _ => _.DblFillColor,
                _ => _.DblLeftColor,
                _ => _.DblRightColor,
                _ => _.DblWidth,
                _ => _.DblLeftWidth,
                _ => _.DblRightWidth,
                _ => _.DblLength,
                _ => _.DblGap,
                _ => _.DblBackgroundColor,
                _ => _.DblRes1, _ => _.DblRes2,
                _ => _.DecMode,
                _ => _.DecLast,
                _ => _.DecRes,
                _ => _.FrColor,
                _ => _.FrWidth,
                _ => _.FrStyle,
                _ => _.PrimDSize,
                _ => _.SecDSize,
                _ => _.CornerDSize,
                _ => _.StartDSize,
                _ => _.EndDSize,
                _ => _.UseSymbolFlags,
                _ => _.Reserved);


            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);

            parser.SetDynamicList(_ => _.MainSymbols, _ => _.MainSymbols.Sum(e => e.NumberPoly + 2) < _.PrimDSize);
            parser.SetDynamicList(_ => _.SecondarySymbols,
                _ => _.SecondarySymbols.Sum(e => e.NumberPoly + 2) < _.SecDSize);
            parser.SetDynamicList(_ => _.CornerSymbols, _ => _.CornerSymbols.Sum(e => e.NumberPoly + 2) < _.CornerDSize);
            parser.SetDynamicList(_ => _.StartSymbols, _ => _.StartSymbols.Sum(e => e.NumberPoly + 2) < _.StartDSize);
            parser.SetDynamicList(_ => _.EndSymbols, _ => _.EndSymbols.Sum(e => e.NumberPoly + 2) < _.EndDSize);
        }
    }
}