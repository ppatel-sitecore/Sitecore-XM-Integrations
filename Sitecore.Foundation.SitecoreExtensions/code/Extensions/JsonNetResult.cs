using System.Web.Mvc;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public class JsonNetResult : JsonResult
	{
		/// <summary>JsonNetResult() constructor method with params for the JsonNetResult class.</summary>
		/// <param name="data"></param>
		public JsonNetResult(object data)
		{
			Data = data;
		}

		/// <summary>Initializes a new instance of the <see cref="JsonNetResult" /> class.</summary>
		/// <param name="data">The data.</param>
		/// <param name="behavior">The behavior.</param>
		public JsonNetResult(object data, JsonRequestBehavior behavior)
		{
			Data = data;
			MaxJsonLength = int.MaxValue;
		}
	}
}