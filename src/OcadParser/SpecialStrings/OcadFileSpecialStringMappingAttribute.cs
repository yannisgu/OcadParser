using System;

namespace OcadParser
{
    public class OcadFileSpecialStringMappingAttribute : Attribute
    {
        public string[] Letters { get; set; }

        public OcadFileSpecialStringMappingAttribute(params string[] letters)
        {
            Letters = letters;
        }
    }
}