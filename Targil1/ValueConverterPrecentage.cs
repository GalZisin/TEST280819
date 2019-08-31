using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Targil1
{
    class ValueConverterPrecentage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double sliderSize = System.Convert.ToDouble(value);
            if (sliderSize < 25 * 100 / 100)
            {
                return "SMALL";
            }
            if (sliderSize < 50 * 100 / 100 && sliderSize > 25 * 100 / 100)
            {
                return "MEDIUM";
            }
            if (sliderSize < 75 * 100 / 100 && sliderSize > 50 * 100 / 100)
            {
                return "LARGE";
            }
            if (sliderSize > 75 * 100 / 100)
            {
                return "EXTRA LARGE";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
