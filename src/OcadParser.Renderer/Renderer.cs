using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcadParser.Models;
using Svg;

namespace OcadParser.Renderer
{
    public class Renderer
    {
        private readonly OcadBaseProject _project;

        public Renderer(OcadBaseProject project)
        {
            _project = project;
        }

        public SvgDocument GetSvg()
        {
            var svg = new SvgDocument();
            var symbol = _project.Symbols.Where(_ => _.Code == 1);
            return svg;
        }   
    }
}
