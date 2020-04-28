using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinearTransformation.Components {
    /// <summary>
    /// Interaction logic for BackgroundLine.xaml
    /// </summary>
    public partial class BackgroundLine: UserControl {
        public BackgroundLine(Canvas canvas, CoordinateSystemData coordinateSystemData, Vector position1, Vector position2, bool isThick = false) {
            this.InitializeComponent();
            Vector pos1 = CoordinateConverter.FromCoordinateSystemPointToCanvasPoint(canvas, coordinateSystemData, position1);
            Vector pos2 = CoordinateConverter.FromCoordinateSystemPointToCanvasPoint(canvas, coordinateSystemData, position2);
            this.MainLine.X1 = pos1.X;
            this.MainLine.Y1 = pos1.Y;
            this.MainLine.X2 = pos2.X;
            this.MainLine.Y2 = pos2.Y;
            this.BorderBrush = (isThick) ? Brushes.MediumVioletRed : Brushes.LightGray;
            this.BorderThickness = new Thickness((isThick) ? 5 : 1);
        }

    }
}
