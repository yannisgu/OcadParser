using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using OcadParser.Models;
using RP.Math;
using Svg;
using Svg.Pathing;
using Svg.Transforms;

namespace OcadParser.Renderer
{
    public class OcadRenderer
    {
        private readonly OcadBaseProject project;
        private SvgDocument svg;

        private readonly List<KeyValuePair<int, SvgElement>> elements = new List<KeyValuePair<int, SvgElement>>(); 

        public OcadRenderer(OcadBaseProject project)
        {
            this.project = project;
        }

        public SvgDocument Svg
        {
            get
            {
                EnsureSvg();
                return svg;
            }
        }

        public void EnsureSvg()
        {
            if (svg == null)
            {
                svg = new SvgDocument();
                RenderSvg();
            }
        }

        private void RenderSvg()
        {
            foreach (var ocadObject in project.Objects.Where(_ => _.Status == OcadFileOcadObject.OcadFileObjectStatus.Normal))
            {
                RenderSymbol(project.Symbols.FirstOrDefault(_ => _.SymNum == ocadObject.Sym), ocadObject.Poly, new string(ocadObject.Chars), (float)ocadObject.Ang / 10, ocadObject);
            }
            foreach (var element in elements.OrderByDescending(_ => _.Key).Select(_ => _.Value))
            {
                Svg.Children.Add(element);
            }
        }

        private void RenderSymbol(OcadFileBaseSymbol symbol, TdPoly[] poly, string text, float angle, OcadFileOcadObject obj)
        {
            if (symbol != null && symbol.Status != OcadFileSymbolStatus.Hidden)
            {
                IEnumerable<Tuple<int, SvgElement>> elementsOfThisSymbol = null;

                if (symbol is OcadFilePointSymbol)
                {
                    elementsOfThisSymbol = RenderPointSymbol((OcadFilePointSymbol) symbol, poly, angle);
                }
                else if (symbol is OcadFileLineSymbol)
                {
                    elementsOfThisSymbol = RenderLineSymbol((OcadFileLineSymbol) symbol, poly);
                }
                else if (symbol is OcadFileAreaSymbol)
                {
                    elementsOfThisSymbol = RenderAreaSymol((OcadFileAreaSymbol) symbol, poly);
                }
                else if (symbol is OcadFileTextSymbol)
                {
                    elementsOfThisSymbol = RenderText((OcadFileTextSymbol) symbol, poly, text,angle);
                }
                else if (symbol is OcadFileLineTextSymbol)
                {
                }

                if (elementsOfThisSymbol != null)
                {
                    var elementsOfThisSymbolArray = elementsOfThisSymbol.ToArray();
                    foreach (var element in elementsOfThisSymbolArray)
                    {
                        elements.Add(new KeyValuePair<int, SvgElement>(GetDrawIndex((short) element.Item1),
                            element.Item2));
                    }
                    ObjectElementMapping[obj] = elementsOfThisSymbolArray.Select(_ => _.Item2).ToList();
                }
            }
        }

        private IEnumerable<Tuple<int, SvgElement>> RenderText(OcadFileTextSymbol symbol, TdPoly[] poly, string text, float angle)
        {
            var points = poly.Select(GetSvgUnit).ToList();
            var element = new SvgText()
            {
                X = new SvgUnitCollection(),
                Y = new SvgUnitCollection(),
                FontFamily =  new string(symbol.FontName.Skip(1).Select(_ => (char)_).ToArray()).TrimEnd('\0'),
                Transforms = new SvgTransformCollection() { new SvgRotate(-angle, points.First().X, points.First().Y) },
                FontSize =  (int)(symbol.FontSize * 10 * 0.376),
                Fill = new SvgColourServer(GetColor(symbol.FontColor))
            };
            element.X.Add(points.Select(_ => _.X).First());
            element.Y.Add(points.Select(_ => _.Y).First());
            var i = 0;
            foreach (var line in text.Split('\n'))
            {
                var fixedLine = line.Replace("\t", "");

                var tSpan = new SvgTextSpan();
                tSpan.SpaceHandling = XmlSpaceHandling.preserve;
                if (i != 0)
                {
                    tSpan.Dy =
                        new SvgUnitCollection
                        { new SvgUnit((int) (symbol.FontSize*10*0.376))};
                    tSpan.X = new SvgUnitCollection
                    { new SvgUnit(points.Select(_ => _.X).First())};
                    
                }
                tSpan.Nodes.Add(new SvgContentNode() {Content = fixedLine });
                element.Children.Add(tSpan);
                i++;
            }
            yield return new Tuple<int, SvgElement>(
                symbol.FontColor, element);
        }

