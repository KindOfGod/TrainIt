﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace TrainIt.Helper
{
    public class DoubleToBrushConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = 125 - (int)((double)value * 25);

            if (num < 0)
                num = 0;

            Brush brush = new SolidColorBrush(GetBlendedColor(num));

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Color GetBlendedColor(int percentage)
        {
            if (percentage < 50)
                return Interpolate(Color.FromRgb(213, 92, 52), Color.FromRgb(254, 250, 110), percentage / 50.0);
            return Interpolate(Color.FromRgb(254, 250, 110), Color.FromRgb(56, 158, 46), (percentage - 50) / 50.0);
        }

        private Color Interpolate(Color color1, Color color2, double fraction)
        {
            double r = Interpolate(color1.R, color2.R, fraction);
            double g = Interpolate(color1.G, color2.G, fraction);
            double b = Interpolate(color1.B, color2.B, fraction);
            return Color.FromRgb((byte)Math.Round(r), (byte)Math.Round(g), (byte)Math.Round(b));
        }

        private double Interpolate(double d1, double d2, double fraction)
        {
            return d1 + (d2 - d1) * fraction;
        }
        #endregion
    }
}
