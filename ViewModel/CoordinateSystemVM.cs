using LinearTransformation.Components;
using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LinearTransformation.ViewModel {
    public class CoordinateSystemVM {
        private CoordinateSystemData coordinateSystemData;
        private readonly Canvas _canvas;

        public CoordinateSystemVM(Canvas canvas) {
            this._canvas = canvas;
        }

        public void Update() {
            this._canvas.Children.Clear();
            this.InstantiateViewSettings();
            this.InstantiateBackground();
            this.DrawTestingVectors();
        }

        private void DrawTestingVectors() {
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.DarkOrchid,
                                                       this.coordinateSystemData,
                                                       new Vector(2, 2),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.DarkSalmon,
                                                       this.coordinateSystemData,
                                                       new Vector(-3, -1),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.LimeGreen,
                                                       this.coordinateSystemData,
                                                       new Vector(5, -2),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.Red,
                                                       this.coordinateSystemData,
                                                       new Vector(-1, 1),
                                                       new Vector(0, 0)));
            this._canvas.Children.Add(new CanvasVector(new Size(this._canvas.ActualWidth, this._canvas.ActualHeight),
                                                       Brushes.DeepPink,
                                                       this.coordinateSystemData,
                                                       new Vector(1, 0),
                                                       new Vector(0, 0)));
        }

        private void InstantiateBackground() {
            CoordinateSystemDrawer.Draw(this._canvas, this.coordinateSystemData);
        }

        private void InstantiateViewSettings() {
            this.coordinateSystemData = new CoordinateSystemData {
                MinX = -2,
                MinY = -2,
                MaxX =  2,
                MaxY =  2,
                Unit =  1,
                Step =  0,
            };
        }

    }
}
