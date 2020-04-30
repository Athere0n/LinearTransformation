using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinearTransformation.Model {
    public class Utility {

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
