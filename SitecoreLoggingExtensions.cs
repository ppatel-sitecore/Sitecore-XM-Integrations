using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Blob;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Foundation.SitecoreExtensions.MVC.Extensions;

namespace Sitecore.Diagnostics
{
	public static class LogExt
	{
		/// <summary>
		/// Common re-usable LogException method used for logging exception separated log errors throughout the entire application into Sitecore Log files
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <param name="obj"></param>
		/// <param name="createCustomLogFile"></param>
		public static void LogException(LogSettings configSettings, string methodName, string message, Exception origExc, object obj, bool createCustomLogFile = false)
		{
			message = (message.Contains("[methodName]")) ? message.Replace("[methodName]", methodName) : message;
			if (createCustomLogFile)
			{
				LoggingNotifications.LogExceptionNotification(configSettings, methodName, message, origExc);
			}
			else
			{
				var exception = LoggingNotifications.GetLogExceptionMessage(methodName, message, origExc, LoggingNotifications.GetApiResponseMessagePrefixKey());
				Log.Error($@"{message} - {LoggingNotifications.GetExceptionMessagePrefixKey()}: {exception}.", obj);
				if (origExc != null)
				{
					throw origExc;
				}
			}
		}

		/// <summary>
		/// Common re-usable LogException method used for logging exception separated log errors throughout the entire application into Sitecore Log files
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <param name="obj"></param>
		/// <param name="createCustomLogFile"></param>
		public static void LogException(ConfigSettings configSettings, string methodName, string message, Exception origExc, object obj, bool createCustomLogFile = false)
		{
			message = (message.Contains("[methodName]")) ? message.Replace("[methodName]", methodName) : message;
			if (createCustomLogFile)
			{
				LoggingNotifications.LogExceptionNotification(configSettings, methodName, message, origExc);
			}
			else
			{
				var exception = LoggingNotifications.GetLogExceptionMessage(methodName, message, origExc, LoggingNotifications.GetApiResponseMessagePrefixKey());
				Log.Error($@"{message} - {LoggingNotifications.GetExceptionMessagePrefixKey()}: {exception}.", obj);
				if (origExc != null)
				{
					throw origExc;
				}
			}            
		}
        
