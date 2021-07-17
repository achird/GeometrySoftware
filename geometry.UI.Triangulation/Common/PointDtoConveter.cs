using geometry.Core.Triangulation.Application.Use.Query;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace geometry.UI.Triangulation.Common
{
    public class PointDtoConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var point = value as PointDto;
            return new Point(point.X, point.Y);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw (new Exception());
        }
    }
}
