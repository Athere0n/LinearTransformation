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
            this.PreviewKeyDown += new KeyEventHandler(this.CoordinateCanvas_KeyDown);

            this._vm = new CoordinateSystemVM(this.CoordinateCanvas);
            this.DataContext = this._vm;

            this.Loaded += delegate { this._vm.Update(); };
            this.CoordinateCanvas.SizeChanged += delegate { this._vm.Update(); };
        }

        private void CoordinateCanvas_KeyDown(object sender, KeyEventArgs e) {
            this._vm.Control_KeyboardMove(sender, e);
        }
    }
}
