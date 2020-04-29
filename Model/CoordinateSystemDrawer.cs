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
        }

        private static void DrawAxisLabels(Canvas canvas, CoordinateSystemData data) {
            Size canvasSize = new Size(canvas.ActualWidth, canvas.ActualHeight);



            #region X Axis

            #endregion

            #region Y Axis

            #endregion
        }

        public static void ClearCanvas(Canvas canvas) {
            for (int i = canvas.Children.Count; i >= 0; i--) {
                if (canvas.Children[i] is Line || canvas.Children[i] is Label) {
                    canvas.Children.RemoveAt(i);
                }
            }
        }

        private static void DrawBackgroundLines(Canvas canvas, CoordinateSystemData data, double step = .5) {
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

                BackgroundLine temp = new BackgroundLine(/*canvasSize,*/
                                                         //data,
                                                         new Vector(x, data.MinY),
                                                         new Vector(x, data.MaxY),
                                                         (x == 0) ? axisLineBrush : ((((int) x) == x) ? unitLineBrush : stepLineBrush),
                                                         (x == 0) ? axisLineThickness : ((((int) x) == x) ? unitLineThickness : stepLineThickness));

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

                Canvas.SetZIndex(line, (x == 0) ? axisZ : ((((int) x) == x) ? unitZ : stepZ));

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
                                                         (y == 0) ? axisLineBrush : ((((int) y) == y) ? unitLineBrush : stepLineBrush),
                                                         (y == 0) ? axisLineThickness : ((((int) y) == y) ? unitLineThickness : stepLineThickness));

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

                Canvas.SetZIndex(line, (y == 0) ? axisZ : ((((int) y) == y) ? unitZ : stepZ));

                canvas.Children.Add(line);

                y += step;
            }
            #endregion

            #region axis labeling


            #endregion
        }
    }
}