        private IEnumerable<Tuple<int, SvgElement>> RenderAreaSymol(OcadFileAreaSymbol symbol, TdPoly[] poly)
        {
            if (symbol.FillOn)
            {   
                var area = new SvgPath()
                {
                    PathData = GetPathData(poly),
                    Fill = new SvgColourServer(GetColor(symbol.FillColor))
                };
                yield return new Tuple<int, SvgElement>(
                    symbol.FillColor,
                    area);
            }
            if (symbol.HatchMode == OcadFileHatchMode.Cross || symbol.HatchMode == OcadFileHatchMode.Single)
            {
                yield return RenderHatch(symbol.HatchDist, symbol.HatchLineWidth, symbol.HatchColor, symbol.HatchAngle1, poly);
                if (symbol.HatchMode == OcadFileHatchMode.Cross)
                {
                    yield return RenderHatch(symbol.HatchDist, symbol.HatchLineWidth, symbol.HatchColor, symbol.HatchAngle2, poly);
                }
            }
        }

        private Tuple<int, SvgElement> RenderHatch(short hatchDist, short hatchLineWidth, short hatchColor, short origAngle, TdPoly[] poly)
        {
            var angle = (float)origAngle/10;
            var pattern = new SvgPatternServer()
            {
                PatternUnits = SvgCoordinateUnits.UserSpaceOnUse,
                Width = 10,
                Height = hatchDist,
                PatternTransform = new SvgTransformCollection()
                {
                    new SvgRotate(-angle)
                }
            };

            var line = new SvgLine()
            {
                Stroke = new SvgColourServer(GetColor(hatchColor)),
                StrokeWidth = hatchLineWidth,
                StartX = 0,
                EndX = 10,
                StartY = hatchLineWidth / 2,
                EndY = hatchLineWidth / 2,
            };
            pattern.Children.Add(line);

            return new Tuple<int, SvgElement>(hatchColor, new SvgPath()
            {
                PathData = GetPathData(poly),
                Fill = pattern
            });/*
            var angle = origAngle/10;
            if (angle > 180)
            {
                angle = (short) (angle - 180);
            }
            
            var width = hatchDist;
            var lineWidth = hatchDist - hatchLineWidth;
            double height;
            if (angle != 0 && angle != 90)
            {
                height = Math.Tan(Math.PI*angle/180.0)*lineWidth;
            }
            else
            {
                height = lineWidth;
            }
            var patternHeight = height;
            var pattern = new SvgPatternServer()
            {
                PatternUnits = SvgCoordinateUnits.UserSpaceOnUse,
                Width = width,
                Height = (float)patternHeight
            };
            var startX = angle <=
                         90
                ? hatchLineWidth/2
                : width - hatchLineWidth/2;
            var line = new SvgLine()
            {
                Stroke = new SvgColourServer(GetColor(hatchColor)),
                StrokeWidth = hatchLineWidth,
                StartX = startX,
                EndX = width-startX,
                StartY = 0,
                EndY = (float)patternHeight,
            };
            pattern.Children.Add(line);
            
            elements.Add(new KeyValuePair<int, SvgElement>(GetDrawIndex(hatchColor), new SvgPath()
            {
                PathData = GetPathData(poly),
                Fill = pattern
            }));*/
        }

        private IEnumerable<Tuple<int, SvgElement>> RenderLineSymbol(OcadFileLineSymbol symbol, TdPoly[] poly)
        {
            if (symbol.LineWidth > 0)
            {
                var line = new SvgPath()
                {
                    PathData = GetPathData(poly),
                    Stroke = new SvgColourServer(GetColor(symbol.LineColor)),
                    StrokeWidth = symbol.LineWidth,
                    Fill = SvgPaintServer.None
                };
                if (symbol.MainGap > 0)
                {
                    line.StrokeDashArray = new SvgUnitCollection()
                    {
                        new SvgUnit(symbol.MainLength),
                        new SvgUnit(symbol.MainGap)
                    };
                }
                yield return new Tuple<int, SvgElement>(
                    symbol.LineColor,
                    line);
            }
            if (symbol.DblMode != 0)
            {
                foreach (var element in RenderDoubleLine(symbol, poly))
                {
                    yield return element;
                }
            }

        }

