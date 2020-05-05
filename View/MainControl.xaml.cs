using LinearTransformation.Components;
using LinearTransformation.Model;
using LinearTransformation.ViewModel;
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
    public partial class MainControl: UserControl {
        private readonly MainControlVM _vm;

        public MainControl() {
            this.InitializeComponent();
            this._vm = new MainControlVM(this);
            this.DataContext = this._vm;
        }

        public void DeleteVector(CanvasVector canvasVector) {
            this._vm.DeleteVector(canvasVector);
        }

        private void Button_Click_AddVector(object sender, RoutedEventArgs e) {
            this._vm.Button_Click_AddVector(sender, e);
        }

        private void Button_Click_Transform(object sender, RoutedEventArgs e) {
            this._vm.Button_Click_Transform(sender, e);
        }

        private void ToggleButtons_StateChanged(object sender, RoutedEventArgs e) {
            this._vm?.ToggleButtons_StateChanged(sender, e);
        }

        private void Canvas_MouseLeftButtonDown_SelectVectorColour(object sender, MouseButtonEventArgs e) {
            this._vm.Canvas_MouseLeftButtonDown_SelectVectorColour(sender, e);
        }

        private void Button_Click_ApplyChanges(object sender, RoutedEventArgs e) {
            this._vm.Button_Click_ApplyChanges(sender, e);

        }

        private void Button_Click_RevertChanges(object sender, RoutedEventArgs e) {
            this._vm.Button_Click_RevertChanges(sender, e);
        }
    }
}
