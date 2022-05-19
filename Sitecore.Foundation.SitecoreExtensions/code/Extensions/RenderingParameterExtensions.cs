using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.Mvc.Presentation;
using System.Collections.Generic;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class RenderingParameterExtensions
	{
		/// <summary>Converts to json.</summary>
		/// <param name="renderingParameters">The rendering parameters.</param>
		/// <returns>The Rendering objects JSon string value</returns>
		public static string ToJson(this RenderingParameters renderingParameters)
		{
			if (!(renderingParameters is IEnumerable<KeyValuePair<string, string>> keyValues))
			{
				return string.Empty;
			}

			var renderingParams = new JObject();
			foreach (var keyValue in keyValues)
			{
				renderingParams.Add(keyValue.Key, keyValue.Value);
			}
			return JsonConvert.SerializeObject(renderingParams);
		}
	}
}