		/// <summary>
		/// Common re-usable LogApiResponseMessages method used for api response message logging throughout the entire application
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="messageKeyValue"></param>
		/// <param name="isError"></param>
		/// <param name="origExc"></param>
		public static void LogApiResponseMessages(string methodName, string message, string messageKeyValue, bool isError = false, Exception origExc = null)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			messageKeyValue = (!string.IsNullOrEmpty(messageKeyValue)) ? messageKeyValue : LoggingNotifications.GetGeneralLogMessagePrefixKey();
			message = LoggingNotifications.GetLogExceptionMessage(methodName, $@"{messageKeyValue}: {message}", origExc);
			if (isError || origExc != null)
			{
				Log.Error(message, new object());
				if (origExc != null)
				{
					throw origExc;
				}
			}
			Log.Info(message, new object());
		}
	}
}

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public static class LoggingNotifications
	{
		private static readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

		/// <summary>
		/// Common re-usable GetGeneralLogMessagePrefixKey method used for returning the GeneralLogMessagePrefixKey value
		/// </summary>
		/// <returns>The GeneralLogMessagePrefixKey string value</returns>
		public static string GetGeneralLogMessagePrefixKey()
		{
			return @"Api General Log Message";
		}

		/// <summary>
		/// Common re-usable GetApiResponseMessagePrefixKey method used for returning the ApiResponseMessagePrefixKey value
		/// </summary>
		/// <returns>The ApiResponseMessagePrefixKey string value</returns>
		public static string GetApiResponseMessagePrefixKey()
		{
			return @"Api Response Message";
		}

		/// <summary>
		/// Common re-usable GetExceptionMessagePrefixKey method used for returning the ExceptionMessagePrefixKey value
		/// </summary>
		/// <returns>The ExceptionMessagePrefixKey string value</returns>
		public static string GetExceptionMessagePrefixKey()
		{
			return @"Api Exception Message";
		}

		/// <summary>
		/// Common re-usable GetNoMethodIdPassedInPrefixKey method used for returning the NoMethodIdPassedInPrefixKey value
		/// </summary>
		/// <returns>The NoMethodIdPassedInPrefixKey string value</returns>
		public static string GetNoMethodIdPassedInPrefixKey()
		{
			return @"NoMethodIdPassedIn Api Message";
		}

		/// <summary>
		/// Common re-usable GenerateLogExceptionMessage method used for returning exception separated log messages throughout the entire application
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <returns>The generated and formatted LogExceptionMessage string value</returns>
		public static string GenerateLogExceptionMessage(string methodName, string message, Exception origExc)
		{
			return GetLogExceptionMessage(methodName, message, origExc);
		}

		/// <summary>
		/// Common re-usable LogExceptionNotification method used for exception logging throughout the entire application
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		public static async void LogExceptionNotification(LogSettings configSettings, string methodName, string message, Exception origExc)
		{
			if (configSettings.EnableCustomFileLogging && !string.IsNullOrEmpty(configSettings.ConnectionString))
			{
				await WriteApiResponseMessagesAsync(configSettings, methodName, message, origExc, GetApiResponseMessagePrefixKey());
			}
			if (origExc != null)
			{
				throw origExc;
			}
		}

		/// <summary>
		/// Common re-usable LogExceptionNotification method used for exception logging throughout the entire application
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		public static async void LogExceptionNotification(ConfigSettings configSettings, string methodName, string message, Exception origExc)
		{
			var logSettings = configSettings.LogSettings;
			if (logSettings.EnableCustomFileLogging && !string.IsNullOrEmpty(logSettings.ConnectionString))
			{
				await WriteApiResponseMessagesAsync(logSettings, methodName, message, origExc, GetApiResponseMessagePrefixKey());
			}
			if (origExc != null)
			{
				throw origExc;
			}
		}

		/// <summary>
		/// Common re-usable LogApiResponseMessages method used for api response message logging throughout the entire application
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <param name="messagePrefixKeyValue"></param>
		public static async void LogApiResponseMessages(LogSettings configSettings, string methodName, string message, Exception origExc, string messagePrefixKeyValue = "")
		{
			if (configSettings.EnableCustomFileLogging && !string.IsNullOrEmpty(configSettings.ConnectionString))
			{
				await WriteApiResponseMessagesAsync(configSettings, methodName, message, origExc, messagePrefixKeyValue);
			}
			if (origExc != null)
			{
				throw origExc;
			}
		}

		/// <summary>
		/// Common re-usable LogApiResponseMessages method used for api response message logging throughout the entire application
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <param name="messagePrefixKeyValue"></param>
		public static async void LogApiResponseMessages(ConfigSettings configSettings, string methodName, string message, Exception origExc, string messagePrefixKeyValue = "")
		{
			var logSettings = configSettings.LogSettings;
			if (logSettings.EnableCustomFileLogging && !string.IsNullOrEmpty(logSettings.ConnectionString))
			{
				await WriteApiResponseMessagesAsync(logSettings, methodName, message, origExc, messagePrefixKeyValue);
			}
			if (origExc != null)
			{
				throw origExc;
			}
		}

		/// <summary>
		/// Common re-usable WriteApiResponseMessages method used for api response message logging throughout the entire application
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <param name="messageKeyValue"></param>
		/// <param name="isError"></param>
		private static async Task WriteApiResponseMessagesAsync(LogSettings configSettings, string methodName, string message, Exception origExc, string messageKeyValue = "")
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			messageKeyValue = (!string.IsNullOrEmpty(messageKeyValue)) ? messageKeyValue : GetGeneralLogMessagePrefixKey();
			message = GetLogExceptionMessage(methodName, $@"{messageKeyValue}: {message}", origExc, GetApiResponseMessagePrefixKey());
			if (origExc != null)
			{
				await WriteToCustomLogFileAsync(configSettings, methodName, message, false, true);
				return;
			}
			await WriteToCustomLogFileAsync(configSettings, methodName, message, true);
		}

		/// <summary>
		/// Common re-usable for building formatting wrappers message logging for the 'LogExceptionNotification'; 'LogNotificationMessages' and 'LogApiResponseMessages' methods above
		/// to make the logging chucks easily readable in the log file/s
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="message"></param>
		/// <param name="origExc"></param>
		/// <param name="notificationTypePrefix"></param>
		/// <returns>The generated and formatted GetLogExceptionMessage string value</returns>
		public static string GetLogExceptionMessage(string methodName, string message, Exception origExc, string notificationTypePrefix = "")
		{
			var notification = string.Empty;
			notificationTypePrefix = (!string.IsNullOrEmpty(notificationTypePrefix)) ? notificationTypePrefix : GetGeneralLogMessagePrefixKey();
			methodName = (string.IsNullOrEmpty(methodName)) ? GetNoMethodIdPassedInPrefixKey() : methodName;

			var sb = new StringBuilder();
			var dateStamp = DateTime.Now.ToString().Trim();
			sb.Append($@"------------------------------{methodName}:{dateStamp}------------------------------" + Environment.NewLine);
			var logMessage = string.Empty;

			if (!string.IsNullOrEmpty(message))
			{
				logMessage = $@"{methodName.Trim()} - {notificationTypePrefix}: {message.Trim()}.";
			}
			if (origExc != null)
			{
				logMessage = $@"{methodName.Trim()} - {notificationTypePrefix}: {message.Trim()}. {GetExceptionMessagePrefixKey()}: {origExc.Message} - {origExc.StackTrace}.";
			}

			sb.Append($@"{logMessage}" + Environment.NewLine);
			sb.Append($@"------------------------------{methodName}:{dateStamp}------------------------------" + Environment.NewLine);
			notification = sb.ToString().Trim();

			return notification;
		}

		/// <summary>
		/// Common re-usable GetHtmlNotificationForView method to return server error messages with HTML wrapper 
		/// </summary>
		/// <param name="notificationMessage"></param>
		/// <param name="alertType"></param>
		/// <returns>The NotificationAlerts message as a HtmlString wrapped value</returns>
		public static string GetHtmlNotificationForView(string notificationMessage, string alertType)
		{
			return ServerSideNotificationWrappers.NotificationAlerts(notificationMessage, alertType);
		}

		/// <summary>
		/// Common re-usable WriteToCustomLogFile method to create log file/s (1 per day) and logging message/s daily:
		/// Thread-safe code that Locks the file while it is being written to and Ques other thread requests until file is released
		/// </summary>
		/// <param name="configSettings"></param>
		/// <param name="methodName"></param>
		/// <param name="logMessage"></param>
		/// <param name="isInfoMessage"></param>
		/// <param name="isExceptionMessage"></param>
		private static async Task WriteToCustomLogFileAsync(LogSettings configSettings, string methodName, string logMessage, bool isInfoMessage = false, bool isExceptionMessage = false)
		{
			try
			{
				var sb = new StringBuilder();
				var dateTimeStamp = DateTime.Now.ToString("MM_dd_yyyy-HH_mm_ss");
				var dateStamp = DateTime.Now.ToString("MM_dd_yyyy");
				var fileRefPath = $@"{configSettings.AppLogFileKey}ApiApplicationNotifications_{dateStamp}.txt";

				sb.Append(
					$@"------------------------------{methodName}:{dateTimeStamp}:{GetMessageTypeKey(isExceptionMessage, isInfoMessage)}------------------------------" +
					Environment.NewLine);
				sb.Append($@"{logMessage}." + Environment.NewLine);
				sb.Append(
					$@"------------------------------{methodName}:{dateTimeStamp}:{GetMessageTypeKey(isExceptionMessage, isInfoMessage)}------------------------------" +
					Environment.NewLine);

				var logsBlobConfig = new BlobServiceConfig()
				{
					ConnectionString = configSettings.ConnectionString,
					ContainerName = configSettings.ContainerName,
					AccessType = BlobContainerPublicAccessType.Container
				};
				var logsBlob = new SitecoreIntegrationsBlobService(logsBlobConfig);
				await logsBlob.Save(fileRefPath, sb.ToString().Trim());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Common re-usable GetMessageTypeKey() method
		/// </summary>
		/// <param name="isExceptionMessage"></param>
		/// <param name="isInfoMessage"></param>
		/// <returns>The MessageTypeKey string value</returns>
		private static string GetMessageTypeKey(bool isExceptionMessage, bool isInfoMessage = false)
		{
			return !isExceptionMessage ? @"InfoMessage" : @"ExceptionMessage";
		}
	}

	public static class ServerSideNotificationWrappers
	{
		/// <summary>
		/// Common re-usable NotificationAlerts method used for dynamically create an HTML div wrapper for notifications styled message around server message 
		/// needed to be returned to the user - All this types and HTML div wrapper are as per Bootstrap responsive-design standards
		/// </summary>
		/// <param name="notificationMessage"></param>
		/// <param name="alertType"></param>
		/// <returns>The NotificationAlerts message as a HtmlString wrapped value</returns>
		public static string NotificationAlerts(string notificationMessage, string alertType)
		{
			var wrapperNotify = GetAlertType(notificationMessage, alertType);
			var notificationWrapperId = $@"<div id='notify-message-wrapper'>{wrapperNotify}<div>";
			return notificationWrapperId;
		}

		/// <summary>
		/// Common re-usable GetAlertType method to apply inner HTML responsive styled wrappers for notifications styled message around server message needed to be returned to the user
		/// (i.e. info; success; danger/error; warning; certificate; question styled messages) - All this types and HTML div wrapper are as per Bootstrap responsive-design standards
		/// </summary>
		/// <param name="alertType"></param>
		/// <returns>The GetAlertType string as a HtmlString wrapped value</returns>
		private static string GetAlertType(string notificationMessage, string alertType)
		{
			var notificationWrapperInfo = "<div id='notify-message' class='validation-summary-info alert-message alert-message alert-message-info'><i class='fas fa-info-circle'></i><span>{0}</span></div>";
			var notificationWrapperSuccess = "<div id='notify-message' class='validation-summary-success alert-message alert-message-success'><i class='fas fa-check'></i><span>{0}</span></div>";
			var notificationWrapperWarning = "<div id='notify-message' class='validation-summary-warning alert-message alert-message-warning'><i class='fas fa-exclamation-triangle'></i><span>{0}</span></div>";
			var notificationWrapperDanger = "<div id='notify-message' class='validation-summary-errors alert-message alert-message-danger'><i class='fas fa-exclamation-triangle'></i><span>{0}</span></div>";
			var notificationWrapperCertificate = "<div id='notify-message' class='validation-summary-certificate alert-message alert-message-certificate'><i class='fas fa-certificate'></i><span>{0}</span></div>";
			var notificationWrapperQuestion = "<div id='notify-message' class='validation-summary-question alert-message alert-message-question'><i class='fas fa-question-circle'></i><span>{0}</span></div>";

			var wrapperNotify = "";
			switch (alertType)
			{
				case "info":
					wrapperNotify = string.Format(notificationWrapperInfo, notificationMessage);
					break;
				case "success":
					wrapperNotify = string.Format(notificationWrapperSuccess, notificationMessage);
					break;
				case "warning":
					wrapperNotify = string.Format(notificationWrapperWarning, notificationMessage);
					break;
				case "danger":
					wrapperNotify = string.Format(notificationWrapperDanger, notificationMessage);
					break;
				case "certificate":
					wrapperNotify = string.Format(notificationWrapperCertificate, notificationMessage);
					break;
				case "question":
					wrapperNotify = string.Format(notificationWrapperQuestion, notificationMessage);
					break;
				default:
					wrapperNotify = string.Format(notificationWrapperDanger, notificationMessage);
					break;
			}
			return wrapperNotify;
		}

		/// <summary>
		/// Common re-usable ModelWrappedErrors method for applying wrapping a list of model errors as bulleted text summary
		/// </summary>
		/// <param name="modelErrors"></param>
		/// <returns>The ModelWrappedUlErrorsSummary message as a HtmlString wrapped value</returns>
		public static string ModelWrappedUlErrorsSummary(List<string> modelErrors)
		{
			var jsonModelErrors = new List<string>();
			if (modelErrors.Count <= 0)
			{
				return string.Empty;
			}

			foreach (var err in modelErrors)
			{
				var jsonModelError = $@"<li>{err}</li>";
				jsonModelErrors.Add(jsonModelError);
			}
			return $@"<ul id='notify-ul'>{string.Join(@"<br/>", jsonModelErrors)}<ul>";
		}
	}
}