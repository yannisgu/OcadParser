using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcadParser.Models
{
    public abstract class OcadBaseProject
    {
        public OcadFile File { get; private set; }

        public virtual void Load(OcadFile ocadFile)
        {
            File = ocadFile;
            Symbols = ocadFile.Symbols;
            Objects = ocadFile.Objects;
            LoadColors(ocadFile);
        }

        public List<OcadFileOcadObject> Objects { get; set; }

        public List<OcadFileBaseSymbol> Symbols { get; set; }

        private void LoadColors(OcadFile ocadFile)
        {
            foreach(var color in ocadFile.Strings.Where(_ => _.Record is ColorSSR))
            {
                var colorRecord = (ColorSSR)color.Record;

                var colorModel = new OcadColor()
                {
                    Name = colorRecord.Name,
                    Number = short.Parse(colorRecord.Number),
                    Cyan = double.Parse(colorRecord.Cyan) / 100,
                    Magenta = double.Parse(colorRecord.Magenta) / 100,
                    Yellow = double.Parse(colorRecord.Yellow) / 100,
                    Black = double.Parse(colorRecord.Black) / 100,
                    Overprint = colorRecord.Overprint,
                    Transparency = ParseFloat(colorRecord.Transparency) / 100,
                    SpotColorSeparationName = colorRecord.SpotColorSeparationName,
                    PercentageInTheSpotColorSeparation = colorRecord.PercentageInTheSpotColorSeparation,
                };
                Colors.Add(colorModel);
            }
        }

        private float ParseFloat(string text, float defaultValue = default(float))
        {
            float returnValue;
            if (!float.TryParse(text, out returnValue))
            {
                returnValue = defaultValue;
            }
            return returnValue;
        }

        public List<OcadColor> Colors { get;  } = new List<OcadColor>();
        
    }
}
