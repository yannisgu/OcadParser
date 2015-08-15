namespace OcadParser.Models
{
    public class OcadColor
    {
        public string Name { get; set; }
        public short Number { get; set; }
        public double Cyan { get; set; }
        public double Magenta { get; set; }
        public double Yellow { get; set; }
        public double Black { get; set; }
        public string Overprint { get; set; }
        public float Transparency { get; set; }
        public string SpotColorSeparationName { get; set; }
        public string PercentageInTheSpotColorSeparation { get; set; }
    }
}