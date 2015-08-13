using System;

namespace OcadParser
{
    public class OcadFileSpecialStringMappingAttribute : Attribute
    {
        public string Letter { get; set; }

        public OcadFileSpecialStringMappingAttribute(string letter)
        {
            Letter = letter;
        }
    }
}