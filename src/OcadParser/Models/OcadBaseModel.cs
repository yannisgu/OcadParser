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
            LoadSymbols(ocadFile);
            LoadObjects(ocadFile);
        }

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
                    newSymbol = new PointSymbol();
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
