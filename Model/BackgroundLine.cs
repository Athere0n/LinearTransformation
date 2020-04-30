using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LinearTransformation.Model {
    public class BackgroundLine {
        public readonly Vector Pos1, Pos2;
        public readonly Brush LineBrush;
        public readonly double LineThickness;

        public BackgroundLine(Vector pos1, Vector pos2, Brush brush, double thickness) {
            this.Pos1 = pos1;
            this.Pos2 = pos2;
            this.LineBrush = brush;
            this.LineThickness = thickness;
        }

    }
}
