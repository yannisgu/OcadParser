using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OcadParser.Models;
using Svg;
using Svg.Pathing;
using Svg.Transforms;

namespace OcadParser.Renderer
{
    public class OcadRenderer
    {
        private readonly OcadBaseProject project;
        private SvgDocument svg;

        public OcadRenderer(OcadBaseProject project)
        {
            this.project = project;
        }

        public SvgDocument Svg
        {
            get {
                if (svg == null)
                {
                    svg = new SvgDocument();
                    RenderSvg();
                }
                return svg;
            }
        }

        private void RenderSvg()
        {
            foreach (var ocadObject in project.Objects.Where(_ => _.Status == OcadFileOcadObject.OcadFileObjectStatus.Normal))
            {
                RenderSymbol(project.Symbols.FirstOrDefault(_ => _.SymNum == ocadObject.Sym), ocadObject.Poly);
            }
        }

        private void RenderSymbol(OcadFileBaseSymbol symbol, TdPoly[] poly)
        {
            if (symbol is OcadFilePointSymbol)
            {
                RenderPointSymbol((OcadFilePointSymbol)symbol, poly);
            }
            else if (symbol is OcadFileLineSymbol)
            {
                
            }
        }

        
        private void RenderPointSymbol(OcadFilePointSymbol symbol, TdPoly[] poly)
        {
            var point = poly[0];
            var svgPoint = GetSvgUnit(point);

            foreach (var element in symbol.Elements)
            {
                switch (element.Type)
                {
                    case OcadFileSymbolElementType.Circle:
                        var center = GetSvgUnit(element.Poly[0]);
                        var circle = new SvgCircle()
                        {
                            CenterX = svgPoint.X + center.X,
                            CenterY = svgPoint.Y + center.Y,
                            Radius = element.Diameter / 2,
                            Fill = SvgPaintServer.None,
                            Stroke = new SvgColourServer(GetColor(element.Color)),
                            StrokeWidth = element.LineWidth
                        };
                        svg.Children.Add(circle);
                        break;
                    case OcadFileSymbolElementType.Area:
                        var area = new SvgPath()
                        {
                            PathData = GetPathData(element.Poly.Select(_ => _.MoveBy(point)).ToArray()),
                            Fill = new SvgColourServer(GetColor(element.Color))
                        };
                        svg.Children.Add(area);
                        break;
                    case OcadFileSymbolElementType.Line:
                        var line = new SvgPath()
                        {
                            PathData = GetPathData(element.Poly.Select(_ => _.MoveBy(point)).ToArray()),
                            Stroke = new SvgColourServer(GetColor(element.Color)),
                            StrokeWidth = element.LineWidth,
                            Fill = SvgPaintServer.None
                        };
                        svg.Children.Add(line);
                        break;
                    case OcadFileSymbolElementType.Dot:
                        center = GetSvgUnit(element.Poly[0]);
                        var dot = new SvgCircle()
                        {
                            CenterX = svgPoint.X + center.X,
                            CenterY = svgPoint.Y + center.Y,
                            Radius = element.Diameter / 2,
                            Fill = new SvgColourServer(GetColor(element.Color))
                        };
                        svg.Children.Add(dot);
                        break;
                }
            }
        }

        private Color GetColor(short color)
        {
            return GetColor(project.Colors.FirstOrDefault(_ => _.Number == color));
        }

        private SvgPathSegmentList GetPathData(TdPoly[] poly)
        {
            var pathSegmentList = new SvgPathSegmentList();
            PointF start = GetPoint(poly[0]);
            pathSegmentList.Add(new SvgMoveToSegment(start));
            int i = 1;
            PointF? firstBezier = null;
            PointF? secondBezier = null;
            while (i < poly.Length)
            {
                var point = GetPoint(poly[i]);
                if (poly[i].IsFirstBezierCurvePoint)
                {
                    firstBezier = point;
                }
                else if (poly[i].IsSecondBezierCurvePoint)
                {
                    secondBezier = point;
                }
                else
                {
                    if (firstBezier != null || secondBezier != null)
                    {
                        if (firstBezier != null && secondBezier != null)
                        {
                            pathSegmentList.Add(new SvgCubicCurveSegment(start, (PointF) firstBezier,
                                (PointF) secondBezier, point));
                        }
                        else
                        {
                            pathSegmentList.Add(new SvgQuadraticCurveSegment(start,
                                (PointF) (firstBezier ?? secondBezier), point));
                        }
                    }
                    else
                    {
                        pathSegmentList.Add(new SvgLineSegment(start, point));
                    }
                    start = point;
                    firstBezier = null;
                    secondBezier = null;
                }
                i++;
            }
            return pathSegmentList;
        }

        private SvgPoint GetSvgUnit(TdPoly poly)
        {
            return new SvgPoint(poly.X.Coordinate, -poly.Y.Coordinate);
        }

        private PointF GetPoint(TdPoly poly)
        {
            return new PointF(poly.X.Coordinate, -poly.Y.Coordinate);
        }

        private Color GetColor(OcadColor color)
        {
            var r = (int)(255 * (1 - color.Cyan) * (1 - color.Black));
            var g = (int)(255 * (1 - color.Magenta) * (1 - color.Black));
            var b = (int)(255 * (1 - color.Yellow) * (1 - color.Black));
            return Color.FromArgb((int)(color.Transparency * 255), r, g, b);
        }

        public Bitmap GetBitmap()
        {
            Svg.ViewBox = new SvgViewBox(Svg.Bounds.X,Svg.Bounds.Y, Svg.Bounds.Width, Svg.Bounds.Height);
            return Svg.Draw();
        }
    }
}
