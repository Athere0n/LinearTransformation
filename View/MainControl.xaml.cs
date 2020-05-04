using System;
using System.Collections.Generic;
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

        public MainControl() {
            this.InitializeComponent();
            this.DataContext = this;
            this._coordinateSystemControl = new CoordinateSystem();
        }

        public void Redraw() {
            
        }

        private void Button_Click_AddVector(object sender, RoutedEventArgs e) {

        }

        private void Button_Click_Transform(object sender, RoutedEventArgs e) {

        }

        private void ToggleButtons_StateChanged(object sender, RoutedEventArgs e) {

        }
    }
}
