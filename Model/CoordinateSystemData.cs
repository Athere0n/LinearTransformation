using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LinearTransformation.Model {
    public struct CoordinateSystemData {
        public double MinX, MaxX, MinY, MaxY, UnitX, UnitY, StepX, StepY;

        public CoordinateSystemData(double minX, double maxX, double minY, double maxY,
                                    double unit, double step) {

            if (minX > maxX || minY > maxY) {
                throw new Exception("Invalid boundaries");
            }

            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
            this.UnitX = unit;
            this.UnitY = unit;
            this.StepX = step;
            this.StepY = step;
        }

        public CoordinateSystemData(double minX, double maxX,
                                    double minY, double maxY,
                                    double unitX, double unitY,
                                    double stepX, double stepY) {

            if (minX > maxX || minY > maxY) {
                throw new Exception("Invalid boundaries");
            }

            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
            this.UnitX = unitX;
            this.UnitY = unitY;
            this.StepX = stepX;
            this.StepY = stepY;
        }

        private static double CalculateCellAmount(double min, double max, double unit) {
            // Calculate the amount of units which fit into the given range

            // Variable used to adjusting the cell amount based on the unit value
            double temp = ((unit < 1) ? (1 / unit * .5) : 1);

            // case 1: Min < 0 < Max
            if (min < 0 && 0 < max) {
                return (Math.Abs(min) + Math.Abs(max)) * temp;
            }

            // case 2: 0 < Min < Max
            if (0 < min) {
                return (max - min) * temp;
            }

            // case 3: Min < Max < 0
            if (0 > max) {
                return (Math.Abs(min) - Math.Abs(max)) * temp;
            }

            // case 4: (Min = 0) < Max
            if (min == 0) {
                return max * temp;
            }

            // case 5: Min < (Max = 0)
            if (max == 0) {
                return Math.Abs(min) * temp;
            }

            throw new Exception("invalid range");
        }

        public Size GetCellSize() {
            // Returns the cell size while considering the value for one unit
            return new Size {
                Width = CoordinateSystemData.CalculateCellAmount(this.MinX, this.MaxX, this.UnitX),
                Height = CoordinateSystemData.CalculateCellAmount(this.MinY, this.MaxY, this.UnitY),
            };
        }

        internal void SetUnitAndStepDynamically(Size size) {
            // TODO: Implement this
            //throw new NotImplementedException();
        }
    }
}
