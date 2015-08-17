using System;

namespace OcadParser
{
    public class OcadFileOcadObject : IBinaryParsable<OcadFileOcadObject>
    {
        public int Sym { get; set; } // symbol number
        // -4 = layout vector object               
        // -3 = from PDF, AI
        // -2 =
        public byte Otp { get; set; } // object typ
        public byte Customer { get; set; } // 
        public Int16 Ang { get; set; } // Angle, unit is 0.1 degrees, used for
        // - point object
        // - area objects with structure
        // - unformatted text objects
        // - rectangle objects
        public int nItem { get; set; } // number of coordinates in the Poly array
        public Int16 nText { get; set; } // number of characters in the Poly, array used for storing text
        // nText is > 0 for
        // - line text objects
        // - text objects
        // for all other objects it is 0
        public byte Mark { get; set; } // Used for Marked property        
        public byte SnappingMark { get; set; } // Used for Snapping marked property
        public int Col { get; set; }
        // Color number for symbolized objects or color of graphic objects, -1 for image or layout objects
        public Int16 LineWidth { get; set; } //
        public Int16 DiamFlags { get; set; } // 
        public int ServerObjectId { get; set; } // added for server objects                
        public int Height { get; set; } // Height [1/256 mm]                      
        public double _Date { get; set; } // not used
        public TdPoly[] Poly { get; set; } // array[0..] coordinates of the object followed by a zero-terminated string

        public char[] Chars { get; set; } // if nText > 0 TCord is explained at the beginning of this description

        public byte Status { get; set; }

        public void SetupBinaryParser(BinaryParser<OcadFileOcadObject> parser)
        {
            parser.SetPropertyOrder(
                _ => _.Sym,
                _ => _.Otp,
                _ => _.Customer,
                _ => _.Ang,
                _ => _.nItem,
                _ => _.nText,
                _ => _.Mark,
                _ => _.SnappingMark,
                _ => _.Col,
                _ => _.LineWidth,
                _ => _.DiamFlags,
                _ => _.ServerObjectId,
                _ => _.Height,
                _ => _._Date,
                _ => _.Poly,
                _ => _.Chars);

            parser.SetArrayLength(_ => _.Poly, _ => _.nItem);
            parser.AddPropertyEnabledFunction(_ => _.Chars, _ => _.nText > 0);
        }


        public struct OcadFileObjectStatus
        {
            public static byte Normal = 1;
        }

        public override bool Equals(object otherObject)
        {
            var ocadObject = otherObject as OcadFileOcadObject;
            if (ocadObject == null)
            {
                return false;
            }

            if (Poly.Length != ocadObject.Poly.Length)
            {
                return false;
            }

            for (var i = 0; i < Poly.Length; i++)
            {
                if (!Poly[i].Equals(ocadObject.Poly[i]))
                {
                    return false;
                }
            }

            if (Sym != ocadObject.Sym)
            {
                return false;
            }

            return true;
        }


    }
}