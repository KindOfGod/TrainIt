using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TrainIt.Converter
{
    public class DateToBrushConverter : IValueConverter
    {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //UEXC: NullException
            var num = 100 - (int)(((DateTime.Now - (DateTime)value).TotalDays) * 3.3);

            if (num < 0)
                num = 0;

            Brush brush = new SolidColorBrush(GetBlendedColor(num));

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static Color GetBlendedColor(int percentage)
        {
            if (percentage < 50)
                return Interpolate(Color.FromRgb(201, 80, 80), Color.FromRgb(193, 119, 222), percentage / 50.0);
            return Interpolate(Color.FromRgb(193, 119, 222), Color.FromRgb(65, 132, 201), (percentage - 50) / 50.0);
        }

        private static Color Interpolate(Color color1, Color color2, double fraction)
        {
            var r = Interpolate(color1.R, color2.R, fraction);
            var g = Interpolate(color1.G, color2.G, fraction);
            var b = Interpolate(color1.B, color2.B, fraction);
            return Color.FromRgb((byte)Math.Round(r), (byte)Math.Round(g), (byte)Math.Round(b));
        }

        private static double Interpolate(double d1, double d2, double fraction)
        {
            return d1 + (d2 - d1) * fraction;
        }
        #endregion
    }
}
