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

        //private readonly CoordinateSystemData _coordinateSystemData;
        //private readonly Size _canvasSize;

        public BackgroundLine(/*Size canvasSize, CoordinateSystemData coordinateSystemData, */Vector pos1, Vector pos2, Brush brush, double thickness) {
            //this._canvasSize = canvasSize;
            //this._coordinateSystemData = coordinateSystemData;
            this.Pos1 = pos1;
            this.Pos2 = pos2;
            this.LineBrush = brush;
            this.LineThickness = thickness;
        }

    }
}
