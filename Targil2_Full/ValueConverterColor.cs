using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Targil2
{
    class ValueConverterColor : IValueConverter
    {
        public Brush NormalBrush { get; set; }

        public Brush HighlightBrush { get; set; }
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = null;
            if (value == null)
                return NormalBrush;

           int number = System.Convert.ToInt32(value.ToString());
            if (number>15)
            {
                brush= NormalBrush;
            }
            else if(number<=15)
            {
                brush = HighlightBrush;
            }
            return brush;
        }
         
 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
