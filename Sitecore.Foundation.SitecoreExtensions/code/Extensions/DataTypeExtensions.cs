using System.Text.RegularExpressions;

namespace System
{
	public static class DataTypeExtensions
	{
		/// <summary>Gets the content of the clean ritch text.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The clean text content from a Ritch-Text Html content, as a string with all Html stripped out</returns>
		public static string GetCleanRitchTextContent(this string value)
		{
			return Regex.Replace(value.Trim(), @"\r\n?|\n", string.Empty).Replace("&nbsp;", string.Empty).Replace(@"<br/>", string.Empty).Replace(@"<br />", string.Empty).Trim();
		}

		/// <summary>Gets the string unique identifier.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The Guid value or Guid.Empty value from a string object, as a Guid object value</returns>
		public static Guid GetStringGuid(this string value)
		{
			var guidId = Guid.Empty;
			Guid.TryParse(value, out guidId);
			return guidId;
		}

		/// <summary>Gets the unique identifier string.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The Guid value or Guid.Empty value from a string object, as a string value</returns>
		public static string GetGuidString(this string value)
		{
			var guidId = Guid.Empty;
			Guid.TryParse(value, out guidId);
			return guidId.ToString().Trim();
		}

		/// <summary>Gets the string to int.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The Integer value from a string object (defaults to empty int value), as an int value</returns>
		public static int GetStringToInt(this string value)
		{
			int.TryParse(value, out var intId);
			return intId;
		}

		/// <summary>Gets the operational start datetime.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The DateTime value from a string object, as a DateTime value (default value returns as DateTime.Now value)</returns>
		public static DateTime GetOperationalStartDatetime(this string value)
		{
			var now = DateTime.Now;
			DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
			DateTime.TryParse(value, out dateTime);
			return dateTime;
		}

		/// <summary>Gets the operational end datetime.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The DateTime value value from a string object, as a DateTime value (default value returns as DateTime.Now value)</returns>
		public static DateTime GetOperationalEndDatetime(this string value)
		{
			DateTime now = DateTime.Now;
			DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, 20, 0, 0);
			DateTime.TryParse(value, out dateTime);
			return dateTime;
		}

		/// <summary>Gets the date time.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The DateTime value from a string param value, as a DateTime value (default value returns as DateTime.Now value)</returns>
		public static DateTime GetDateTime(string value)
		{
			value = (string.IsNullOrEmpty(value)) ? string.Empty : value;
			var dtVal = DateTime.Now;
			DateTime.TryParse(value, out dtVal);
			return dtVal;
		}

		/// <summary>Gets the int.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The Integer value from a string param value, as an int value (default value returns as 0)</returns>
		public static int GetInt(string value)
		{
			value = (string.IsNullOrEmpty(value)) ? string.Empty : value;
			int.TryParse(value, out var intVal);
			return intVal;
		}

		/// <summary>Gets the double.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The Double value from a string param value, as a double value (default value returns as 0.00)</returns>
		public static double GetDouble(string value)
		{
			value = (string.IsNullOrEmpty(value)) ? string.Empty : value;
			double.TryParse(value, out var dblVal);
			return dblVal;
		}

		/// <summary>Gets the boolean.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The Boolean value from a string param value, as an int value (default value returns as false)</returns>
		public static bool GetBoolean(string value)
		{
			value = (string.IsNullOrEmpty(value)) ? string.Empty : value;
			bool.TryParse(value, out var blVal);
			return blVal;
		}

		/// <summary>Determines whether [is in range] [the specified base date].</summary>
		/// <param name="dateTime">The date time.</param>
		/// <param name="baseDate">The base date.</param>
		/// <param name="rangeSeconds">The range seconds.</param>
		/// <returns>
		///   <c>true</c> if [is in range] [the specified base date]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsInRange(this DateTime dateTime, DateTime baseDate, int rangeSeconds)
		{
			dateTime = dateTime.ToUniversalTime();
			baseDate = baseDate.ToUniversalTime();

			var startDate = baseDate.AddSeconds(-rangeSeconds);
			var endDate = baseDate.AddSeconds(rangeSeconds);
			return ((startDate <= dateTime) && (dateTime <= endDate));
		}

		/// <summary>Determines whether [is in range] [the specified start date].</summary>
		/// <param name="dateTime">The date time.</param>
		/// <param name="startDate">The start date.</param>
		/// <param name="endDate">The end date.</param>
		/// <returns>
		///   <c>true</c> if [is in range] [the specified start date]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsInRange(this DateTime dateTime, DateTime startDate, DateTime endDate)
		{
			dateTime = dateTime.ToUniversalTime();
			startDate = startDate.ToUniversalTime();
			endDate = endDate.ToUniversalTime();
			if (endDate >= startDate) // swap start/end if they're out of order
			{
				return ((startDate <= dateTime) && (dateTime <= endDate));
			}

			var tempStart = startDate;
			startDate = endDate;
			endDate = tempStart;
			return ((startDate <= dateTime) && (dateTime <= endDate));
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ExcludeFromHashAttribute : Attribute
	{
	}
}