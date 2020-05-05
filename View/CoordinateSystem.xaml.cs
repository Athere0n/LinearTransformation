using LinearTransformation.Components;
using LinearTransformation.Model;
using LinearTransformation.ViewModel;
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

namespace LinearTransformation.View {
    /// <summary>
    /// Interaction logic for CoordinateSystem.xaml
    /// </summary>
    public partial class CoordinateSystem: UserControl {
        public readonly CoordinateSystemVM _coordinateSystemVM;
        public readonly MainControlVM _mainControlVM;

        //public CoordinateSystem() {
        //    this.InitializeComponent();

        //    this._coordinateSystemVM = new CoordinateSystemVM(this._mainControlVM, this.CoordinateCanvas);
        //    this.DataContext = this._coordinateSystemVM;

        //    this.Loaded += delegate { this._coordinateSystemVM.Update(); };
        //    this.CoordinateCanvas.SizeChanged += delegate { this._coordinateSystemVM.Update(); };
        //    this.CoordinateCanvas.Loaded += delegate { Keyboard.Focus(this.CoordinateCanvas); };
        //}

        public CoordinateSystem(MainControlVM mainControlVM, CoordinateSystemData data, List<CanvasVector> vectors = null) {
            this.InitializeComponent();

            this._mainControlVM = mainControlVM;
            this._coordinateSystemVM = new CoordinateSystemVM(this._mainControlVM, this.CoordinateCanvas, data, vectors);
            this.DataContext = this._coordinateSystemVM;

            this.Loaded += delegate { this._coordinateSystemVM.Update(); };
            this.CoordinateCanvas.SizeChanged += delegate { this._coordinateSystemVM.Update(); };
            this.CoordinateCanvas.Loaded += delegate { Keyboard.Focus(this.CoordinateCanvas); };
        }

        public CanvasVector AddVector(double x, double y, Brush b) {
            return this._coordinateSystemVM.AddVector(x, y, b);
        }

        public void DeleteVector(CanvasVector canvasVector) {
            this._coordinateSystemVM.DeleteVector(canvasVector);
        }
    }
}