        private IEnumerable<Tuple<int,SvgElement>> RenderDoubleLine(OcadFileLineSymbol symbol, TdPoly[] poly)
        {
            var line = new SvgPath()
            {
                PathData = GetPathData(poly),
                Stroke = new SvgColourServer(GetColor(symbol.DblFillColor)),
                StrokeWidth = symbol.DblWidth,
                Fill = SvgPaintServer.None
            };
            yield return new Tuple<int, SvgElement>(
                symbol.DblFillColor,
                line);

            var newPoly = MoveBezierPoly(poly, (symbol.DblWidth / 2) + (symbol.DblLeftWidth / 2), symbol.DblWidth);
            var lineLeft = new SvgPath()
            {
                PathData = GetPathData(newPoly),
                Stroke = new SvgColourServer(GetColor(symbol.DblLeftColor)),
                StrokeWidth = symbol.DblLeftWidth,
                Fill = SvgPaintServer.None
            };
            yield return new Tuple<int, SvgElement>(
                symbol.DblLeftColor,
                lineLeft);

            var polyRight = MoveBezierPoly(poly, -((symbol.DblWidth / 2) + (symbol.DblLeftWidth / 2)), symbol.DblWidth);
            var lineRight = new SvgPath()
            {
                PathData = GetPathData(polyRight),
                Stroke = new SvgColourServer(GetColor(symbol.DblRightColor)),
                StrokeWidth = symbol.DblRightWidth,
                Fill = SvgPaintServer.None
            };
            yield return new Tuple<int, SvgElement>(
                symbol.DblRightColor,
                lineRight);
        }

        private TdPoly[] MoveBezierPoly(TdPoly[] poly, int moveByBase, int lineWidth)
        {
            var newPoly = new List<TdPoly>();
            
            for (var i = 0; i < poly.Length; i++)
            {
                Vector3 moveDirectionVector;
                double factor;
                var originalPoint = GetVector(poly[i]);

                if (i == 0)
                {
                    var firstPoint = GetVector(poly[0]);
                    var secondPoint = GetVector(poly[1]);
                    var firstPointVector = (firstPoint - secondPoint);
                    moveDirectionVector = new Vector3(firstPointVector.Y, -firstPointVector.X, 0);
                    factor = moveByBase;
                }
                else if (i + 1 == poly.Length)
                {
                    var secondLastPoint = GetVector(poly[i-1]);
                    var secondLastPointVector = (secondLastPoint - originalPoint);
                    moveDirectionVector = new Vector3(secondLastPointVector.Y, -secondLastPointVector.X, 0);
                    factor = moveByBase;
                }
                else
                {
                    var vectorBefore = GetVector(poly[i - 1]);
                    var vectorAfter = GetVector(poly[i + 1]);
                    var vectorToBefore = vectorBefore - originalPoint;
                    var vectorToAfter =  vectorAfter - originalPoint;
                    moveDirectionVector = vectorToBefore.Scale(1) + vectorToAfter.Scale(1);

                    var dp1 = vectorToAfter.X * vectorToBefore.Y;
                    var dp2 = vectorToBefore.X * vectorToAfter.Y;

                    if (dp1 - dp2 >= 0)
                    {
                        moveDirectionVector =   vectorToBefore.Scale(1) + vectorToAfter.Scale(1);
                    }
                    else
                    {
                        moveDirectionVector = -vectorToBefore.Scale(1) + -vectorToAfter.Scale(1);
                    }
                    
                    var angle = vectorToBefore.Angle(vectorToAfter);
                    factor = moveByBase / Math.Sin(angle/2);

                    if (moveDirectionVector == Vector3.Zero)
                    {
                        moveDirectionVector = new Vector3(vectorToBefore.Y, -vectorToBefore.X, 0); ;
                    }

                }

                Vector3 moveVector;
                if (factor < 0)
                {
                    moveVector = -moveDirectionVector.Scale(-factor);
                }
                else
                {
                    moveVector = moveDirectionVector.Scale(factor);

                }
                var newPoint = originalPoint + moveVector;
                newPoly.Add(new TdPoly((int)newPoint.X, (int)newPoint.Y, poly[i]));
            }

            /*for (var i = 0; i < poly.Length; i++)
            {
                var originalPoint = GetVector(poly[i]);

                Vector3 vectorBefore;
                if (i == 0)
                {
                    vectorBefore = GetVector(poly[i]);
                }
                else
                {
                    vectorBefore = GetVector(poly[i - 1]);
                }
                Vector3 vectorAfter;
                if (i + 1 == poly.Length)
                {
                    vectorAfter = GetVector(poly[i]);
                }
                else
                {
                    vectorAfter = GetVector(poly[i + 1]);
                }
                var vectorReference = vectorAfter - vectorBefore;
                var vectorMoveDirection = new Vector3(vectorReference.X, -vectorReference.Y, 0);
                var moveVector = vectorMoveDirection.Scale(moveBy);
                var originalPoint = GetVector(poly[i]);
                var newPoint = originalPoint + moveVector;
                newPoly.Add(new TdPoly((int) newPoint.X, (int) newPoint.Y, poly[i]));
            }*/

            return newPoly.ToArray();
        }

