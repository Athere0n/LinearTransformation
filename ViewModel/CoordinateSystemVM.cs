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
        public List<CanvasVector> Vectors { get; set; }

        private readonly MainControlVM _mainControlVM;
        private readonly Canvas _canvas;
        public CoordinateSystemData _data;

        // movement Variables
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

                this._mainControlVM.UpdateWindowSettings(this._data);

                this.Update();
            }
        }

        public CanvasVector AddVector(double x, double y, Brush b) {
            CanvasVector canvasVector = new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                              b, this._data, new Vector(x, y), new Vector(0, 0));
            this.Vectors.Add(canvasVector);
            this.Update();
            return canvasVector;
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
                this._mainControlVM.UpdateWindowSettings(this._data);
                this.Update();
            }
        }
        private void Control_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (e.Delta > 0) {
                // Zoom in
                this._data.MinX *= .9;
                this._data.MaxX *= .9;
                this._data.MinY *= .9;
                this._data.MaxY *= .9;
                this._data.SetUnitAndStepDynamically(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight));
                this._mainControlVM.UpdateWindowSettings(this._data);
                this.Update();
            } else if (e.Delta < 0) {
                // Zoom out
                this._data.MinX *= 1.1;
                this._data.MaxX *= 1.1;
                this._data.MinY *= 1.1;
                this._data.MaxY *= 1.1;
                this._data.SetUnitAndStepDynamically(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight));
                this._mainControlVM.UpdateWindowSettings(this._data);
                this.Update();
            }
        }

        public CoordinateSystemVM(MainControlVM mainControlVM, Canvas canvas) {
            this._mainControlVM = mainControlVM;
            this._canvas = canvas;
            this.InstantiateViewSettings();
            this.AddMovementFunctionality();
            this.Vectors = new List<CanvasVector> {};
        }

        public CoordinateSystemVM(MainControlVM mainControlVM, Canvas canvas, CoordinateSystemData data, List<CanvasVector> vectors) {
            this._mainControlVM = mainControlVM;
            this._canvas = canvas;
            this._data = data;
            this.AddMovementFunctionality();
            if (vectors == null)
                this.Vectors = new List<CanvasVector>();
            else
                this.Vectors = vectors;
        }

        private void AddMovementFunctionality() {
            // Adding mouse movement
            this._canvas.MouseLeftButtonDown += new MouseButtonEventHandler(this.Control_MouseLeftButtonDown);
            this._canvas.MouseLeftButtonUp += new MouseButtonEventHandler(this.Control_MouseLeftButtonUp);
            this._canvas.MouseMove += new MouseEventHandler(this.Control_MouseMove);

            // Adding keyboard movement
            this._canvas.KeyDown += new KeyEventHandler(this.Control_KeyboardMove);

            // Adding scroll wheel zoom
            this._canvas.MouseWheel += new MouseWheelEventHandler(this.Control_MouseWheel);
        }

        public void Update() {

            bool showDynamicGrid = (bool) this._mainControlVM._mainControl.ToggleButton_DynamicGrid.IsChecked;
            bool showStaticGrid = (bool) this._mainControlVM._mainControl.ToggleButton_StaticGrid.IsChecked;
            bool showVectors = (bool) this._mainControlVM._mainControl.ToggleButton_Vectors.IsChecked;



            this._canvas.Children.Clear();
            
            if (showStaticGrid)
                this.InstantiateBackground();
            if (showVectors)
                this.InstantiateVectors();
        }

        public void DeleteVector(CanvasVector canvasVector) {
            this._canvas.Children.Remove(canvasVector);
            this.Vectors.Remove(canvasVector);
        }

        private void InstantiateVectors() {
            foreach (CanvasVector vector in this.Vectors) {
                vector.CanvasSize = new Size(this._canvas.ActualWidth, this._canvas.ActualHeight);
                vector.Data = this._data;
                vector.UpdateCoordinates();
                this._canvas.Children.Add(vector);
            }
        }

        private void InstantiateBackground() {
            CoordinateSystemDrawer.Draw(this._canvas, this._data);
        }

        private void InstantiateViewSettings() {

            this._data = new CoordinateSystemData {
                MinX = -3,
                MaxX = 3,
                MinY = -3,
                MaxY = 3,
                UnitX = 1,
                UnitY = 1,
                StepX = .5,
                StepY = .5,
            };

            //this._data = new CoordinateSystemData(
            //    minX: -4,
            //    minY: -4,
            //    maxX:  4,
            //    maxY:  4,
            //    unit:  1,
            //    step: .5
            //);
        }

    }
}
