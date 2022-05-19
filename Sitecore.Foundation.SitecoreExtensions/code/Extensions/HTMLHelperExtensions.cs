using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class HtmlHelperExtensions
	{
		/// <summary>Validations the error for.</summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="error">The error.</param>
		/// <returns>The ValidationErrorFor boolean status as a MvcHtmlString value</returns>
		public static MvcHtmlString ValidationErrorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string error)
		{
			return htmlHelper.HasError(ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression)) ? new MvcHtmlString(error) : null;
		}

		/// <summary>Determines whether the specified model metadata has error.</summary>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="modelMetadata">The model metadata.</param>
		/// <param name="expression">The expression.</param>
		/// <returns>
		///   <c>true</c> if the specified model metadata has error; otherwise, <c>false</c>.
		/// </returns>
		private static bool HasError(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression)
		{
			var modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
			var formContext = htmlHelper.ViewContext.FormContext;
			if (formContext == null)
			{
				return false;
			}

			if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName))
			{
				return false;
			}

			var modelState = htmlHelper.ViewData.ModelState[modelName];
			var modelErrors = modelState?.Errors;
			return modelErrors?.Count > 0;
		}
	}
}