        private static Vector3 GetVector(TdPoly poly)
        {
            return new Vector3(poly.X.Coordinate, poly.Y.Coordinate, 0);
        }


        private IEnumerable<Tuple<int, SvgElement>> RenderPointSymbol(OcadFilePointSymbol symbol, TdPoly[] poly, float angle)
        {
            var point = poly[0];
            var svgPoint = GetSvgUnit(point);

            foreach (var element in symbol.Elements)
            {
                SvgElement svgElement = null;
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
                        svgElement = circle;
                        break;
                    case OcadFileSymbolElementType.Area:
                        var area = new SvgPath()
                        {
                            PathData = GetPathData(element.Poly.Select(_ => _.MoveBy(point)).ToArray()),
                            Fill = new SvgColourServer(GetColor(element.Color))
                        };
                       svgElement = area;
                        break;
                    case OcadFileSymbolElementType.Line:
                        var line = new SvgPath()
                        {
                            PathData = GetPathData(element.Poly.Select(_ => _.MoveBy(point)).ToArray()),
                            Stroke = new SvgColourServer(GetColor(element.Color)),
                            StrokeWidth = element.LineWidth,
                            Fill = SvgPaintServer.None
                        };
                        svgElement = line;
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
                       svgElement =dot;
                        break;
                }

                if (svgElement != null)
                {
                    if (angle != 0)
                    {
                        svgElement.Transforms = new SvgTransformCollection()
                        {
                            new SvgRotate(-angle, svgPoint.X, svgPoint.Y)
                        };
                    }
                    yield return new Tuple<int, SvgElement>(
                                element.Color, svgElement);
                }

            }
        }

        private int GetDrawIndex(short color)
        {
            return project.Colors.FindIndex(_ => _.Number == color);
        }

        private Color GetColor(short color)
        {
            return GetColor(project.Colors.FirstOrDefault(_ => _.Number == color));
        }

        private SvgPathSegmentList GetPathData(TdPoly[] poly)
        {
            var pathSegmentList = new SvgPathSegmentList();
            /*
            for (var i = 1; i < poly.Length; i++)
            {
                pathSegmentList.Add(new SvgLineSegment(
                   new PointF(poly[i-1].X.Coordinate, -poly[i-1].Y.Coordinate),
                   new PointF(poly[i].X.Coordinate, -poly[i].Y.Coordinate) ));
            }*/
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
            var minX = project.File.Objects.Min(_ => _.Poly.Min(p => p.X.Coordinate));
            var minY = -project.File.Objects.Max(_ => _.Poly.Max(p => p.Y.Coordinate));
            var maxX = project.File.Objects.Max(_ => _.Poly.Max(p => p.X.Coordinate));
            var maxY = -project.File.Objects.Min(_ => _.Poly.Min(p => p.Y.Coordinate));
            Svg.ViewBox = new SvgViewBox(minX, minY, maxX- minX, maxY - minY);
            Svg.Width =  GetPixel(maxX - minX);
            Svg.Height = GetPixel(maxY - minY);
            return Svg.Draw();
        }

        public Dictionary<OcadFileOcadObject, List<SvgElement>> ObjectElementMapping { get; } = new Dictionary<OcadFileOcadObject, List<SvgElement>>(); 

        public Bitmap GetBitmap(OcadFileOcadObject obj, int targetWidth)
        {
            var minX = obj.Poly.Min(p => p.X.Coordinate);
            var minY = -obj.Poly.Max(p => p.Y.Coordinate);
            var maxX = obj.Poly.Max(p => p.X.Coordinate);
            var maxY = -obj.Poly.Min(p => p.Y.Coordinate);
            Svg.ViewBox = new SvgViewBox(minX, minY, maxX - minX, maxY - minY);
            Svg.Width = targetWidth;
            Svg.Height = targetWidth * ((maxY - minY) / (maxX - minX));
            return Svg.Draw();
        }

        public Bitmap GetBitmap(int minX, int minY, int maxX, int maxY, int targetWidth)
        {
            Svg.ViewBox = new SvgViewBox(minX, minY, maxX - minX, maxY - minY);
            Svg.Width = targetWidth;
            Svg.Height = targetWidth * ((float)(maxY - minY) / (maxX - minX));
            return Svg.Draw();
        }

        private SvgUnit GetPixel(int ocadUnit)
        {
            var mm = (float) ocadUnit/100;
            var inch = mm*0.0393700787;
            return new SvgUnit(SvgUnitType.Pixel, (float)inch*450); // 300dpi
        }
    }
}
