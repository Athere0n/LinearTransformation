using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LinearTransformation.Model {
    public struct CoordinateSystemData {
        public double MinX, MaxX, MinY, MaxY;

        public CoordinateSystemData(double minX, double maxX, double minY, double maxY) {
            if (minX > maxX || minY > maxY) {
                throw new Exception("Invalid boundaries");
            }

            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
        }

        public Size GetCellSize() {
            // TODO: MAYBE ADD 1 DEPENDING ON WHETHER 0 is visible
            return new Size {
                Width = Math.Abs(this.MinX) + Math.Abs(this.MaxX),
                Height = Math.Abs(this.MinY) + Math.Abs(this.MaxY),
            };
        }
    }
}
