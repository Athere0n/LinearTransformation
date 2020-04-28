using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace LinearTransformation.Model {
    public static class CoordinateSystemDrawer {

        public static void Draw(Canvas canvas, CoordinateSystemData data) {
            CoordinateSystemDrawer.DrawBackgroundLines(data);

        }

        //private static double RoundToNextStep(double value, double step) {
        //    int l = step.ToString().Length;
        //    if (step.ToString().Contains(','))
        //        l--;
            
        //    double d = Math.Round(value, l);

        //    if (d % step == 0) {
        //        return d;
        //    }

        //    int i(int) (d / step)
        //}

        private static void DrawBackgroundLines(CoordinateSystemData data, double step = .2) {
            Brush lineBrush = Brushes.LightGray;


            // Vertical Lines
            double x = data.MinX;
            //if (data.MinX % step != 0)
            //    x = CoordinateSystemDrawer.RoundToNextStep(data.MinX, step);

            //for () {
            
            //}
        }
    }
}
