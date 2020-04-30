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

        #region temporary variables
        // TODO: MOVE THESE TO PROJECT PROPERTIES
        private static readonly Brush _axisLineBrush = Brushes.White,
                                      _unitLineBrush = Brushes.LightGray,
                                      _stepLineBrush = Brushes.LightSlateGray;

        private static readonly double _axisLineThickness = 5,
                                       _unitLineThickness = 2,
                                       _stepLineThickness = 1;

        private static readonly int _axisZ = -1,
                                    _unitZ = -2,
                                    _stepZ = -3;
        #endregion

        public static void Draw(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawBackgroundGrid(canvas, data);
            CoordinateSystemDrawer.DrawAxes(canvas, data);
        }

        #region Background Grid
        private static void DrawBackgroundGrid(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawVerticalBackgroundLines(canvas, data);
            CoordinateSystemDrawer.DrawHorizontalBackgroundLines(canvas, data);
        }
        private static void DrawVerticalBackgroundLines(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);
            double x = data.MinX;
            if (data.MinX % data.StepX != 0)
                x = Utility.Round(x, data.StepX);

            while (x < data.MaxX) {
                if (x <= data.MinX) {
                    x += data.StepX;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector(x, data.MinY),
                                                         new Vector(x, data.MaxY),
                                                         CoordinateSystemDrawer._stepLineBrush,
                                                         CoordinateSystemDrawer._stepLineThickness);

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

                Canvas.SetZIndex(line, CoordinateSystemDrawer._stepZ);

                canvas.Children.Add(line);

                x += data.StepX;
            }
        }
        private static void DrawHorizontalBackgroundLines(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            #region Horizontal Lines
            double y = data.MinY;
            if (data.MinY % data.StepY != 0)
                y = Utility.Round(y, data.StepY);

            while (y < data.MaxY) {
                if (y <= data.MinY) {
                    y += data.StepY;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(/*canvasSize,*/
                                                         //data,
                                                         new Vector(data.MinX, y),
                                                         new Vector(data.MaxX, y),
                                                         CoordinateSystemDrawer._stepLineBrush,
                                                         CoordinateSystemDrawer._stepLineThickness);

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

                Canvas.SetZIndex(line, CoordinateSystemDrawer._stepZ);

                canvas.Children.Add(line);

                y += data.StepY;
            }
            #endregion
        }
        #endregion

        #region Axes
        private static void DrawAxes(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawAxisUnitLabels(canvas, data);
            CoordinateSystemDrawer.DrawAxisArrows(canvas, data);
        }

        private static void DrawAxisUnitLabels(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawXAxisUnitLabels(canvas, data);
            CoordinateSystemDrawer.DrawYAxisUnitLabels(canvas, data);
        }
        private static void DrawXAxisUnitLabels(Canvas canvas, CoordinateSystemData data) {
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
            if (data.MinX % data.UnitX != 0)
                x = Utility.Round(x, data.UnitX);

            while (x < data.MaxX) {
                if (x <= data.MinX || x == 0) {
                    x += data.UnitX;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector(x, y + (-data.StepY * .5)),
                                                         new Vector(x, y + ( data.StepY * .5)),
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
                bool above;
                if (data.MinY < 0 && 0 < data.MaxY) {
                    above = false;
                }

                // case 2: 0 < Min < Max
                else if (0 < data.MinY) {
                    above = true;
                }

                // case 3: Min < Max < 0
                else if (0 > data.MaxY) {
                    above = false;
                }

                // case 4: (Min=0) < Max
                else if (data.MinY == 0) {
                    above = true;
                }

                // case 5: Min < (Max = 0)
                else if (data.MaxY == 0) {
                    above = false;
                } else throw new Exception();

                if (above) {
                    // above axis
                    labelY = y + data.StepY * .5;
                } else {
                    // under axis
                    labelY = y - data.StepY * .5;
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
                Size labelSize = Utility.GetTextSize(label.Content.ToString(), label.FontSize);

                if (above) {
                    // above axis
                    Canvas.SetTop(label, labelPosition.Y - labelSize.Height);
                } else {
                    // under axis
                    Canvas.SetTop(label, labelPosition.Y);
                }

                Canvas.SetLeft(label, labelPosition.X - labelSize.Width * .5);

                Canvas.SetZIndex(label, int.MaxValue);
                canvas.Children.Add(label);
                #endregion


                x += data.UnitX;
            }
        }
        private static void DrawYAxisUnitLabels(Canvas canvas, CoordinateSystemData data) {
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
            if (data.MinY % data.UnitY != 0)
                y = Utility.Round(y, data.UnitY);

            while (y < data.MaxY) {
                if (y <= data.MinY || y == 0) {
                    y += data.UnitY;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector((-data.StepX * .5) + x, y),
                                                         new Vector(( data.StepX * .5) + x, y),
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
                // Decide whether to place text left or right
                bool right;
                if (data.MinX < 0 && 0 < data.MaxX) {
                    right = false;
                }

                // case 2: 0 < Min < Max
                else if (0 < data.MinX) {
                    right = true;
                }

                // case 3: Min < Max < 0
                else if (0 > data.MaxX) {
                    right = false;
                }

                // case 4: (Min=0) < Max
                else if (data.MinX == 0) {
                    right = true;
                }

                // case 5: Min < (Max = 0)
                else if (data.MaxX == 0) {
                    right = false;
                } else throw new Exception();


                if (right) {
                    // right
                    labelX = x + data.StepX * .5;
                } else {
                    // left
                    labelX = x - data.StepX * .5;
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
                Size labelSize = Utility.GetTextSize(label.Content.ToString(), label.FontSize);

                Canvas.SetZIndex(label, int.MaxValue);

                if (right) {
                    // right
                    Canvas.SetLeft(label, labelPosition.X);
                } else {
                    // left
                    Canvas.SetLeft(label, labelPosition.X - labelSize.Width);
                }
                Canvas.SetTop(label, labelPosition.Y - labelSize.Height * .5);
                canvas.Children.Add(label);
                #endregion

                y += data.UnitY;
            }

        }

        private static void DrawAxisArrows(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawXAxis(canvas, data);
            CoordinateSystemDrawer.DrawYAxis(canvas, data);
            CoordinateSystemDrawer.DrawXAxisDirection(canvas, data);
            CoordinateSystemDrawer.DrawYAxisDirection(canvas, data);

        }
        private static void DrawXAxis(Canvas canvas, CoordinateSystemData data) {
            // If the X-Axis is visible
            if (data.MaxY > 0 && 0 > data.MinY) {
                Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

                BackgroundLine axis = new BackgroundLine(new Vector(data.MinX, 0),
                                                         new Vector(data.MaxX, 0),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                Vector pos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, axis.Pos1);
                Vector pos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, axis.Pos2);

                Line axisLine = new Line {
                    X1 = pos1.X,
                    Y1 = pos1.Y,
                    X2 = pos2.X,
                    Y2 = pos2.Y,
                    Stroke = axis.LineBrush,
                    StrokeThickness = axis.LineThickness,
                };

                Canvas.SetZIndex(axisLine, CoordinateSystemDrawer._axisZ);

                canvas.Children.Add(axisLine);
            }
        }
        private static void DrawYAxis(Canvas canvas, CoordinateSystemData data) {
            // If the Y-Axis is visible
            if (data.MaxX > 0 && 0 > data.MinX) {
                Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

                BackgroundLine axis = new BackgroundLine(new Vector(0, data.MinY),
                                                         new Vector(0, data.MaxY),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                Vector pos1 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, axis.Pos1);
                Vector pos2 = CoordinateConverter.FromCoordinateToPoint(canvasSize, data, axis.Pos2);

                Line axisLine = new Line {
                    X1 = pos1.X,
                    Y1 = pos1.Y,
                    X2 = pos2.X,
                    Y2 = pos2.Y,
                    Stroke = axis.LineBrush,
                    StrokeThickness = axis.LineThickness,
                };

                Canvas.SetZIndex(axisLine, CoordinateSystemDrawer._axisZ);

                canvas.Children.Add(axisLine);
            }
        }
        private static void DrawXAxisDirection(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            if (data.MaxY > 0 && 0 > data.MinY) {
                // Make sure the Arrow has a length > 0
                if (data.StepX == 0)
                    data.StepX = data.UnitX * .5;
                if (data.StepY == 0)
                    data.StepY = data.UnitY * .5;

                BackgroundLine top    = new BackgroundLine(new Vector(data.MaxX, 0),
                                                           new Vector(data.MaxX - data.StepX * .5, data.StepY * .5),
                                                           CoordinateSystemDrawer._axisLineBrush,
                                                           CoordinateSystemDrawer._axisLineThickness);
                BackgroundLine bottom = new BackgroundLine(new Vector(data.MaxX - data.StepX * .5, -data.StepY * .5),
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

        }
        private static void DrawYAxisDirection(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);
            if (data.MaxX > 0 && 0 > data.MinX) {
                // Make sure the Arrow has a length > 0
                if (data.StepX == 0)
                    data.StepX = data.UnitX * .5;
                if (data.StepY == 0)
                    data.StepY = data.UnitY * .5;

                BackgroundLine left = new BackgroundLine(new Vector(0, data.MaxY),
                                                         new Vector(data.StepX * .5, data.MaxY - data.StepY * .5),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                BackgroundLine right = new BackgroundLine(new Vector(-data.StepX * .5, data.MaxY - data.StepY * .5),
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
        #endregion

        //public static void ClearCanvas(Canvas canvas) {
        //    for (int i = canvas.Children.Count; i >= 0; i--) {
        //        if (canvas.Children[i] is Line || canvas.Children[i] is Label) {
        //            canvas.Children.RemoveAt(i);
        //        }
        //    }
        //}

    }
}
