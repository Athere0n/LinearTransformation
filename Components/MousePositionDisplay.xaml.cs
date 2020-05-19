using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
            Vector dynamicPosition = CoordinateConverter.FromStaticToDynamic(dynamicData.IHat, dynamicData.JHat, staticPosition);

            this.Label_StaticX.Content  = $"{Utility.GetDoubleAsStringWithDecimals(staticPosition.X, (int) Properties.Settings.Default["AmountOfDecimals"])}";
            this.Label_StaticY.Content  = $"{Utility.GetDoubleAsStringWithDecimals(staticPosition.Y, (int) Properties.Settings.Default["AmountOfDecimals"])}";
            this.Label_DynamicX.Content = $"{Utility.GetDoubleAsStringWithDecimals(dynamicPosition.X, (int) Properties.Settings.Default["AmountOfDecimals"])}";
            this.Label_DynamicY.Content = $"{Utility.GetDoubleAsStringWithDecimals(dynamicPosition.Y, (int) Properties.Settings.Default["AmountOfDecimals"])}";
        }

        private void SetPosition(Position position) {
            // TODO: 
            // Consider the following:
            //  "If you specify them, the attached properties Canvas.Top or Canvas.Left
            //   take priority over Canvas.Bottom or Canvas.Right properties."
            // Instead of allowing to call this method from outside just create a new instance every time

            //Size headerSize = Utility.GetTextSize(this.HeaderLabel.Content.ToString(), this.HeaderLabel.FontSize, this.HeaderLabel.FontFamily);

            double halfHeight = 0/*headerSize.Height * .5*/;


            switch (position) {
                case Position.TopLeft:
                    Canvas.SetLeft(this, halfHeight);
                    Canvas.SetTop(this, 0);
                    break;
                case Position.TopRight:
                    Canvas.SetRight(this, halfHeight);
                    Canvas.SetTop(this, 0);
                    break;
                case Position.BottomLeft:
                    Canvas.SetLeft(this, halfHeight);
                    Canvas.SetBottom(this, halfHeight);
                    break;
                case Position.BottomRight:
                    Canvas.SetRight(this, halfHeight);
                    Canvas.SetBottom(this, halfHeight);
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
