using LinearTransformation.View;
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
    /// Interaction logic for VectorListItem.xaml
    /// </summary>
    public partial class VectorListItem: UserControl {

        private MainControl _mainControl;
        public CanvasVector _canvasVector;

        public VectorListItem(MainControl mainControl, CanvasVector canvasVector) {
            this.InitializeComponent();

            this._mainControl = mainControl;
            this._canvasVector = canvasVector;

            this.InputVectorColour.Background = this._canvasVector.VectorBrush;
            this.InputVectorX.Text = $"{this._canvasVector.X}";
            this.InputVectorY.Text = $"{this._canvasVector.Y}";
        }

        private void Canvas_MouseLeftButtonDown_SelectVectorColour(object sender, MouseButtonEventArgs e) {
            System.Windows.Media.Brush mediaBrush = ((Canvas) sender).Background;
            System.Drawing.SolidBrush drawingSolidBrush = new System.Drawing.SolidBrush(
                (System.Drawing.Color) new System.Drawing.ColorConverter().ConvertFromString(new BrushConverter().ConvertToString(mediaBrush)));
            //double a = drawingSolidBrush.Color.A;
            //double r = drawingSolidBrush.Color.R;
            //double g = drawingSolidBrush.Color.G;
            //double b = drawingSolidBrush.Color.B;

            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog {
                Color = drawingSolidBrush.Color,
            };
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                SolidColorBrush b = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A,
                                                                                  colorDialog.Color.R,
                                                                                  colorDialog.Color.G,
                                                                                  colorDialog.Color.B));

                ((Canvas) sender).Background = b;
                this._canvasVector.VectorBrush = b;

                this._canvasVector.UpdateBrush();

            }
        }

        private void InputVectorX_TextChanged(object sender, TextChangedEventArgs e) {
            if (!double.TryParse(this.InputVectorX.Text, out double x))
                return;
                //throw new Exception("Invalid X Value");

            if (double.IsNaN(x) || x == 0)
                return;
                //throw new Exception("Invalid X Value");

            this._canvasVector.X = x;
            this._canvasVector.UpdateCoordinates();
        }

        private void InputVectorY_TextChanged(object sender, TextChangedEventArgs e) {
            if (!double.TryParse(this.InputVectorY.Text, out double y))
                return;
                //throw new Exception("Invalid Y Value");
            if (double.IsNaN(y) || y == 0)
                return;
                //throw new Exception("Invalid Y Value");

            this._canvasVector.Y = y;
            this._canvasVector.UpdateCoordinates();
        }

        private void Button_Click_DeleteVector(object sender, RoutedEventArgs e) {
            // Delete CanvasVector from MainControl List and order a redraw
            this._mainControl.Vectors.Remove(this);
            this._mainControl.DeleteVector(this._canvasVector);
        }
    }
}
