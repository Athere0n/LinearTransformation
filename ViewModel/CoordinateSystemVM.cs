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
        private CoordinateSystemData _data;
        private readonly Canvas _canvas;

        public CoordinateSystemVM(Canvas canvas) {
            this._canvas = canvas;
        }

        public void Update() {
            this._canvas.Children.Clear();
            this.InstantiateViewSettings();
            this.InstantiateBackground();
            this.DrawTestingVectors();

            Size canvasSize = new Size(this._canvas.ActualWidth, this._canvas.ActualHeight);
            Vector coordinate = CoordinateConverter.FromCoordinateToPoint(canvasSize,
                                                                          this._data,
                                                                          new Vector(1.4, 1.2));
            Vector point = CoordinateConverter.FromPointToCoordinate(canvasSize,
                                                                     this._data,
                                                                     coordinate);

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
            this._data = new CoordinateSystemData {
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
