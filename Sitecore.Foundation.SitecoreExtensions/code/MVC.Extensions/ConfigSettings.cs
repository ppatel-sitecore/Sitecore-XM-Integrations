using System;
using System.IO;
using Newtonsoft.Json;
using Sitecore.Diagnostics;
using Sitecore.Foundation.SitecoreExtensions.Extensions;

namespace Sitecore.Foundation.SitecoreExtensions.MVC.Extensions
{
	public sealed class ConfigSettings
	{
		public LogSettings LogSettings { get; set; }
		private static ConfigSettings _instance = null;
		private static readonly object Padlock = new object();

		/// <summary>Gets the instance.</summary>
		/// <value>The instance.</value>
		public static ConfigSettings Instance
		{
			get
			{
				if (_instance != null)
				{
					_instance.GetConfigSettings();
					return _instance;
				}
				lock (Padlock)
				{
					_instance ??= new ConfigSettings();
					_instance.GetConfigSettings();
				}
				return _instance;
			}
		}


		/// <summary>Prevents a default instance of the <see cref="ConfigSettings" /> class from being created.</summary>
		private ConfigSettings()
		{
			//Do nothing as this is a Singleton Class which means class can only be instantiated with-in the class itself and create only one instance of itself,
			//allowing to have thread safe object instance with thread-safe locking on use of the object (i.e. using double-check locking) 
			//See: http://csharpindepth.com/articles/general/singleton.aspx for details
		}

		/// <summary>Gets the configuration settings.</summary>
		private void GetConfigSettings()
		{
			try
			{
				var jsonDataString = string.Empty;
				using (var reader = new StreamReader(@".\ApiAppSettings.json"))
				{
					jsonDataString = reader.ReadToEnd();
				}
				if (string.IsNullOrEmpty(jsonDataString))
				{
					return;
				}
				var jsonData = JsonConvert.DeserializeObject<AppEnvSettingsModel>(jsonDataString);
				LogSettings = jsonData != null 
					? new LogSettings()
					{
						EnableCustomFileLogging = jsonData.EnableCustomFileLogging,
						AppLogFileKey = jsonData.AppLogFileKey.Trim(),
						ConnectionString = jsonData.ConnectionString.Trim()
					}
					: new LogSettings();
			}
			catch (Exception ex)
			{
				LogExt.LogException(LogSettings, Helpers.GetMethodName(), "", ex, this, true);
			}
		}
	}
}