using Microsoft.WindowsAzure.Storage.Blob;
using Sitecore.Foundation.SitecoreExtensions.Extensions;
using Sitecore.Foundation.SitecoreExtensions.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sitecore.Diagnostics
{
	public static class LogExt
	{
		/// <summary>Logs the exception.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="obj">The object.</param>
		/// <param name="createCustomLogFile">if set to <c>true</c> [create custom log file].</param>
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

		/// <summary>Logs the exception.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="obj">The object.</param>
		/// <param name="createCustomLogFile">if set to <c>true</c> [create custom log file].</param>
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

		/// <summary>Logs the API response messages.</summary>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="messageKeyValue">The message key value.</param>
		public static void LogApiResponseMessages(string methodName, string message, Exception origExc = null, string messageKeyValue = "")
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			messageKeyValue = (!string.IsNullOrEmpty(messageKeyValue)) ? messageKeyValue : LoggingNotifications.GetGeneralLogMessagePrefixKey();
			message = LoggingNotifications.GetLogExceptionMessage(methodName, $@"{messageKeyValue}: {message}", origExc);
			if (origExc != null)
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
		private static readonly ReaderWriterLockSlim ReadWriteLock = new ReaderWriterLockSlim();

		/// <summary>Gets the general log message prefix key.</summary>
		/// <returns>Api General Log Message</returns>
		public static string GetGeneralLogMessagePrefixKey()
		{
			return "Api General Log Message";
		}

		/// <summary>Gets the API response message prefix key.</summary>
		/// <returns>Api Response Message</returns>
		public static string GetApiResponseMessagePrefixKey()
		{
			return "Api Response Message";
		}

		/// <summary>Gets the exception message prefix key.</summary>
		/// <returns>Api Exception Message</returns>
		public static string GetExceptionMessagePrefixKey()
		{
			return "Api Exception Message";
		}

		/// <summary>Gets the no method identifier passed in prefix key.</summary>
		/// <returns>NoMethodIdPassedIn Api Message</returns>
		public static string GetNoMethodIdPassedInPrefixKey()
		{
			return "NoMethodIdPassedIn Api Message";
		}

		/// <summary>Gets the HTML notification for view.</summary>
		/// <param name="notificationMessage">The notification message.</param>
		/// <param name="alertType">Type of the alert.</param>
		/// <returns>HtmlNotification message string display in Html view</returns>
		public static string GetHtmlNotificationForView(string notificationMessage, string alertType)
		{
			return ServerSideNotificationWrappers.NotificationAlerts(notificationMessage, alertType);
		}

		/// <summary>Generates the log exception message.</summary>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <returns>The generated and formatted log exception message string value</returns>
		public static string GenerateLogExceptionMessage(string methodName, string message, Exception origExc)
		{
			return GetLogExceptionMessage(methodName, message, origExc);
		}

		/// <summary>Logs the exception notification.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
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

		/// <summary>Logs the exception notification.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
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

		/// <summary>Logs the API response messages.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="messagePrefixKeyValue">The message prefix key value.</param>
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

		/// <summary>Logs the API response messages.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="messagePrefixKeyValue">The message prefix key value.</param>
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

		/// <summary>Gets the log exception message.</summary>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="notificationTypePrefix">The notification type prefix.</param>
		/// <returns>The generated and formatted log exception message string value</returns>
		public static string GetLogExceptionMessage(string methodName, string message, Exception origExc, string notificationTypePrefix = "")
		{
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
			return sb.ToString().Trim();
		}

		/// <summary>Writes the API response messages asynchronous.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="message">The message.</param>
		/// <param name="origExc">The original exc.</param>
		/// <param name="messagePrefixKeyValue">The message prefix key value.</param>
		private static async Task WriteApiResponseMessagesAsync(LogSettings configSettings, string methodName, string message, Exception origExc, string messagePrefixKeyValue = "")
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			messagePrefixKeyValue = (!string.IsNullOrEmpty(messagePrefixKeyValue)) ? messagePrefixKeyValue : GetGeneralLogMessagePrefixKey();
			message = GetLogExceptionMessage(methodName, $@"{messagePrefixKeyValue}: {message}", origExc, GetApiResponseMessagePrefixKey());
			if (origExc != null)
			{
				await WriteToCustomLogFileAsync(configSettings, methodName, message, false, true);
				return;
			}
			await WriteToCustomLogFileAsync(configSettings, methodName, message, true);
		}

		/// <summary>Writes to custom log file asynchronous.</summary>
		/// <param name="configSettings">The configuration settings.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="logMessage">The log message.</param>
		/// <param name="isInfoMessage">if set to <c>true</c> [is information message].</param>
		/// <param name="isExceptionMessage">if set to <c>true</c> [is exception message].</param>
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

		/// <summary>Gets the message type key.</summary>
		/// <param name="isExceptionMessage">if set to <c>true</c> [is exception message].</param>
		/// <param name="isInfoMessage">if set to <c>true</c> [is information message].</param>
		/// <returns>The Message Type Key string value (i.e. InfoMessage or ExceptionMessage)</returns>
		private static string GetMessageTypeKey(bool isExceptionMessage, bool isInfoMessage = false)
		{
			return !isExceptionMessage ? @"InfoMessage" : @"ExceptionMessage";
		}
	}

	public static class ServerSideNotificationWrappers
	{
		/// <summary>Notifications the alerts.</summary>
		/// <param name="notificationMessage">The notification message.</param>
		/// <param name="alertType">Type of the alert.</param>
		/// <returns>The notification alerts message as a HtmlString wrapped value</returns>
		public static string NotificationAlerts(string notificationMessage, string alertType)
		{
			var wrapperNotify = GetAlertType(notificationMessage, alertType);
			var notificationWrapperId = $@"<div id='notify-message-wrapper'>{wrapperNotify}<div>";
			return notificationWrapperId;
		}

		/// <summary>Models the wrapped ul errors summary.</summary>
		/// <param name="modelErrors">The model errors.</param>
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


		/// <summary>
		/// Gets the type of the alert.
		/// This method will apply inner HTML responsive styled wrappers for notifications styled message around server message needed to be returned to the user
		/// (i.e. info; success; danger/error; warning; certificate; question styled messages) - All this types and HTML div wrapper are as per Bootstrap responsive-design standards
		/// </summary>
		/// <param name="notificationMessage">The notification message.</param>
		/// <param name="alertType">Type of the alert.</param>
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
	}
}