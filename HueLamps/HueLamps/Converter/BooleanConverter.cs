using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HueLamps.Converter
{
	public class BooleanConverter : IValueConverter
	{
		//From bool to nullable
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool b = (bool)value;
			bool? be = b as bool?;
			return be;
		}

		//From nullable to bool
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			bool? b = (bool?)value;
			bool be = false;
			if (b.HasValue)
				be = (bool)b;

			return be;

		}
	}
}