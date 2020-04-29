using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinearTransformation.Model {
    public static class CoordinateConverter {
        public static Vector FromPointToCoordinate(Size canvasSize, CoordinateSystemData coordinateSystemData, Vector point) {
            double w = canvasSize.Width;
            double h = canvasSize.Height;

            throw new NotImplementedException();

        }

        private static double CalculatePointX(double min, double max, double coordinate) {
            // case 1: Min < 0 < Max
            if (min < 0 && 0 < max) {
                if (coordinate == 0) {
                    // Ursprung
                    return -min;
                } else if (coordinate > 0) {
                    //Ursprung + coordinate
                    return -min + coordinate;
                } else if (coordinate < 0) {
                    // Ursprung + (negative) coordinate
                    return -min + coordinate;
                }
            }

            // case 2: 0 < Min < Max
            if (0 < min) {
                if (coordinate == 0) {
                    // Ursprung
                    return -min;
                } else if (coordinate > 0) {
                    // Ursprung + coordinate
                    return -min + coordinate;
                } else if (coordinate < 0) {
                    // Ursprung + (negative) coordinate)
                    return -min + coordinate;
                }
            }

            // case 3: Min < Max < 0
            if (0 > max) {
                if (coordinate == 0) {
                    // Ursprung
                    return -min;
                } else if (coordinate > 0) {
                    // Ursprung + coordinate
                    return -min + coordinate;
                } else if (coordinate < 0) {
                    // Ursprung + (negative) coordinate
                    return -min + coordinate;
                }
            }

            // case 4: (Min = 0) < Max
            if (min == 0) {
                if (coordinate == 0) {
                    // Ursprung
                    return -min;
                } else if (coordinate > 0) {
                    // Ursprung + coordinate
                    return -min + coordinate;
                } else if (coordinate < 0) {
                    // Urpsrung + (negative coordinate)
                    return -min + coordinate;
                }
            }

            // case 5: Min < (Max = 0)
            if (max == 0) {
                if (coordinate == 0) {
                    // Ursprung
                    return -min;
                } else if (coordinate > 0) {
                    // Ursprung + coordinate
                    return -min + coordinate;
                } else if (coordinate < 0) {
                    // Ursprung + (negative) coordinate;
                    return -min + coordinate;
                }
            }

            throw new Exception("invalid range");
        }

        private static double CalculatePointY(double min, double max, double coordinate) {
            // case 1: Min < 0 < Max
            if (min < 0 && 0 < max) {
                if (coordinate == 0) {
                    // Ursprung
                    return max;
                } else if (coordinate > 0) {
                    //Ursprung - coordinate
                    return max - coordinate;
                } else if (coordinate < 0) {
                    // Ursprung - (negative) coordinate
                    return max - coordinate;
                }
            }

            // case 2: 0 < Min < Max
            if (0 < min) {
                if (coordinate == 0) {
                    // Ursprung
                    return max;
                } else if (coordinate > 0) {
                    // Ursprung - coordinate
                    return max - coordinate;
                } else if (coordinate < 0) {
                    // Ursprung - (negative) coordinate)
                    return max - coordinate;
                }
            }

            // TODO: CHECK THIS OUT - THIS ALGORITHM USES -MAX INSTEAD OF MAX
            // case 3: Min < Max < 0
            if (0 > max) {
                if (coordinate == 0) {
                    // Ursprung
                    return -max;
                } else if (coordinate > 0) {
                    // Ursprung - coordinate
                    return -max - coordinate;
                } else if (coordinate < 0) {
                    // Ursprung - (negative) coordinate
                    return -max - coordinate;
                }
            }

            // case 4: (Min=0) < Max
            if (min == 0) {
                if (coordinate == 0) {
                    // Ursprung
                    return max;
                } else if (coordinate > 0) {
                    // Ursprung - coordinate
                    return max - coordinate;
                } else if (coordinate < 0) {
                    // Urpsrung - (negative coordinate)
                    return max - coordinate;
                }
            }

            // case 5: Min < (Max = 0)
            if (max == 0) {
                if (coordinate == 0) {
                    // Ursprung
                    return max;
                } else if (coordinate > 0) {
                    // Ursprung - coordinate
                    return max - coordinate;
                } else if (coordinate < 0) {
                    // Ursprung + (negative) coordinate;
                    return max - coordinate;
                }
            }

            throw new Exception("invalid range");
        }

        public static Vector FromCoordinateToPoint(Size canvasSize, CoordinateSystemData coordinateSystemData, Vector coordinate) {
            double w = canvasSize.Width;
            double h = canvasSize.Height;

            if (h == 0 || w == 0 || double.IsNaN(w) || double.IsNaN(h)) {
                throw new Exception("Why is my canvas not working?");
            }

            Size cellSize = new Size {
                Width  = w / coordinateSystemData.GetCellSize().Width,
                Height = h / coordinateSystemData.GetCellSize().Height,
            };

            return new Vector(CalculatePointX(coordinateSystemData.MinX, coordinateSystemData.MaxX, coordinate.X) * cellSize.Width,
                              CalculatePointY(coordinateSystemData.MinY, coordinateSystemData.MaxY, coordinate.Y) * cellSize.Height);
        }

        public static double Round(double value, double step) {
            // TODO: adjust this algorithm

            if (((double) ((int) (value / step))) == value / step)
                return value;
            
            double a = RoundUp(value, step) - value;
            double b = value - RoundDown(value, step);

            if (b < a) {
                Console.WriteLine("rounded " + value + " down");
                return RoundDown(value, step);
            }
            //if (a < b)
            Console.WriteLine("rounded " + value + " up");
            return RoundUp(value, step);
            //throw new Exception("aaa");
        }

        public static double RoundUp(double value, double step) {

            if (((double) ((int) (value / step))) == value / step)
                return value;
            
            return (step - value % step) + value;
        }

        public static double RoundDown(double value, double step) {
            return value - value % step;
        }
    }
}
