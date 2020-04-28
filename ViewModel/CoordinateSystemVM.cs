using LinearTransformation.Components;
using LinearTransformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinearTransformation.ViewModel {
    public class CoordinateSystemVM {
        private CoordinateSystemData coordinateSystemData;
        private readonly Canvas _canvas;
        private List<BackgroundLine> _backgroundLines;

        public CoordinateSystemVM(Canvas canvas) {
            this._canvas = canvas;
            //this.InstantiateViewSettings();
            //this.InstantiateBackground();
        }

        public void Update() {
            this._canvas.Children.Clear();
            this.InstantiateViewSettings();
            this.InstantiateBackground();
        }

        private void InstantiateBackground() {
            this._backgroundLines = new List<BackgroundLine>();

            // Hexenwerk:

            this._backgroundLines.Add(new BackgroundLine(this._canvas,
                                                         this.coordinateSystemData,
                                                         new Vector(0, this.coordinateSystemData.MinY),
                                                         new Vector(0, this.coordinateSystemData.MaxY),
                                                         true));

            //for (double x = this.coordinateSystemData.MinX; x <= this.coordinateSystemData.MaxX; x++) {
            //    this._backgroundLines.Add(new BackgroundLine(this._canvas,
            //                                                 this.coordinateSystemData,
            //                                                 new Vector(x, this.coordinateSystemData.MinY),
            //                                                 new Vector(x, this.coordinateSystemData.MaxY),
            //                                                 (x == 0)));
            //}

            //for (double y = this.coordinateSystemData.MinY; y < this.coordinateSystemData.MaxY; y++) {
            //    this._backgroundLines.Add(new BackgroundLine(this._canvas,
            //                                                 this.coordinateSystemData,
            //                                                 new Vector(this.coordinateSystemData.MinX, y),
            //                                                 new Vector(this.coordinateSystemData.MaxX, y),
            //                                                 (y == 0)));
            //}

            this._backgroundLines.ForEach(x => this._canvas.Children.Add(x));
        }

        private void InstantiateViewSettings() {
            this.coordinateSystemData = new CoordinateSystemData {
                MinX = -3,
                MinY = -3,
                MaxX = 3,
                MaxY = 3,
            };
        }

    }
}
