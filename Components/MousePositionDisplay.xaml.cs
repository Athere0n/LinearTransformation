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
    /// Interaction logic for MousePositionDisplay.xaml
    /// </summary>
    public partial class MousePositionDisplay: UserControl {

        public MousePositionDisplay(Position position) {
            this.InitializeComponent();
            this.SetPosition(position);
        }

        public void SetLabelContent(Vector staticPosition, CoordinateSystemData dynamicData) {
            Vector dynamicPosition = Utility.FromStaticToDynamic(dynamicData.IHat, dynamicData.JHat, staticPosition);

            this.Label_StaticX.Content  = $"{staticPosition.X}";
            this.Label_StaticY.Content  = $"{staticPosition.Y}";
            this.Label_DynamicX.Content = $"{dynamicPosition.X}";
            this.Label_DynamicY.Content = $"{dynamicPosition.Y}";
        }

        private void SetPosition(Position position) {
            // TODO: 
            // Consider the following:
            //  "If you specify them, the attached properties Canvas.Top or Canvas.Left
            //   take priority over Canvas.Bottom or Canvas.Right properties."
            // Instead of allowing to call this method from outside just create a new instance every time

            switch (position) {
                case Position.TopLeft:
                    Canvas.SetLeft(this, 0);
                    Canvas.SetTop(this, 0);
                    break;
                case Position.TopRight:
                    Canvas.SetRight(this, 0);
                    Canvas.SetTop(this, 0);
                    break;
                case Position.BottomLeft:
                    Canvas.SetLeft(this, 0);
                    Canvas.SetBottom(this, 0);
                    break;
                case Position.BottomRight:
                    Canvas.SetRight(this, 0);
                    Canvas.SetBottom(this, 0);
                    break;
                default:
                    throw new Exception("Invalid position");
            }
        }

        public enum Position {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

    }
}
