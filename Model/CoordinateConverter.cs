using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinearTransformation.Model {
    public static class CoordinateConverter {
        public static Vector FromCanvasPointToCoordinateSystemPoint(Canvas canvas, CoordinateSystemData coordinateSystemData, Vector point) {
            double w = canvas.Width;
            double h = canvas.Height;

            throw new NotImplementedException();

        }

        public static Vector FromCoordinateSystemPointToCanvasPoint(Canvas canvas, CoordinateSystemData coordinateSystemData, Vector point) {
            #region
            //if (point.X > 0) {
            //    double x = point.X + Math.Abs(coordinateSystemData.MinX) + 1; // +1 because of y = 0
            //    if (point.Y > 0) {
            //        // Quadrant 1 (oben rechts)
            //        return new Vector(x, Math.Abs(coordinateSystemData.MaxY) - point.Y);
            //    } else if (point.Y < 0) {
            //        // Quadrant 4 (unten rechts)
            //        return new Vector(x, point.Y + Math.Abs(coordinateSystemData.MinY) + 1); // +1 because of y = 0
            //    } else {
            //        // ON Y == 0
            //        return new Vector(x, Math.Abs(coordinateSystemData.MinY + 1)); // +1 because of y = 0
            //    }
            //} else if (point.X < 0) {
            //    double x = Math.Abs(coordinateSystemData.MinX) + point.X;
            //    if (point.Y > 0) {
            //        // Quadrant 2 (oben links) (-x|+y)
            //        return new Vector(x, Math.Abs(coordinateSystemData.MaxY) - point.Y);
            //    } else if (point.Y < 0) {
            //        // Quadrant 3 (unten links)
            //        return new Vector(x, point.Y + Math.Abs(coordinateSystemData.MinY) + 1); // +1 because of y = 0
            //    } else {
            //        return new Vector(x, Math.Abs(coordinateSystemData.MinY + 1)); // +1 because of y = 0
            //    }
            //} else {
            //    // x == 0
            //    double x = Math.Abs(coordinateSystemData.MinX) + 1;
            //    if (point.Y > 0) {
            //        // Quadrant 2 (oben links) (-x|+y)
            //        return new Vector(x, Math.Abs(coordinateSystemData.MaxY) - point.Y);
            //    } else if (point.Y < 0) {
            //        // Quadrant 3 (unten links)
            //        return new Vector(x, point.Y + Math.Abs(coordinateSystemData.MinY) + 1); // +1 because of y = 0
            //    } else {
            //        return new Vector(x, Math.Abs(coordinateSystemData.MinY + 1)); // +1 because of y = 0
            //    }
            //}
            #endregion

            // TODO: ANPASSEN FÜR ALLE SONDERFÄLLE

            // Zusammengefasst:
            Vector position = new Vector();
            if (point.X < 0) {
                position.X = point.X + Math.Abs(coordinateSystemData.MinX);
                if (coordinateSystemData.MinX > 0) {
                    position.X = point.X - Math.Abs(coordinateSystemData.MinX) - 1;
                }
            } else {
                position.X = point.X + Math.Abs(coordinateSystemData.MinX);
                if (coordinateSystemData.MinX > 0) {
                    // Add 1 to compensate for x = 0
                    position.X++;
                }
            }
            if (point.Y > 0) {
                position.Y = Math.Abs(coordinateSystemData.MinY) - point.Y;
                if (coordinateSystemData.MaxY < 0) {

                }
            } else {
                position.Y = Math.Abs(coordinateSystemData.MinY) + Math.Abs(point.Y);
            }

            double w = canvas.Width;
            double h = canvas.Height;

            if (h == 0 || w == 0 || double.IsNaN(w) || double.IsNaN(h)) {
                throw new Exception("Why is my canvas not working?");
            }

            Size cellSize = new Size{
                Width = w / coordinateSystemData.GetCellSize().Width,
                Height = h / coordinateSystemData.GetCellSize().Height,
            };

            position.X *= cellSize.Width;
            position.Y *= cellSize.Height;

            return position;
        }
    }
}
