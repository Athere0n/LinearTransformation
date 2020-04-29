﻿using System;
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

        public static void Draw(Canvas canvas, CoordinateSystemData data, double unit = 1, double step = .5) {
            CoordinateSystemDrawer.DrawBackgroundLines(canvas, data, unit, step);
            CoordinateSystemDrawer.DrawAxisLabels(canvas, data, unit, step);
            CoordinateSystemDrawer.DrawAxisDirections(canvas, data, step);
        }

        private static void DrawAxisDirections(Canvas canvas, CoordinateSystemData data, double step) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            // TODO: ADJUST FOR WHEN AXIS IS NOT DISPLAYED ON 0

            // Draw X-Axis Direction
            if (data.MaxX > 0 && 0 > data.MinX) {
                BackgroundLine top = new BackgroundLine(new Vector(data.MaxX, 0),
                                                         new Vector(data.MaxX - step * .5, step * .5),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                BackgroundLine bottom = new BackgroundLine(new Vector(data.MaxX - step * .5, -step * .5),
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
            if (data.MaxY > 0 && 0 > data.MinY) {
                // Draw Y axis direction
                BackgroundLine left = new BackgroundLine(new Vector(0, data.MaxY),
                                                         new Vector(step * .5, data.MaxY - step * .5),
                                                         CoordinateSystemDrawer._axisLineBrush,
                                                         CoordinateSystemDrawer._axisLineThickness);
                BackgroundLine right = new BackgroundLine(new Vector(-step * .5, data.MaxY - step * .5),
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

        private static void DrawXAxisLabels(Canvas canvas, CoordinateSystemData data, double unit, double step) {
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
            if (data.MinX % unit != 0)
                x = CoordinateConverter.Round(x, unit);

            while (x < data.MaxX) {
                if (x <= data.MinX || x == 0) {
                    x += unit;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector(x, y + (-step * .5)),
                                                         new Vector(x, y + ( step * .5)),
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

                x += unit;
            }
        }

        private static void DrawYAxisLabels(Canvas canvas, CoordinateSystemData data, double unit, double step) {
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
            if (data.MinY % unit != 0)
                y = CoordinateConverter.Round(y, unit);

            while (y < data.MaxY) {
                if (y <= data.MinY || y == 0) {
                    y += unit;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector((-step * .5) + x, y),
                                                         new Vector(( step * .5) + x, y),
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

                y += unit;
            }

        }

        private static void DrawAxisLabels(Canvas canvas, CoordinateSystemData data, double unit, double step) {
            CoordinateSystemDrawer.DrawXAxisLabels(canvas, data, unit, step);
            CoordinateSystemDrawer.DrawYAxisLabels(canvas, data, unit, step);
        }

        public static void ClearCanvas(Canvas canvas) {
            for (int i = canvas.Children.Count; i >= 0; i--) {
                if (canvas.Children[i] is Line || canvas.Children[i] is Label) {
                    canvas.Children.RemoveAt(i);
                }
            }
        }

        private static void DrawBackgroundLines(Canvas canvas, CoordinateSystemData data, double unit, double step) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);

            Brush axisLineBrush = CoordinateSystemDrawer._axisLineBrush,
                  unitLineBrush = CoordinateSystemDrawer._unitLineBrush,
                  stepLineBrush = CoordinateSystemDrawer._stepLineBrush;

            double axisLineThickness = CoordinateSystemDrawer._axisLineThickness,
                   unitLineThickness = CoordinateSystemDrawer._unitLineThickness,
                   stepLineThickness = CoordinateSystemDrawer._stepLineThickness;

            int axisZ = CoordinateSystemDrawer._axisZ,
                unitZ = CoordinateSystemDrawer._unitZ,
                stepZ = CoordinateSystemDrawer._stepZ;

            #region Vertical Lines
            double x = data.MinX;
            if (data.MinX % step != 0)
                x = CoordinateConverter.Round(x, step);

            while (x < data.MaxX) {
                if (x <= data.MinX) {
                    x += step;
                    continue;
                }

                BackgroundLine temp = new BackgroundLine(new Vector(x, data.MinY),
                                                         new Vector(x, data.MaxY),
                                                         (x == 0) ? axisLineBrush : stepLineBrush,
                                                         (x == 0) ? axisLineThickness : stepLineThickness);

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

                Canvas.SetZIndex(line, (x == 0) ? axisZ : stepZ);

                canvas.Children.Add(line);

                x += step;
            }
            #endregion

            #region Horizontal Lines
            double y = data.MinY;
            if (data.MinY % step != 0)
                y = CoordinateConverter.Round(y, step);

            while (y < data.MaxY) {
                if (y <= data.MinY) {
                    y += step;
                    continue;
                }

                //double x1 = (y == 0) ? data.MinX : ((((int) y) == y) ? (-step * .25) : data.MinX);
                //double x2 = (y == 0) ? data.MaxX : ((((int) y) == y) ? ( step * .25) : data.MaxX);

                BackgroundLine temp = new BackgroundLine(/*canvasSize,*/
                                                         //data,
                                                         new Vector(data.MinX, y),
                                                         new Vector(data.MaxX, y),
                                                         (y == 0) ? axisLineBrush : stepLineBrush,
                                                         (y == 0) ? axisLineThickness : stepLineThickness);

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

                Canvas.SetZIndex(line, (y == 0) ? axisZ : stepZ);

                canvas.Children.Add(line);

                y += step;
            }
            #endregion

            #region axis labeling


            #endregion
        }
    }
}
