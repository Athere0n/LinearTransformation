using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LinearTransformation.Model {
    public static class CoordinateSystemDrawer {

        private static readonly Brush _axisLineBrush = Brushes.White,
                                      _unitLineBrush = Brushes.LightGray,
                                      _stepLineBrush = Brushes.LightSlateGray;

        private static readonly double _axisLineThickness = 5,
                                       _unitLineThickness = 2,
                                       _stepLineThickness = 1;

        private static readonly int _axisZ = -1,
                                    _unitZ = -2,
                                    _stepZ = -3;

        public static void Draw(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawBackgroundLines(canvas, data);
            CoordinateSystemDrawer.DrawAxisLabels(canvas, data);
            CoordinateSystemDrawer.DrawAxisDirections(canvas, data);
        }

        private static void DrawAxisDirections(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            // Draw X-Axis Direction
            if (data.MaxY > 0 && 0 > data.MinY) {
                BackgroundLine top = new BackgroundLine(new Vector(data.MaxX, 0),
                                                         new Vector(data.MaxX - data.Step * .5, data.Step * .5),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                BackgroundLine bottom = new BackgroundLine(new Vector(data.MaxX - data.Step * .5, -data.Step * .5),
                                                          new Vector(data.MaxX, 0),
                                                          CoordinateSystemDrawer._axisLineBrush,
                                                          CoordinateSystemDrawer._axisLineThickness);

                Vector topPos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, top.Pos1);
                Vector topPos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, top.Pos2);

                Line topLine = new Line {
                    X1              = topPos1.X,
                    Y1              = topPos1.Y,
                    X2              = topPos2.X,
                    Y2              = topPos2.Y,
                    Stroke          = top.LineBrush,
                    StrokeThickness = top.LineThickness,
                };

                Canvas.SetZIndex(topLine, CoordinateSystemDrawer._unitZ);
                canvas.Children.Add(topLine);

                Vector bottomPos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, bottom.Pos1);
                Vector bottomPos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, bottom.Pos2);

                Line bottomLine = new Line {
                    X1              = bottomPos1.X,
                    Y1              = bottomPos1.Y,
                    X2              = bottomPos2.X,
                    Y2              = bottomPos2.Y,
                    Stroke          = bottom.LineBrush,
                    StrokeThickness = bottom.LineThickness,
                };

                Canvas.SetZIndex(bottomLine, CoordinateSystemDrawer._axisZ);
                canvas.Children.Add(bottomLine);
            }

            // Draw Y-Axis Direction
            if (data.MaxX > 0 && 0 > data.MinX) {
                // Draw Y axis direction
                BackgroundLine left = new BackgroundLine(new Vector(0, data.MaxY),
                                                         new Vector(data.Step * .5, data.MaxY - data.Step * .5),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                BackgroundLine right = new BackgroundLine(new Vector(-data.Step * .5, data.MaxY - data.Step * .5),
                                                          new Vector(0, data.MaxY),
                                                          CoordinateSystemDrawer._axisLineBrush,
                                                          CoordinateSystemDrawer._axisLineThickness);

                Vector leftPos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, left.Pos1);
                Vector leftPos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, left.Pos2);

                Line leftLine = new Line {
                    X1              = leftPos1.X,
                    Y1              = leftPos1.Y,
                    X2              = leftPos2.X,
                    Y2              = leftPos2.Y,
                    Stroke          = left.LineBrush,
                    StrokeThickness = left.LineThickness,
                };

                Canvas.SetZIndex(leftLine, CoordinateSystemDrawer._unitZ);
                canvas.Children.Add(leftLine);

                Vector rightPos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, right.Pos1);
                Vector rightPos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, right.Pos2);

                Line rightLine = new Line {
                    X1              = rightPos1.X,
                    Y1              = rightPos1.Y,
                    X2              = rightPos2.X,
                    Y2              = rightPos2.Y,
                    Stroke          = right.LineBrush,
                    StrokeThickness = right.LineThickness,
                };

                Canvas.SetZIndex(rightLine, CoordinateSystemDrawer._unitZ);
                canvas.Children.Add(rightLine);
            }
        }

        private static void DrawXAxisLabels(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            double y;

            // Is the x axis visible?
            if (data.MinY < 0 && 0 < data.MaxY) {
                y = 0;
            }
            // Axis will be placed bottom
            else if (data.MinY >= 0) {
                y = data.MinY;
            }
            // Axis will be placed top
            else {
                y = data.MaxY;
            }


            double x = data.MinX;
            if (data.MinX % data.Unit != 0)
                x = CoordinateConverter.Round(x, data.Unit);

            while (x < data.MaxX) {
                if (x <= data.MinX || x == 0) {
                    x += data.Unit;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector(x, y + (-data.Step * .5)),
                                                         new Vector(x, y + ( data.Step * .5)),
                                                         CoordinateSystemDrawer._unitLineBrush,
                                                         CoordinateSystemDrawer._unitLineThickness);

                Vector pos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos1);
                Vector pos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos2);

                Line line = new Line {
                    X1              = pos1.X,
                    Y1              = pos1.Y,
                    X2              = pos2.X,
                    Y2              = pos2.Y,
                    Stroke          = temp.LineBrush,
                    StrokeThickness = temp.LineThickness,
                };

                Canvas.SetZIndex(line, CoordinateSystemDrawer._unitZ);

                canvas.Children.Add(line);

                #region Text
                double labelY;
                // Decide whether to place text above or under
                if (y >= data.MaxY) {
                    // above
                    labelY = y + data.Step * .5;
                } else {
                    // under
                    labelY = y - data.Step * .5;
                }

                Label label = new Label {
                    Content = $"{x}",
                    Foreground = _unitLineBrush,
                    FontSize = 20,
                };

                Vector labelPosition = CoordinateConverter.FromCoordinateToPoint(canvasSize,
                                                                                 data,
                                                                                 new Vector(x,
                                                                                            labelY));
                // Calculate label dimensions
                Size labelSize = CoordinateSystemDrawer.GetTextSize(label.Content.ToString(), label.FontSize);

                if (y >= data.MaxY) {
                    // above
                    Canvas.SetTop(label, labelPosition.Y + labelSize.Height);
                } else {
                    // under
                    Canvas.SetTop(label, labelPosition.Y);
                }

                Canvas.SetLeft(label, labelPosition.X - labelSize.Width * .5);

                Canvas.SetZIndex(label, int.MaxValue);
                canvas.Children.Add(label);
                #endregion


                x += data.Unit;
            }
        }

        private static void DrawYAxisLabels(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            double x;

            // Is the Y axis visible?
            if (data.MinX < 0 && 0 < data.MaxX) {
                x = 0;
            }
            // Axis will be placed left
            else if (data.MinX >= 0) {
                x = data.MinX;
            }
            // Axis will be placed right
            else {
                x = data.MaxX;
            }

            double y = data.MinY;
            if (data.MinY % data.Unit != 0)
                y = CoordinateConverter.Round(y, data.Unit);

            while (y < data.MaxY) {
                if (y <= data.MinY || y == 0) {
                    y += data.Unit;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector((-data.Step * .5) + x, y),
                                                         new Vector(( data.Step * .5) + x, y),
                                                         CoordinateSystemDrawer._unitLineBrush,
                                                         CoordinateSystemDrawer._unitLineThickness);

                Vector pos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos1);
                Vector pos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos2);

                Line line = new Line {
                    X1              = pos1.X,
                    Y1              = pos1.Y,
                    X2              = pos2.X,
                    Y2              = pos2.Y,
                    Stroke          = temp.LineBrush,
                    StrokeThickness = temp.LineThickness,
                };

                Canvas.SetZIndex(line, CoordinateSystemDrawer._unitZ);

                canvas.Children.Add(line);

                #region Text
                double labelX;
                // Decide whether to place text above or under
                if (0 < data.MinX) {
                    // right
                    labelX = x + data.Step * .5;
                } else {
                    // left
                    labelX = x - data.Step * .5;
                }

                Label label = new Label {
                    Content = $"{y}",
                    Foreground = _unitLineBrush,
                    FontSize = 20,
                };

                Vector labelPosition = CoordinateConverter.FromCoordinateToPoint(canvasSize,
                                                                                 data,
                                                                                 new Vector(labelX,
                                                                                            y));

                // Calculate label dimensions
                Size labelSize = CoordinateSystemDrawer.GetTextSize(label.Content.ToString(), label.FontSize);

                Canvas.SetZIndex(label, int.MaxValue);

                if (0 < data.MinX) {
                    // right
                    Canvas.SetLeft(label, labelPosition.X);
                } else {
                    // left
                    Canvas.SetLeft(label, labelPosition.X - labelSize.Width);
                }
                Canvas.SetTop(label, labelPosition.Y - labelSize.Height * .5);
                canvas.Children.Add(label);
                #endregion

                y += data.Unit;
            }

        }

        private static Size GetTextSize(string text, double fontSize) {
            Label label = new Label {
                Content = text,
                FontSize = fontSize,
            };

            label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            label.Arrange(new Rect(label.DesiredSize));

            return new Size(label.ActualWidth, label.ActualHeight);
        }

        private static void DrawAxisLabels(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawXAxisLabels(canvas, data);
            CoordinateSystemDrawer.DrawYAxisLabels(canvas, data);
        }

        public static void ClearCanvas(Canvas canvas) {
            for (int i = canvas.Children.Count; i >= 0; i--) {
                if (canvas.Children[i] is Line || canvas.Children[i] is Label) {
                    canvas.Children.RemoveAt(i);
                }
            }
        }

        private static void DrawBackgroundLines(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawVerticalBackgroundLines(canvas, data);
            CoordinateSystemDrawer.DrawHorizontalBackgroundLines(canvas, data);
        }

        private static void DrawVerticalBackgroundLines(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);
            double x = data.MinX;
            if (data.MinX % data.Step != 0)
                x = CoordinateConverter.Round(x, data.Step);

            while (x < data.MaxX) {
                if (x <= data.MinX) {
                    x += data.Step;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector(x, data.MinY),
                                                         new Vector(x, data.MaxY),
                                                         (x == 0) ? CoordinateSystemDrawer._axisLineBrush : CoordinateSystemDrawer._stepLineBrush,
                                                         (x == 0) ? CoordinateSystemDrawer._axisLineThickness : CoordinateSystemDrawer._stepLineThickness);

                Vector pos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos1);
                Vector pos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos2);

                Line line = new Line {
                    X1              = pos1.X,
                    Y1              = pos1.Y,
                    X2              = pos2.X,
                    Y2              = pos2.Y,
                    Stroke          = temp.LineBrush,
                    StrokeThickness = temp.LineThickness,
                };

                Canvas.SetZIndex(line, (x == 0) ? CoordinateSystemDrawer._axisZ : CoordinateSystemDrawer._stepZ);

                canvas.Children.Add(line);

                x += data.Step;
            }
        }

        private static void DrawHorizontalBackgroundLines(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            #region Horizontal Lines
            double y = data.MinY;
            if (data.MinY % data.Step != 0)
                y = CoordinateConverter.Round(y, data.Step);

            while (y < data.MaxY) {
                if (y <= data.MinY) {
                    y += data.Step;
                    continue;
                }

                //double x1 = (y == 0) ? data.MinX : ((((int) y) == y) ? (-step * .25) : data.MinX);
                //double x2 = (y == 0) ? data.MaxX : ((((int) y) == y) ? ( step * .25) : data.MaxX);

                BackgroundLine temp = new BackgroundLine(/*canvasSize,*/
                                                         //data,
                                                         new Vector(data.MinX, y),
                                                         new Vector(data.MaxX, y),
                                                         (y == 0) ? CoordinateSystemDrawer._axisLineBrush : CoordinateSystemDrawer._stepLineBrush,
                                                         (y == 0) ? CoordinateSystemDrawer._axisLineThickness : CoordinateSystemDrawer._stepLineThickness);

                Vector pos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos1);
                Vector pos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, temp.Pos2);

                Line line = new Line {
                    X1              = pos1.X,
                    Y1              = pos1.Y,
                    X2              = pos2.X,
                    Y2              = pos2.Y,
                    Stroke          = temp.LineBrush,
                    StrokeThickness = temp.LineThickness,
                };

                Canvas.SetZIndex(line, (y == 0) ? CoordinateSystemDrawer._axisZ : CoordinateSystemDrawer._stepZ);

                canvas.Children.Add(line);

                y += data.Step;
            }
            #endregion
        }
    }
}
