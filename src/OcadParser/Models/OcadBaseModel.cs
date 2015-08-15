using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcadParser.Models
{
    public abstract class OcadBaseProject
    {
        public List<Symbol> SymbolsOld { get; } = new List<Symbol>();

        public List<OcadObject> ObjectsOld { get; } = new List<OcadObject>();

        public OcadFile File { get; private set; }

        public virtual void Load(OcadFile ocadFile)
        {
            File = ocadFile;
            Symbols = ocadFile.Symbols;
            Objects = ocadFile.Objects;
            LoadColors(ocadFile);
            LoadSymbols(ocadFile);
            LoadObjects(ocadFile);
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
                    Transparency = float.Parse(colorRecord.Transparency) / 100,
                    SpotColorSeparationName = colorRecord.SpotColorSeparationName,
                    PercentageInTheSpotColorSeparation = colorRecord.PercentageInTheSpotColorSeparation,
                };
                Colors.Add(colorModel);
            }
        }

        public List<OcadColor> Colors { get;  } = new List<OcadColor>();

        private void LoadObjects(OcadFile ocadFile)
        {
            ObjectsOld.Clear();
            var objectIndexBlocks = ocadFile.OcadFileObjectIndex.SelectMany(_ => _.OcadFileObjectIndex).ToArray();
            for (var i = 0; i < ocadFile.Objects.Count; i++)
            {
                var obj = ocadFile.Objects[i];
                var objIndexBlock = objectIndexBlocks[i];

                ObjectsOld.Add(new OcadObject()
                {
                    Symbol = SymbolsOld.FirstOrDefault(_ => _.Code == GetSymbolNumber(obj.Sym)),
                    Poly = obj.Poly,
                    Status = (OcadObject.OcadObjectStatus)objIndexBlock.Status
                });    
            }
        }

        private float GetSymbolNumber(int symbol)
        {
            return (float)symbol/1000;
        }

        private void LoadSymbols(OcadFile ocadFile)
        {
            SymbolsOld.Clear();
            foreach (var symbol in ocadFile.Symbols)
            {
                Symbol newSymbol;
                if (symbol is OcadFileAreaSymbol)
                {
                    newSymbol = new AreaSymbol();
                }
                else if (symbol is OcadFileLineSymbol)
                {
                    newSymbol = new LineSymbol(this, (OcadFileLineSymbol)symbol);
                }
                else if (symbol is OcadFileLineTextSymbol)
                {
                    newSymbol = new FileLineTextSymbol();
                }
                else if (symbol is OcadFilePointSymbol)
                {
                    newSymbol = new PointSymbol()
                    {
                        Elements =
                            ((OcadFilePointSymbol) symbol).Elements.Select(_ => new SymbolElement()
                            {
                                Poly = _.Poly,
                                Diameter = _.Diameter,
                                Flags = (SymbolElement.SymbolElementFlag) _.Flags,
                                LineWidth = _.LineWidth,
                                Type = (SymbolElement.SymbolElementType)_.Type,
                                Color = Colors.FirstOrDefault(c => c.Number == _.Color)
                            }).ToList()
                    };
                }
                else if (symbol is OcadFileRectangleSymbol)
                {
                    newSymbol = new RectangleSymbol();
                }
                else if (symbol is OcadFileTextSymbol)
                {
                    newSymbol = new FileTextSymbol();
                }
                else
                {
                    newSymbol = new Symbol();
                }
                newSymbol.Code = GetSymbolNumber(symbol.SymNum);
                newSymbol.Description = new string(symbol.Description);
                SymbolsOld.Add(newSymbol);
            }
        }
    }
}
