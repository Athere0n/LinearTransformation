using LinearTransformation.Model;
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

namespace LinearTransformation.Components {
    /// <summary>
    /// Interaction logic for CanvasVector.xaml
    /// </summary>
    public partial class CanvasVector: UserControl {

        public Size CanvasSize;
        private readonly double _degrees;
        private readonly double _arrowHeadLength;
        public readonly Vector _origin;
        public Vector _destination;

        public Brush VectorBrush;
        public double X { get => this._destination.X; set { this._destination.X = value; } }
        public double Y { get => this._destination.Y; set { this._destination.Y = value; } }

        public CoordinateSystemData Data { get; set; }

        public CanvasVector(Size canvasSize, Brush brush,
                            CoordinateSystemData coordinateSystemData,
                            Vector destination, Vector origin,
                            double degrees = 45, double arrowHeadLength = 15) {

            this.InitializeComponent();

            this.VectorBrush = brush;

            this.Data = coordinateSystemData;
            this._destination = destination;
            this._origin = origin;
            this._degrees = degrees;
            this._arrowHeadLength = arrowHeadLength;

            this.CanvasSize = canvasSize;
            this.MainLine.Stroke = this.VectorBrush;
            this.MainLine.StrokeThickness = 5;

            this.DirectionalLine1.Stroke = this.VectorBrush;
            this.DirectionalLine1.StrokeThickness = 5;

            this.DirectionalLine2.Stroke = this.VectorBrush;
            this.DirectionalLine2.StrokeThickness = 5;

            this.UpdateCoordinates();
        }

        public void UpdateCoordinates() {

            Vector originOnCanvas = CoordinateConverter.FromCoordinateToPoint(this.CanvasSize,
                                                                              this.Data,
                                                                              this._origin);
            Vector destinationOnCavnas = CoordinateConverter.FromCoordinateToPoint(this.CanvasSize,
                                                                                   this.Data,
                                                                                   this._destination);

            this.MainLine.X1 = originOnCanvas.X;
            this.MainLine.Y1 = originOnCanvas.Y;
            this.MainLine.X2 = destinationOnCavnas.X;
            this.MainLine.Y2 = destinationOnCavnas.Y;

            this.UpdateArrowCoordinates();

            this.UpdateToolTip();
        }

        public void UpdateBrush() {
            this.MainLine.Stroke = this.VectorBrush;

            this.DirectionalLine1.Stroke = this.VectorBrush;

            this.DirectionalLine2.Stroke = this.VectorBrush;
        }

        private void UpdateToolTip() {
            string x = Utility.GetDoubleAsStringWithDecimals(this._destination.X, (int) Properties.Settings.Default["AmountOfDecimals"]);
            string y = Utility.GetDoubleAsStringWithDecimals(this._destination.Y, (int) Properties.Settings.Default["AmountOfDecimals"]);
            this.VectorToolTip.Content = $"X: {x}\nY: {y}";
        }

        private void UpdateArrowCoordinates() {
            Vector p = this.GetDirectionPoint();
            Vector r1 = this.RotatePointAroundAnotherPointByDegrees(p, new Vector(this.MainLine.X2, this.MainLine.Y2),  this._degrees);
            Vector r2 = this.RotatePointAroundAnotherPointByDegrees(p, new Vector(this.MainLine.X2, this.MainLine.Y2), -this._degrees);

            this.DirectionalLine1.X1 = this.MainLine.X2;
            this.DirectionalLine1.Y1 = this.MainLine.Y2;
            this.DirectionalLine1.X2 = r1.X;
            this.DirectionalLine1.Y2 = r1.Y;

            this.DirectionalLine2.X1 = this.MainLine.X2;
            this.DirectionalLine2.Y1 = this.MainLine.Y2;
            this.DirectionalLine2.X2 = r2.X;
            this.DirectionalLine2.Y2 = r2.Y;
        }

        private Vector RotatePointAroundAnotherPointByDegrees(Vector p, Vector o, double degrees) {
            return this.RotatePointAroundAnotherPointByRadians(p, o, degrees * (Math.PI / 180));
        }

        private Vector RotatePointAroundAnotherPointByRadians(Vector p, Vector o, double radians) {
            return new Vector((int) Math.Round(Math.Cos(radians) * (p.X - o.X) - Math.Sin(radians) * (p.Y - o.Y) + o.X),
                              (int) Math.Round(Math.Sin(radians) * (p.X - o.X) + Math.Cos(radians) * (p.Y - o.Y) + o.Y));
        }

        private Vector GetDirectionPoint() {
            Vector vector = new Vector(this.MainLine.X2 - this.MainLine.X1,
                                       this.MainLine.Y2 - this.MainLine.Y1);
            Vector desiredVector = vector;
            desiredVector.Normalize();
            double desiredLength = this._arrowHeadLength;
            vector -= (desiredVector * desiredLength);

            return new Vector(Math.Round(this.MainLine.X1 + vector.X),
                              Math.Round(this.MainLine.Y1 + vector.Y));
        }

        private void Grid_ContextMenuOpening(object sender, ContextMenuEventArgs e) {

        }
    }
}
