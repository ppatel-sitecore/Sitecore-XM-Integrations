using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Pipelines;
using Sitecore.Publishing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sitecore.Project.Template.Solution.MVC.Extensions
{
	public static class DynamicSiteImplementation
	{
		/// <summary>Processes the specified arguments.</summary>
		/// <param name="args">The arguments.</param>
		public static async void Process(PipelineArgs args)
		{
			var controller = new HomeController();
			var appSettings = controller.ConfigSettings.AppSettings;
			Log.Debug($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}-Started.");
			try
			{
				await SetupDynamicSiteImplementationSolution(appSettings);
			}
			catch (Exception ex)
			{
				Log.Error($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.", 
					ex, "DynamicSiteImplementation");
			}
			Log.Debug($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}-Completed.");
		}

		/// <summary>Setups the dynamic site implementation solution.</summary>
		private static async Task SetupDynamicSiteImplementationSolution(AppEnvSettingsModel appSettings)
		{
			try
			{
				using (new SecurityModel.SecurityDisabler())
				{
					if (appSettings.DynamicLayoutItem == null)
					{
						Log.Error($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}
							: Missing the 'appSettings.DynamicLayoutItem' Item in the Sitecore Content Tree.",
							appSettings.DynamicLayoutItem);
						return;
					}

					var parentItem = appSettings.DynamicLayoutItem.Parent;
					if (!appSettings.SiteNameKey.Contains("SiteName"))
					{
						Log.Error($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}
							: The 'appSettings.SiteNameKey' configuration has not been set correctly.",
							appSettings.DynamicLayoutItem);
						return;
					}

					if (!appSettings.SiteNameKey.Contains("SiteName"))
					{
						Log.Error($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}
							: The 'appSettings.SiteNameKey' configuration has not been set correctly.",
							appSettings.DynamicLayoutItem);
						return;
					}
					var newLayoutItem = appSettings.MasterDBTarget.GetItem($@"{parentItem.Paths.Path}/{appSettings.SiteNameKey}") ?? 
					                    parentItem.Add(appSettings.SiteNameKey, appSettings.DynamicLayoutItem.Template);
					newLayoutItem.Editing.BeginEdit();
					newLayoutItem[appSettings.DynamicLayoutFieldContentId] = appSettings.DynamicLayoutCshtmlPath;
					newLayoutItem.Editing.AcceptChanges();
					newLayoutItem.Editing.EndEdit();
					await PublishItemsToAllTargets(appSettings, newLayoutItem);
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.",
					ex, "DynamicSiteImplementation");
			}
		}

		/// <summary>Publishes the items to all targets.</summary>
		/// <param name="appSettings">The application settings.</param>
		/// <param name="contextItem">The context item.</param>
		private static Task PublishItemsToAllTargets(AppEnvSettingsModel appSettings, Item contextItem)
		{
			try
			{
				var publishingTargets = PublishManager.GetPublishingTargets(appSettings.MasterDBTarget);
				foreach (var publishingTarget in publishingTargets)
				{
					// Find the target database name, move to the next publishing target if it is empty.
					if (string.IsNullOrEmpty(publishingTarget.GetFieldValue("Target database")))
					{
						continue;
					}

					// Get the target database, if missing skip
					var targetDatabase = Configuration.Factory.GetDatabase(publishingTarget.GetFieldValue("Target database"));
					if (targetDatabase == null)
					{
						continue;
					}

					// Setup publishing options based on your need
					contextItem.PublishItem(PublishMode.Smart, targetDatabase.ConnectionStringName, false, true);
				}
			}
			catch (Exception ex)
			{
				Log.Error($@"{appSettings.SiteNameKey}-{Helpers.GetMethodName()}-ExceptionLog: {ex.Message}.",
					ex, "DynamicSiteImplementation");
			}
			return Task.CompletedTask;
		}
	}
}