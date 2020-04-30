using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinearTransformation.Model {
    public class Utility {

        #region Round
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
        #endregion

        public static Size GetTextSize(string text, double fontSize) {
            // Create a temporary label with the given content
            Label label = new Label {
                Content = text,
                FontSize = fontSize,
            };

            // Resize the label based on its content
            label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            label.Arrange(new Rect(label.DesiredSize));

            // Return the size
            return new Size(label.ActualWidth, label.ActualHeight);
        }
    }
}
