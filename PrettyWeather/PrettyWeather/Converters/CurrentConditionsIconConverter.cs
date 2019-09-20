using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrettyWeather.Converters
{
    public class CurrentConditionsIconConverter : IMarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // these hex codes coorespond to font aweosome solid codes
            if (value is string condition)
            {
                if (condition == "clear-day")
                    return ((char)0xf185).ToString();

                if (condition == "clear-night")
                    return ((char)0xf186).ToString();

                if (condition == "rain")
                    return ((char)0xf740).ToString();

                if (condition == "snow")
                    return ((char)0xf73b).ToString();

                if (condition == "sleet")
                    return ((char)0xf7ae).ToString();

                if (condition == "wind")
                    return ((char)0xf72e).ToString();

                if (condition == "fog")
                    return ((char)0xf75f).ToString();

                if (condition == "cloudy")
                    return ((char)0xf0c2).ToString();

                if (condition == "partly-cloudy-day")
                    return ((char)0xf6c4).ToString();

                if (condition == "partly-cloudy-night")
                    return ((char)0xf6c3).ToString();
                       
            }

            return ((char)0xf0e7).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
