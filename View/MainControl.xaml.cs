using LinearTransformation.Components;
using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace LinearTransformation.View {
    /// <summary>
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl: UserControl, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string name = "")
               => this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));

        private UserControl _coordinateSystemControl;

        public UserControl CoordinateSystemControl {
            get { return this._coordinateSystemControl; }
            set {
                this._coordinateSystemControl = value;
                this.RaisePropertyChanged();
            }
        }

        public ObservableCollection<VectorListItem> Vectors { get; set; } = new ObservableCollection<VectorListItem>();

        public MainControl() {
            this.InitializeComponent();
            this.DataContext = this;

            CoordinateSystemData data = new CoordinateSystemData(-3, 3, -3, 3, 1, .5);
            this._coordinateSystemControl = new CoordinateSystem(data);

            this.InputMinX.Text  = $"{data.MinX}";
            this.InputMaxX.Text  = $"{data.MaxX}";
            this.InputMinY.Text  = $"{data.MinY}";
            this.InputMaxY.Text  = $"{data.MaxY}";
            this.InputUnitX.Text = $"{data.UnitX}";
            this.InputUnitY.Text = $"{data.UnitY}";
            this.InputStepX.Text = $"{data.StepY}";
            this.InputStepY.Text = $"{data.StepY}";

        }

        public void DeleteVector(CanvasVector canvasVector) {
            //this.Vectors.Select(x => x._canvasVector);
            ((CoordinateSystem) this.CoordinateSystemControl).DeleteVector(canvasVector);
        }

        private void Button_Click_AddVector(object sender, RoutedEventArgs e) {
            if (!double.TryParse(this.InputVectorX.Text, out double x))
                throw new Exception("Invalid X Value");

            if (!double.TryParse(this.InputVectorY.Text, out double y))
                throw new Exception("Invalid Y Value");

            if (double.IsNaN(x) || x == 0)
                throw new Exception("Invalid X Value");

            if (double.IsNaN(y) || y == 0)
                throw new Exception("Invalid Y Value");

            Brush b = this.InputVectorColour.Background;

            CanvasVector canvasVector = ((CoordinateSystem) this._coordinateSystemControl).AddVector(x, y, b);
            this.Vectors.Add(new VectorListItem(this, canvasVector));

            this.InputVectorX.Text = "";
            this.InputVectorY.Text = "";
        }

        private void Button_Click_Transform(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void ToggleButtons_StateChanged(object sender, RoutedEventArgs e) {
            //throw new NotImplementedException();
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
                ((Canvas) sender).Background = new SolidColorBrush(Color.FromArgb(colorDialog.Color.A,
                                                                                  colorDialog.Color.R,
                                                                                  colorDialog.Color.G,
                                                                                  colorDialog.Color.B));
            }
        }

        private void Button_Click_ApplyChanges(object sender, RoutedEventArgs e) {
            // TODO:
            if (!double.TryParse(this.InputMinX.Text, out double minX))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputMaxX.Text, out double maxX))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputMinY.Text, out double minY))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputMaxY.Text, out double maxY))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputUnitX.Text, out double unitX))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputUnitY.Text, out double unitY))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputStepX.Text, out double stepX))
                throw new Exception("Invalid Value");
            if (!double.TryParse(this.InputStepY.Text, out double stepY))
                throw new Exception("Invalid Value");

            var temp = (CoordinateSystem) this.CoordinateSystemControl;
            temp._vm._data = new CoordinateSystemData(minX, maxX, minY, maxY, unitX, unitY, stepX, stepY);
            temp._vm.Update();
            //this.CoordinateSystemControl = new CoordinateSystem(data, ((CoordinateSystem) this.CoordinateSystemControl)._vm.Vectors);

        }

        private void Button_Click_RevertChanges(object sender, RoutedEventArgs e) {
            var temp = (CoordinateSystem) this.CoordinateSystemControl;
            this.InputMinX.Text = $"{temp._vm._data.MinX}";
            this.InputMaxX.Text = $"{temp._vm._data.MaxX}";
            this.InputMinY.Text = $"{temp._vm._data.MinY}";
            this.InputMaxY.Text = $"{temp._vm._data.MaxY}";
            this.InputUnitX.Text = $"{temp._vm._data.UnitX}";
            this.InputUnitY.Text = $"{temp._vm._data.UnitY}";
            this.InputStepX.Text = $"{temp._vm._data.StepY}";
            this.InputStepY.Text = $"{temp._vm._data.StepY}";
        }
    }
}
