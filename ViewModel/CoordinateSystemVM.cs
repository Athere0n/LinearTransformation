using LinearTransformation.Components;
using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LinearTransformation.ViewModel {
    public class CoordinateSystemVM {
        public List<CanvasVector> Vectors { get; set; }

        private readonly MainControlVM _mainControlVM;
        private readonly Canvas _canvas;
        public CoordinateSystemData _data;
        public CoordinateSystemData _dynamicData;

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

                this._dynamicData.MinX += distance.X;
                this._dynamicData.MaxX += distance.X;
                this._dynamicData.MinY += distance.Y;
                this._dynamicData.MaxY += distance.Y;

                this._mouseLocationWithinCanvas = m;

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
                this._dynamicData.MinX *= .9;
                this._dynamicData.MaxX *= .9;
                this._dynamicData.MinY *= .9;
                this._dynamicData.MaxY *= .9;
                this._data.SetUnitAndStepDynamically(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight));
                this._mainControlVM.UpdateWindowSettings(this._data);
                this.Update();
            } else if (e.Delta < 0) {
                // Zoom out
                this._data.MinX *= 1.1;
                this._data.MaxX *= 1.1;
                this._data.MinY *= 1.1;
                this._data.MaxY *= 1.1;
                this._dynamicData.MinX *= 1.1;
                this._dynamicData.MaxX *= 1.1;
                this._dynamicData.MinY *= 1.1;
                this._dynamicData.MaxY *= 1.1;
                this._data.SetUnitAndStepDynamically(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight));
                this._mainControlVM.UpdateWindowSettings(this._data);
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
                this._dynamicData.MinY += distance;
                this._dynamicData.MaxY += distance;
                canvasNeedsRedraw = true;
            }

            if (key == Key.Down && !Keyboard.IsKeyDown(Key.Up)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinY -= distance;
                this._data.MaxY -= distance;
                this._dynamicData.MinY -= distance;
                this._dynamicData.MaxY -= distance;
                canvasNeedsRedraw = true;
            }

            if (key == Key.Left && !Keyboard.IsKeyDown(Key.Right)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinX -= distance;
                this._data.MaxX -= distance;
                this._dynamicData.MinX -= distance;
                this._dynamicData.MaxX -= distance;
                canvasNeedsRedraw = true;
            }

            if (key == Key.Right && !Keyboard.IsKeyDown(Key.Left)) {
                double distance = (this._data.StepY == 0) ? this._data.UnitY
                                                          : this._data.StepY;
                this._data.MinX += distance;
                this._data.MaxX += distance;
                this._dynamicData.MinX += distance;
                this._dynamicData.MaxX += distance;
                canvasNeedsRedraw = true;
            }

            if (canvasNeedsRedraw) {
                this._mainControlVM.UpdateWindowSettings(this._data);
                this.Update();
            }
        }

        public CoordinateSystemVM(MainControlVM mainControlVM, Canvas canvas) {
            this._mainControlVM = mainControlVM;
            this._canvas = canvas;
            this.InstantiateViewSettings();
            this.AddMovementFunctionality();
            this.Vectors = new List<CanvasVector>();
        }

        public CoordinateSystemVM(MainControlVM mainControlVM, Canvas canvas, CoordinateSystemData data, List<CanvasVector> vectors) {
            this._mainControlVM = mainControlVM;
            this._canvas = canvas;
            this._data = data;
            this._dynamicData = data;
            this.AddMovementFunctionality();
            if (vectors == null) {
                this.Vectors = new List<CanvasVector>();
            } else {
                this.Vectors = vectors;
            }
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
            bool showStaticGrid = (bool) this._mainControlVM._mainControl.ToggleButton_StaticGrid.IsChecked;
            bool showVectors = (bool) this._mainControlVM._mainControl.ToggleButton_Vectors.IsChecked;
            bool showDynamicGrid = (bool) this._mainControlVM._mainControl.ToggleButton_DynamicGrid.IsChecked;


            this._canvas.Children.Clear();

            if (showStaticGrid)
                CoordinateSystemDrawer.Draw(this._canvas, this._data);
            if (showDynamicGrid)
                CoordinateSystemDrawer.Draw(this._canvas, this._dynamicData);
            if (showVectors)
                this.InstantiateVectors(this._dynamicData, this.Vectors);


        }

        public CanvasVector AddVector(double x, double y, Brush b) {
            CanvasVector canvasVector = new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                              b, this._data, new Vector(x, y), new Vector(0, 0));
            this.Vectors.Add(canvasVector);
            this.Update();
            return canvasVector;
        }

        public void DeleteVector(CanvasVector canvasVector) {
            this._canvas.Children.Remove(canvasVector);
            this.Vectors.Remove(canvasVector);
        }

        public void NewTransformation(Vector iHat, Vector jHat) {
            //this._dynamicData = this._data;
            //this._dynamicData.IHat = iHat;
            //this._dynamicData.JHat = jHat;

            // Start the transformation
            this.StartAnimation(iHat, jHat, 5);
            //this.Update();
        }

        private double MoveTowards(double from, double to, double step) {
            if (from < to)
                return from + step;

            if (from > to)
                return from - step;

            return from;

        }

        private Task StartAnimation(Vector iHat, Vector jHat, int duration, int fps = 60) {
            return Task.Run(() => Application.Current.Dispatcher.Invoke(async () => {
                //this._dynamicData.IHat.X
                double iHatStepX = /*Math.Abs*/(iHat.X - this._dynamicData.IHat.X ) / duration / fps;
                double iHatStepY = /*Math.Abs*/(iHat.Y - this._dynamicData.IHat.Y ) / duration / fps;
                double jHatStepX = /*Math.Abs*/(jHat.X - this._dynamicData.JHat.X ) / duration / fps;
                double jHatStepY = /*Math.Abs*/(jHat.Y - this._dynamicData.JHat.Y ) / duration / fps;


                //while (iHat != this._dynamicData.IHat && jHat != this._dynamicData.JHat) {
                for (int i = 0; i < (duration * 1000) - (1000 / fps); i += (1000) / fps) {
                    // Set iHat and jHat accordingly
                    this._dynamicData.IHat.X = this._dynamicData.IHat.X + iHatStepX /*this.MoveTowards(iHat.X, this._dynamicData.IHat.X, iHatStepX)*/;
                    this._dynamicData.IHat.Y = this._dynamicData.IHat.Y + iHatStepY /*this.MoveTowards(iHat.Y, this._dynamicData.IHat.Y, iHatStepY)*/;
                    this._dynamicData.JHat.X = this._dynamicData.JHat.X + jHatStepX /*this.MoveTowards(jHat.X, this._dynamicData.JHat.X, jHatStepX)*/;
                    this._dynamicData.JHat.Y = this._dynamicData.JHat.Y + jHatStepY /*this.MoveTowards(jHat.Y, this._dynamicData.JHat.Y, jHatStepY)*/;

                    // Wait
                    await Task.Delay((1000 / fps));
                    // Update UI
                    this.Update();
                    Application.Current.Dispatcher.Invoke(delegate { }, System.Windows.Threading.DispatcherPriority.Render);

                }

                // Just set them directly afterwards to compensate for small differences
                this._dynamicData.IHat = iHat;
                this._dynamicData.JHat = jHat;


                // Wait
                //await Task.Delay((1000 / fps));

                // Update UI
                this.Update();
                Application.Current.Dispatcher.Invoke(delegate { }, System.Windows.Threading.DispatcherPriority.Render);
            }));
        }


        private void InstantiateVectors(CoordinateSystemData data, List<CanvasVector> vectors) {
            foreach (CanvasVector vector in vectors) {
                vector.CanvasSize = new Size(this._canvas.ActualWidth, this._canvas.ActualHeight);
                vector.Data = data;
                vector.UpdateCoordinates();
                this._canvas.Children.Add(vector);
            }
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
