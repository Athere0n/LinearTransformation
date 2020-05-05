using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LinearTransformation.Model {
    public struct CoordinateSystemData {
        private double _minX;
        private double _maxX;
        private double _minY;
        private double _maxY;
        private double _unitX;
        private double _unitY;
        private double _stepX;
        private double _stepY;

        public Vector IHat;
        public Vector JHat;

        public double MinX {
            get => this._minX;
            set {
                if (value >= this.MaxX)
                    throw new Exception("Invalid range");
                this._minX = value;
            }
        }
        public double MaxX {
            get => this._maxX;
            set {
                if (value <= this.MinX)
                    throw new Exception("Invalid range");
                this._maxX = value;
            }
        }
        public double MinY {
            get => this._minY;
            set {
                if (value >= this.MaxY)
                    throw new Exception("Invalid range");
                this._minY = value;
            }
        }
        public double MaxY {
            get => this._maxY;
            set {
                if (value <= this.MinY)
                    throw new Exception("Invalid range");
                this._maxY = value;
            }
        }
        public double UnitX {
            get => this._unitX;
            set {
                if (value <= 0)
                    throw new Exception("Value can not be smaller than 0");
                this._unitX = value;
            }
        }
        public double UnitY {
            get => this._unitY;
            set {
                if (value <= 0)
                    throw new Exception("Value can not be smaller than 0");
                this._unitY = value;
            }
        }
        public double StepX {
            get => this._stepX;
            set {
                if (value < 0)
                    throw new Exception("Value can not be smaller than 0");
                this._stepX = value;
            }
        }
        public double StepY {
            get => this._stepY;
            set {
                if (value < 0)
                    throw new Exception("Value can not be smaller than 0");
                this._stepY = value;
            }
        }

        public CoordinateSystemData(double minX, double maxX,
                                    double minY, double maxY,
                                    double unit, double step) {

            if (minX > maxX || minY > maxY || minX == maxX || minY == maxY) {
                throw new Exception("Invalid boundaries");
            }

            if (unit <= 0) {
                throw new Exception("Unit can not be smaller than or equal to 0");
            }

            if (step < 0) {
                throw new Exception("Step can not be smaller than 0");
            }

            this._minX  = minX;
            this._maxX  = maxX;
            this._minY  = minY;
            this._maxY  = maxY;
            this._unitX = unit;
            this._unitY = unit;
            this._stepX = step;
            this._stepY = step;

            this.IHat = new Vector(1, 0);
            this.JHat = new Vector(0, 1);
        }

        public CoordinateSystemData(double minX, double maxX,
                                    double minY, double maxY,
                                    double unitX, double unitY,
                                    double stepX, double stepY) {

            if (minX > maxX || minY > maxY || minX == maxX || minY == maxY) {
                throw new Exception("Invalid boundaries");
            }

            if (unitX <= 0 || unitY <= 0) {
                throw new Exception("Unit can not be smaller than or equal to 0");
            }

            if (stepX < 0 || stepY < 0) {
                throw new Exception("Step can not be smaller than 0");
            }

            this._minX = minX;
            this._maxX = maxX;
            this._minY = minY;
            this._maxY = maxY;
            this._unitX = unitX;
            this._unitY = unitY;
            this._stepX = stepX;
            this._stepY = stepY;

            this.IHat = new Vector(1, 0);
            this.JHat = new Vector(0, 1);
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

        internal void SetUnitAndStepDynamically(Size canvasSize) {
            // TODO: Implement this

            // UnitX


            // UnitY

            // Step
            //this.StepX = this.UnitX * .5;
            //this.StepY = this.UnitY * .5;
        }
    }
}
