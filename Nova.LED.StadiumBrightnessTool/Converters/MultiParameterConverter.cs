using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nova.LED.StadiumBrightnessTool.Converters
{
    public class MultiParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double brightnessValue = System.Convert.ToDouble(values[0]);
            double redValue = System.Convert.ToDouble(values[1]);
            double greenValue = System.Convert.ToDouble(values[2]);
            double blueValue = System.Convert.ToDouble(values[3]);

            return new double[] { brightnessValue,redValue, greenValue, blueValue };

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
