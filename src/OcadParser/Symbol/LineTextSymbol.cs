namespace OcadParser
{
    using System;

    public class LineTextSymbol : BaseSymbol, IBinaryParsable<LineTextSymbol>
    {
        public char[] FontName { get; set; } // TrueType font

        public Int16 FontColor { get; set; } // Color

        public Int16 FontSize { get; set; } // 10 times the value entered in Size

        public Int16 Weight { get; set; } // Bold as used in the Windows GDI
        //   400: normal
        //   700: bold
        public bool Italic { get; set; } // true if Italic is checked

        public byte Res1 { get; set; } // not used

        public Int16 CharSpace { get; set; } // Char. spacing

        public Int16 WordSpace { get; set; } // Word spacing

        public Int16 Alignment { get; set; } // Alignment     --> constant.pas
        //   0: Bottom Left
        //   1: Bottom Center
        //   2: Bottom Right
        //   3: Bottom All line
        //   4: Middle Left
        //   5: Middle Center
        //   6: Middle Right
        //   7: Middle All line
        //   8: Top Left
        //   9: Top Center
        //  10: Top Right
        //  11: Top All line 
        public byte FrMode { get; set; } // Framing mode
        //   0: no framing
        //   1: shadow framing
        //   2: line framing
        public byte FrLineStyle { get; set; } // Framing line style
        //   0: default OCAD 8 and 9.0 Miter
        //   2: ps_Join_Bevel
        //   1: ps_Join_Round
        //   4: ps_Join_Miter
        public char[] Res2 { get; set; } // not used

        public Int16 FrColor { get; set; } // Framing color

        public Int16 FrWidth { get; set; } // Framing width for line framing

        public Int16 Res3 { get; set; } // not used

        public uint Res4 { get; set; } // not used

        public Int16 FrOfX { get; set; } // Horizontal offset for shadow framing

        public Int16 FrOfY { get; set; } // Vertical offset for shadow framing

        public void SetupBinaryParser(BinaryParser<LineTextSymbol> parser)
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
                _ => _.FontName,
                _ => _.FontColor,
                _ => _.FontSize,
                _ => _.Weight,
                _ => _.Italic,
                _ => _.Res1,
                _ => _.CharSpace,
                _ => _.WordSpace,
                _ => _.Alignment,
                _ => _.FrMode,
                _ => _.FrLineStyle,
                _ => _.Res2,
                _ => _.FrColor,
                _ => _.FrWidth,
                _ => _.Res3,
                _ => _.Res4,
                _ => _.FrOfX,
                _ => _.FrOfY);
            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
            parser.SetArrayLength(_ => _.FontColor, 14);
            parser.SetArrayLength(_ => _.Res2, 14);
        }
    }
}