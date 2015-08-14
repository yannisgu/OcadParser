using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace OcadParser
{
    public class OcadFilePointSymbol : OcadFileBaseSymbol, IBinaryParsable<OcadFilePointSymbol>
    {
        public void SetupBinaryParser(BinaryParser<OcadFilePointSymbol> parser)
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
                _ => _.DataSize,
                _ => _.Reserved);

            parser.SetArrayLength(_ => _.Colors, 14);
            parser.SetArrayLength(_ => _.Description, 64);
            parser.SetArrayLength(_ => _.IconBits, 484);
            parser.SetArrayLength(_ => _.SymbolTreeGroup, 64);
            parser.SetDynamicList(
                _ => _.Elements, 
                _ => Elements.Sum(e => e.NumberPoly + 2) < _.DataSize);
        }

        public ushort DataSize { get; set; }
        public Int16 Reserved { get; set; }
        public List<OcadFileSymbolElement> Elements { get;  } = new List<OcadFileSymbolElement>();
        
    }

    public class OcadFileSymbolElement : IBinaryParsable<OcadFileSymbolElement>
    {
        public Int16 Type { get; set; } // type of the symbol element
        //   1: line
        //   2: area
        //   3: circle
        //   4: dot (filled circle)
        public ushort Flags { get; set; } // Flags
        //   1: line with round ends
        public Int16 Color { get; set; } // color of the object. This is the number which appears in
        // the colors dialog box
        public Int16 LineWidth { get; set; } // line width for lines and circles unit 0.01 mm


        public Int16 Diameter { get; set; } // Diameter for circles and dots. The line width is included
        // one time in this dimension for circles.
        public Int16 NumberPoly { get; set; } // number of coordinates
        public int Res { get; set; }

        public TdPoly[] Poly { get; set; }
        
        public void SetupBinaryParser(BinaryParser<OcadFileSymbolElement> parser)
        {
            parser.SetPropertyOrder(
                _ => _.Type,
                _ => _.Flags,
                _ => _.Color,
                _ => _.LineWidth,
                _ => _.Diameter,
                _ => _.NumberPoly,
                _ => _.Res,
                _ => _.Poly);

            parser.SetArrayLength(_ => _.Poly, _ => _.NumberPoly);
        }
    }
}
