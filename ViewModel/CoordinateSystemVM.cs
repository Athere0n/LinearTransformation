using LinearTransformation.Components;
using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LinearTransformation.ViewModel {
    public class CoordinateSystemVM {
        private readonly Canvas _canvas;
        private CoordinateSystemData _data;

        private bool _isDragging;
        private Vector _mouseLocationWithinCanvas;

        private void Control_MouseLeftButtonDown(object sender, MouseEventArgs e) {
            this._isDragging = true;
            Point m = e.GetPosition(this._canvas);
            this._mouseLocationWithinCanvas = new Vector(m.X, m.Y);
            this._canvas.CaptureMouse();
        }
        private void Control_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            this._isDragging = false;
            this._canvas.ReleaseMouseCapture();
        }
        private void Control_MouseMove(object sender, MouseEventArgs e) {
            if (this._isDragging) {
                Size canvasSize = new Size(this._canvas.ActualWidth, this._canvas.ActualHeight);

                Point p = e.GetPosition(this._canvas);
                Vector m = new Vector(p.X, p.Y);

                Vector toMousePosition = CoordinateConverter.FromPointToCoordinate(canvasSize,
                                                                                   this._data,
                                                                                   m);

                Vector fromMousePosition = CoordinateConverter.FromPointToCoordinate(canvasSize,
                                                                                   this._data,
                                                                                   this._mouseLocationWithinCanvas);

                double scrollspeed = (this._data.UnitX + this._data.UnitY) / 2;
                Vector distance = (fromMousePosition - toMousePosition) * scrollspeed;


                this._data.MinX += distance.X;
                this._data.MaxX += distance.X;
                this._data.MinY += distance.Y;
                this._data.MaxY += distance.Y;

                this._mouseLocationWithinCanvas = m;

                this.Update();
            }
        }

        public void Control_KeyboardMove(object sender, KeyEventArgs e) {
            Key key = (Key) e.Key;

            bool canvasNeedsRedraw = false;


            if (key == Key.Up && !Keyboard.IsKeyDown(Key.Down)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinY += distance;
                this._data.MaxY += distance;
                canvasNeedsRedraw = true;
            }

            if (key == Key.Down && !Keyboard.IsKeyDown(Key.Up)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinY -= distance;
                this._data.MaxY -= distance;
                canvasNeedsRedraw = true;
            }

            if (key == Key.Left && !Keyboard.IsKeyDown(Key.Right)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinX -= distance;
                this._data.MaxX -= distance;
                canvasNeedsRedraw = true;
            }

            if (key == Key.Right && !Keyboard.IsKeyDown(Key.Left)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinX += distance;
                this._data.MaxX += distance;
                canvasNeedsRedraw = true;
            }

            if (canvasNeedsRedraw) {
                this.Update();
            }
        }

        public CoordinateSystemVM(Canvas canvas) {
            this._canvas = canvas;
            this.InstantiateViewSettings();

            // Adding mouse movement
            this._canvas.MouseLeftButtonDown += new MouseButtonEventHandler(this.Control_MouseLeftButtonDown);
            this._canvas.MouseLeftButtonUp += new MouseButtonEventHandler(this.Control_MouseLeftButtonUp);
            this._canvas.MouseMove += new MouseEventHandler(this.Control_MouseMove);

            // Adding keyboard movement
            //this._canvas.KeyDown += new KeyEventHandler(this.Control_KeyboardMove);

            // Adding scroll wheel zoom
            this._canvas.MouseWheel += new MouseWheelEventHandler(this.Control_MouseWheel);
        }

        private void Control_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (e.Delta > 0) {
                // Zoom in
                this._data.MinX *= .9;
                this._data.MaxX *= .9;
                this._data.MinY *= .9;
                this._data.MaxY *= .9;
                this._data.SetUnitAndStepDynamically(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight));
                this.Update();
            } else if (e.Delta < 0) {
                // Zoom out
                this._data.MinX *= 1.1;
                this._data.MaxX *= 1.1;
                this._data.MinY *= 1.1;
                this._data.MaxY *= 1.1;
                this._data.SetUnitAndStepDynamically(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight));
                this.Update();
            }
        }

        public void Update() {
            this._canvas.Children.Clear();
            this.InstantiateBackground();
            this.DrawTestingVectors();

            //Size canvasSize = new Size(this._canvas.ActualWidth, this._canvas.ActualHeight);
            //Vector coordinate = CoordinateConverter.FromCoordinateToPoint(canvasSize,
            //                                                              this._data,
            //                                                              new Vector(1.4, 1.2));
            //Vector point = CoordinateConverter.FromPointToCoordinate(canvasSize,
            //                                                         this._data,
            //                                                         coordinate);

        }

        private void DrawTestingVectors() {
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.DarkOrchid,
                                                       this._data,
                                                       new Vector(2, 2),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.DarkSalmon,
                                                       this._data,
                                                       new Vector(-3, -1),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.LimeGreen,
                                                       this._data,
                                                       new Vector(5, -2),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.Red,
                                                       this._data,
                                                       new Vector(-1, 1),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.DeepPink,
                                                       this._data,
                                                       new Vector(1, 0),
                                                       new Vector(0, 0)));
        }

        private void InstantiateBackground() {
            CoordinateSystemDrawer.Draw(this._canvas, this._data);
        }

        private void InstantiateViewSettings() {

            //this._data = new CoordinateSystemData(
            //    minX: -3,
            //    minY: -3,
            //    maxX:  3,
            //    maxY:  3,
            //    unit:  1,
            //    step: .5
            //);

            this._data = new CoordinateSystemData(
                minX: -3,
                minY: -6,
                maxX:  3,
                maxY:  6,
                unitX: 1,
                unitY: 2,
                stepX: 1,
                stepY: 2
            );
        }

    }
}
