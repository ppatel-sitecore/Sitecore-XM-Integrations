using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Project.Template.Solution.MVC.Extensions
{
	public class AppEnvSettingsModel
	{
		public Database MasterDBTarget { get; set; }
		public Item DynamicLayoutItem { get; set; }
		public ID DynamicLayoutItemTemplateId { get; set; }
		public  ID DynamicLayoutFieldContentId { get; set; }
		public string DynamicLayoutCshtmlPath { get; set; } = string.Empty;
		public string ProjectsTemplateFolderPath { get; set; } = string.Empty;
		public Item RootContentItem { get; set; }
		public string SiteNameKey { get; set; } = string.Empty;
		public Item SiteItem { get; set; }
		public Item HomeItem { get; set; }
		public Item GlobalItem { get; set; }
		public string CanonicalUrl { get; set; } = string.Empty;
		public bool EnableBrandedModalSpinner { get; set; }
		public bool BrandedModalSpinnerSVG { get; set; }
	}
}