using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcadParser.Models
{
    public abstract class OcadBaseProject
    {
        private readonly List<Symbol> _symbols = new List<Symbol>();
        private readonly List<OcadObject> _objects = new List<OcadObject>();

        public List<Symbol> Symbols
        {
            get { return _symbols; }
        }

        public List<OcadObject> Objects
        {
            get { return _objects; }
        }

        public virtual void Load(OcadFile ocadFile)
        {
            LoadColors(ocadFile);
            LoadSymbols(ocadFile);
            LoadObjects(ocadFile);
        }

        private void LoadColors(OcadFile ocadFile)
        {
            foreach(var color in ocadFile.Strings.Where(_ => _.Record is ColorSSR))
            {
                var colorRecord = (ColorSSR)color.Record;

                var colorModel = new OcadColor()
                {
                    Name = colorRecord.Name,
                    Number = short.Parse(colorRecord.Number),
                    Cyan = colorRecord.Cyan,
                    Magenta = colorRecord.Magenta,
                    Yellow = colorRecord.Yellow,
                    Black = colorRecord.Black,
                    Overprint = colorRecord.Overprint,
                    Transparency = colorRecord.Transparency,
                    SpotColorSeparationName = colorRecord.SpotColorSeparationName,
                    PercentageInTheSpotColorSeparation = colorRecord.PercentageInTheSpotColorSeparation,
                };
                Colors.Add(colorModel);
            }
        }

        public List<OcadColor> Colors { get; set; }

        private void LoadObjects(OcadFile ocadFile)
        {
            Objects.Clear();
            var objectIndexBlocks = ocadFile.OcadFileObjectIndex.SelectMany(_ => _.OcadFileObjectIndex).ToArray();
            for (var i = 0; i < ocadFile.Objects.Count; i++)
            {
                var obj = ocadFile.Objects[i];
                var objIndexBlock = objectIndexBlocks[i];

                Objects.Add(new OcadObject()
                {
                    Symbol = Symbols.FirstOrDefault(_ => _.Code == GetSymbolNumber(obj.Sym))
                });    
            }
        }

        private float GetSymbolNumber(int symbol)
        {
            // ReSharper disable once PossibleLossOfFraction
            return symbol/1000;
        }

        private void LoadSymbols(OcadFile ocadFile)
        {
            Symbols.Clear();
            foreach (var symbol in ocadFile.Symbols)
            {
                Symbol newSymbol;
                if (symbol is OcadFileAreaSymbol)
                {
                    newSymbol = new AreaSymbol();
                }
                else if (symbol is OcadFileLineSymbol)
                {
                    newSymbol = new FileLineSymbol();
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
                Symbols.Add(newSymbol);
            }
        }
    }
}
