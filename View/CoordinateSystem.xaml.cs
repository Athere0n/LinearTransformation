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
        private readonly CoordinateSystemVM _vm;

        public CoordinateSystem() {
            this.InitializeComponent();
            
            this._vm = new CoordinateSystemVM(this.CoordinateCanvas);
            this.DataContext = this._vm;

            this.Loaded                       += delegate { this._vm.Update(); };
            this.CoordinateCanvas.SizeChanged += delegate { this._vm.Update(); };
            this.CoordinateCanvas.Loaded      += delegate { Keyboard.Focus(this.CoordinateCanvas); };
        }

        public CoordinateSystem(CoordinateSystemData data, List<CanvasVector> vectors = null) {
            this.InitializeComponent();

            this._vm = new CoordinateSystemVM(this.CoordinateCanvas, data, vectors);
            this.DataContext = this._vm;

            this.Loaded += delegate { this._vm.Update(); };
            this.CoordinateCanvas.SizeChanged += delegate { this._vm.Update(); };
            this.CoordinateCanvas.Loaded += delegate { Keyboard.Focus(this.CoordinateCanvas); };
        }
    }
}
