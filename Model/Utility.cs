using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LinearTransformation.Model {
    public class Utility {

        #region Round
        public static double Round(double value, double step) {
            // TODO: adjust this algorithm

            if (((double) ((int) (value / step))) == value / step)
                return value;

            double a = RoundUp(value, step) - value;
            double b = value - RoundDown(value, step);

            if (b < a) {
                Console.WriteLine("rounded " + value + " down");
                return RoundDown(value, step);
            }
            //if (a < b)
            Console.WriteLine("rounded " + value + " up");
            return RoundUp(value, step);
            //throw new Exception("aaa");
        }
        public static double RoundUp(double value, double step) {

            if (((double) ((int) (value / step))) == value / step)
                return value;

            return (step - value % step) + value;
        }
        public static double RoundDown(double value, double step) {
            return value - value % step;
        }

        public static string GetDoubleAsStringWithDecimals(double value, int decimals) {
            // TODO:
            //return $"{value}";
            value = Math.Round(value, decimals, MidpointRounding.AwayFromZero);

            string temp = $"{value}";

            if (temp.Contains(',')) {
                var a = temp.Split(',');
                int l = a[1].Length;

                if (l < decimals) {
                    // Add more decimals in the form of 0s
                    for (int i = l; i < decimals; i++) {
                        temp += '0';
                    }
                } else if (l > decimals) {
                    // Cut away excess decimals
                    throw new Exception("We should never reach this one");
                }

            } else if (decimals > 0) {
                // Add more decimals in the form of 0s
                temp += ',';
                for (int i = 0; i < decimals; i++) {
                    temp += '0';
                }
            }

            return temp;
        }
        #endregion

        #region TextSize
        public static System.Windows.Size GetTextSize(string text, double fontSize, System.Windows.Media.FontFamily fontFamily) {
            // Create a temporary label with the given content
            Label label = new Label {
                Content = text,
                FontSize = fontSize,
                FontFamily = fontFamily,
            };

            // Resize the label based on its content
            label.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            label.Arrange(new Rect(label.DesiredSize));

            // Return the size
            return new System.Windows.Size(label.ActualWidth, label.ActualHeight);
        }
        #endregion

        #region Lerp
        public static double Lerp(double from, double to, double by) {
            return from + (to - from) * by;
        }
        public static Vector Lerp(Vector from, Vector to, double by) {
            return new Vector(Utility.Lerp(from.X, to.X, by),
                              Utility.Lerp(from.Y, to.Y, by));
        }
        #endregion

        #region Random
        public static Random Random = new Random();
        public static double GetRandomDoubleWithinRange(double min, double max) {
            return Utility.Random.NextDouble() * (max - min) + min;
        }
        public static System.Windows.Media.Brush GetRandomBrush() {
            return Utility.GetRandomBrushFromBrushes();
            //return Utility.GetRandomAntonBrush();
        }
        public static System.Windows.Media.Brush GetRandomBrushFromBrushes() {
            PropertyInfo[] properties = (typeof(System.Windows.Media.Brushes)).GetProperties();
            return (System.Windows.Media.Brush) properties[Utility.Random.Next(properties.Length)].GetValue(null, null);
        }
        public static System.Windows.Media.Brush GetRandomAntonBrush() {
            string[] antonColours = {
                                        "#374b19",
                                        "#4b2d19",
                                        "#758793",
                                        "#4c0000",
                                        "#ff7f7f",
                                        "#ff8000",
                                        "#00ffff",
                                        "#00ff00",
                                        "#7fbf7f",
                                        "#004000",
                                        "#4c4c00",
                                        "#666600",
                                        "#660066",
            };
            string chosenColour = antonColours[Utility.Random.Next(0, antonColours.Length - 1)];
            return new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color) System.Windows.Media.ColorConverter.ConvertFromString(chosenColour));
        }
        #endregion

        #region MessageBoxes
        public static void ShowError(Exception e) {
            MessageBox.Show(e.Message, "Storch", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region CustomColours within ColorDialog
        public static void SetCustomColours(System.Windows.Forms.ColorDialog colorDialog) {
            int[] tempColorArray = new int[colorDialog.CustomColors.Length];

            for (int i = 0; i < colorDialog.CustomColors.Length; i++) {
                tempColorArray[i] = ((int) Properties.Settings.Default[$"CustomColour{i + 1}"]);
            }

            colorDialog.CustomColors = tempColorArray;

            //colorDialog.CustomColors = tempColorArray;
            //for (int i = 0; i < colorDialog.CustomColors.Length; i++) {
            //    colorDialog.CustomColors[i] = ((int) Properties.Settings.Default[$"CustomColour{i + 1}"]);
            //}
        }
        public static void SaveCustomColours(System.Windows.Forms.ColorDialog colorDialog) {
            for (int i = 0; i < colorDialog.CustomColors.Length; i++) {
                Properties.Settings.Default[$"CustomColour{i + 1}"] = colorDialog.CustomColors[i];
            }
            Properties.Settings.Default.Save();
        }
        #endregion

        #region TextBoxTextSelection
        public static void TextBox_SelectAll(TextBox textBox) {
            textBox.Dispatcher.BeginInvoke(new Action(() => textBox.SelectAll()));
        }
        #endregion
    }
}
