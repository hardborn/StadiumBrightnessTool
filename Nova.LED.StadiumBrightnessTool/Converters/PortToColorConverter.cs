using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nova.LED.StadiumBrightnessTool.Converters
{
    public class PortToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte portIndex = System.Convert.ToByte(value);
            string colorStr = string.Empty;
            switch (portIndex)
            {
                case 1:
                    colorStr = "#FF1BA1E2";
                    break;
                case 2:
                    colorStr = "#FF319B00";
                    break;
                case 3:
                    colorStr = "#FFFEA200";
                    break;
                case 4:
                    colorStr = "#FF9374D3";
                    break;
                default:
                    break;
            }
            return colorStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
