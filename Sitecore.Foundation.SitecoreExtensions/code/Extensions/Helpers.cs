using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Runtime.CompilerServices;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public class ApiAuthKey
	{
		public string ApiAuthKeyId { get; set; } = string.Empty;
		public string ApiAuthKeyValue { get; set; } = string.Empty;
	}

	public static class JsonExtension
	{
		public enum JsonRequestBehavior
		{
			/// <summary>
			/// HTTP GET requests from the client are allowed.
			/// </summary>
			AllowGet = 0,

			/// <summary>
			/// HTTP GET requests from the client are not allowed.
			/// </summary>
			DenyGet = 1
		}

		/// <summary>Converts to json.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The JSon string value from the JSon object</returns>
		public static string ToJson(this object value)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Serialize
			};
			return JsonConvert.SerializeObject(value, settings);
		}

		/// <summary>Converts to jsonobject.</summary>
		/// <param name="value">The value.</param>
		/// <returns>The JSon object value</returns>
		public static object ToJsonObject(this object value)
		{
			return JsonConvert.DeserializeObject(value.ToJson());
		}
	}

	public static class Helpers
	{
		/// <summary>Gets the name of the method.</summary>
		/// <param name="callerMethod">The caller method.</param>
		/// <returns>The Calling Method's name string value or empty</returns>
		public static string GetMethodName([CallerMemberName] string callerMethod = "")
		{
			return callerMethod;
		}

		/// <summary>Sets the name of the method.</summary>
		/// <param name="callerMethod">The caller method.</param>
		/// <returns>The callerMethod string value using the callerMethod param</returns>
		public static string SetMethodName(string callerMethod)
		{
			return callerMethod;
		}

		/// <summary>Files the date stamp.</summary>
		/// <param name="dateTime">The date time.</param>
		/// <returns>The File DateStamp string value</returns>
		public static string FileDateStamp(DateTime dateTime)
		{
			return $@"{dateTime:MM/dd/yyyy}";
		}
	}
}