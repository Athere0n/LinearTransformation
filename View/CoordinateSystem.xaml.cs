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
        public CoordinateSystem() {
            this.InitializeComponent();
            var vm = new CoordinateSystemVM(this.CoordinateCanvas);
            this.DataContext = vm;
            this.Loaded += delegate { vm.Update(); };
            this.CoordinateCanvas.SizeChanged += delegate { vm.Update(); };
        }
    }
}
