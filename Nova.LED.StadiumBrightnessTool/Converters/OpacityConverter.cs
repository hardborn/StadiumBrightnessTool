using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nova.LED.StadiumBrightnessTool.Converters
{
    public class OpacityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            int brightnessValue = System.Convert.ToInt32(value);

            return 0.1 + (1.0 - 0.1) / 100d * (double)brightnessValue;
        }

        public object ConvertBack(object value, Type targetType,
               object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
