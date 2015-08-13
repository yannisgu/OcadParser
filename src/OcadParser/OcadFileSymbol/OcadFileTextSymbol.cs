namespace OcadParser
{
    using System;

    public class OcadFileTextSymbol : OcadFileBaseSymbol, IBinaryParsable<OcadFileTextSymbol>
    {
        public char[] FontName { get; set; } // TrueType font

        public Int16 FontColor { get; set; } // Color

        public Int16 FontSize { get; set; } // 10 times the size in pt

        public Int16 Weight { get; set; } // Bold as used in the Windows GDI
        //   400: normal
        //   700: bold
        public bool Italic { get; set; } // true if Italic is checked

        public byte Res1 { get; set; } // not used

        public Int16 CharSpace { get; set; } // Char. spacing

        public Int16 WordSpace { get; set; } // Word spacing

        public Int16 Alignment { get; set; } // Alignment
        //   0: Bottom Left
        //   1: Bottom Center
        //   2: Bottom Right
        //   3: Bottom Justified
        //   4: Middle Left 
        //   5: Middle Center 
        //   6: Middle Right
        //   7: only in LText!
        //   8: Top Left
        //   9: Top Center
        //  10: Top Right
        //  11: only in LText!
        public Int16 LineSpace { get; set; } // Line spacing

        public Int16 ParaSpace { get; set; } // Space after Paragraph

        public Int16 IndentFirst { get; set; } // Indent first line

        public Int16 IndentOther { get; set; } // Indent other lines

        public Int16 nTabs { get; set; } // number of tabulators for text symbol

        public long[] Tabs { get; set; } // Tabulators

        public uint LBOn { get; set; } // true if Line below On is checked

        public Int16 LBColor { get; set; } // Line color (Line below)

        public Int16 LBWidth { get; set; } // Line width (Line below)

        public Int16 LBDist { get; set; } // Distance from text

        public Int16 Res2 { get; set; }

        public byte FrMode { get; set; } // Framing mode
        //   0: no framing
        //   1: shadow framing
        //   2: line framing
        //   3: rectangle framing
        public byte FrLineStyle { get; set; } // Framing line style
        //   0: default OCAD 8 Miter
        //   2: ps_Join_Bevel
        //   1: ps_Join_Round
        //   4: ps_Join_Miter
        public bool PointSymOn { get; set; } // Point symbol is activated

        public int PointSymNumber { get; set; } // Point symbol for text symbol activated if PointSymOn is true

        public char[] Res3 { get; set; } // not used

        public Int16 FrLeft { get; set; } // Left border for rectangle framing

        public Int16 FrBottom { get; set; } // Bottom border for rectangle framing

        public Int16 FrRight { get; set; } // Right border for rectangle framing

        public Int16 FrTop { get; set; } // Top border for rectangle framing

        public Int16 FrColor { get; set; } // Framing color

        public Int16 FrWidth { get; set; } // Framing width for line framing

        public Int16 Res4 { get; set; } // not used

        public uint Res5 { get; set; } // not used

        public Int16 FrOfX { get; set; } // Horizontal offset for shadow framing

        public Int16 FrOfY { get; set; } // Vertical offset for shadow framing

        public void SetupBinaryParser(BinaryParser<OcadFileTextSymbol> parser)
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
                _ => _.LineSpace,
                _ => _.ParaSpace,
                _ => _.IndentFirst,
                _ => _.IndentOther,
                _ => _.nTabs,
                _ => _.Tabs,
                _ => _.LBOn,
                _ => _.LBColor,
                _ => _.LBWidth,
                _ => _.LBDist,
                _ => _.Res2,
                _ => _.FrMode,
                _ => _.FrLineStyle,
                _ => _.PointSymOn,
                _ => _.PointSymNumber,
                _ => _.Res3,
                _ => _.FrLeft,
                _ => _.FrBottom,
                _ => _.FrRight,
                _ => _.FrTop,
                _ => _.FrColor,
                _ => _.FrWidth,
                _ => _.Res4,
                _ => _.Res5,
                _ => _.FrOfX,
                _ => _.FrOfY);


            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
            parser.SetArrayLength(_ => _.FontName, 31);
            parser.SetArrayLength(_ => _.Tabs, 32);
            parser.SetArrayLength(_ => _.Res3, 18);
        }
    }
}