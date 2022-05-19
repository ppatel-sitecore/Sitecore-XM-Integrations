using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sitecore.Foundation.SitecoreExtensions.MVC.Extensions;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.SitecoreExtensions.Extensions
{
	public interface ISitecoreIntegrationsBlobService
	{
		CloudBlobClient Client { get; }
		CloudBlobContainer Container { get; }
		Task<T> Get<T>(string id);
		Task<string> Get(string id);
		Task Save(string reference, string blob, string fileType = null);
		Task Save(string reference, JObject blob, string fileType = null);
		Task Save(string reference, IFormFile blob, string fileType = null);
		Task Save(string reference, Stream file, string fileType = null);
		Task Save(string reference, byte[] bytes, string fileType = null, string cacheControl = null);
		Task Delete(string id);
		Task DeleteContainer();
	}

	public class SitecoreIntegrationsBlobService : ISitecoreIntegrationsBlobService
	{
		public CloudBlobClient Client { get; }
		public CloudBlobContainer Container { get; }
		
		private bool _isInitialized = false;
		private readonly BlobServiceConfig _config;

		/// <summary>
		/// Initializes a new instance of the <see cref="SitecoreIntegrationsBlobService" /> class.
		///	The IOC based constructor method for the SitecoreIntegrationsBlobService class object with Dependency Injection
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <exception cref="System.Exception">
		/// The Connection string not supplied.
		/// or
		/// The Blob container not specified.
		/// or
		/// @"{ex.Message}. The blob service must be invoked with a valid configuration.")
		/// </exception>
		public SitecoreIntegrationsBlobService(BlobServiceConfig config)
		{
			_config = config;
			try
			{
				if (config.ConnectionString == null)
				{
					throw new Exception(@"The Connection string not supplied.");
				}
				if (config.ContainerName == null)
				{
					throw new Exception(@"The Blob container not specified.");
				}

				CloudStorageAccount.TryParse(config.ConnectionString, out var storage);
				Client = storage.CreateCloudBlobClient();
				if (config.ContainerName != null)
				{
					Container = Client.GetContainerReference(config.ContainerName);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($@"{ex.Message}. The blob service must be invoked with a valid configuration.");
			}
		}

		/// <summary>Gets the specified identifier.</summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The response data string value containing the Azure Container Blob Item's data</returns>
		public async Task<string> Get(string id)
		{
			await this.Init();
			var value = await Container.GetBlockBlobReference(id).DownloadTextAsync();
			return value;
		}


		/// <summary>Gets the specified identifier.</summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id">The identifier.</param>
		/// <returns>The serialized response data object value containing the Azure Container Blob Item's data</returns>
		public virtual async Task<T> Get<T>(string id)
		{
			await this.Init();
			var obj = await Container.GetBlockBlobReference(id).DownloadTextAsync();
			return JsonConvert.DeserializeObject<T>(obj);
		}

		/// <summary>Gets the append BLOB reference.</summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>The CloudAppendBlob object value from the Azure Container Blob Item</returns>
		public async Task<CloudAppendBlob> GetAppendBlobReference(string fileName)
		{
			await this.Init();
			return Container.GetAppendBlobReference(fileName);
		}

		/// <summary>Gets the BLOB reference.</summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>The CloudBlob object value from the Azure Container Blob Item</returns>
		public async Task<CloudBlob> GetBlobReference(string fileName)
		{
			await this.Init();
			return Container.GetBlobReference(fileName);
		}

		/// <summary>Saves the specified reference.</summary>
		/// <param name="reference">The reference.</param>
		/// <param name="blob">The BLOB.</param>
		/// <param name="fileType">Type of the file.</param>
		public async Task Save(string reference, JObject blob, string fileType = null)
		{
			await this.Init();
			var block = Container.GetBlockBlobReference(reference);
			if (fileType != null)
			{
				block.Properties.ContentType = fileType;
			}
			await block.UploadTextAsync(JsonConvert.SerializeObject(blob), Encoding.UTF8, AccessCondition.GenerateEmptyCondition(),
				new BlobRequestOptions()
				{
					SingleBlobUploadThresholdInBytes = 4 * 1024 * 1024
				}, new OperationContext());
		}

		/// <summary>Saves the specified reference.</summary>
		/// <param name="reference">The reference.</param>
		/// <param name="bytes">The bytes.</param>
		/// <param name="fileType">Type of the file.</param>
		/// <param name="cacheControl">The cache control.</param>
		public async Task Save(string reference, byte[] bytes, string fileType = null, string cacheControl = null)
		{
			await this.Init();
			var block = Container.GetBlockBlobReference(reference);
			if (fileType != null)
			{
				block.Properties.ContentType = fileType;
			}
			await block.UploadFromByteArrayAsync(bytes, 0, bytes.Length);
		}

		/// <summary>Saves the specified reference.</summary>
		/// <param name="reference">The reference.</param>
		/// <param name="blob">The BLOB.</param>
		/// <param name="fileType">Type of the file.</param>
		public async Task Save(string reference, string blob, string fileType = null)
		{
			await this.Init();
			var block = Container.GetBlockBlobReference(reference);
			if (fileType != null)
			{
				block.Properties.ContentType = fileType;
			}
			await block.UploadTextAsync(blob, Encoding.UTF8, AccessCondition.GenerateEmptyCondition(), 
				new BlobRequestOptions()
				{
					SingleBlobUploadThresholdInBytes = 4 * 1024 * 1024
				}, new OperationContext());
		}

		/// <summary>Saves the specified reference.</summary>
		/// <param name="reference">The reference.</param>
		/// <param name="blob">The BLOB.</param>
		/// <param name="fileType">Type of the file.</param>
		public async Task Save(string reference, IFormFile blob, string fileType = null)
		{
			await this.Init();
			var block = Container.GetBlockBlobReference(reference);
			block.Properties.ContentType = fileType ?? blob.ContentType;
			var stream = blob.OpenReadStream();
			await block.UploadFromStreamAsync(stream);
		}

		/// <summary>Saves the specified reference.</summary>
		/// <param name="reference">The reference.</param>
		/// <param name="file">The file.</param>
		/// <param name="fileType">Type of the file.</param>
		public async Task Save(string reference, Stream file, string fileType = null)
		{
			await this.Init();
			var block = Container.GetBlockBlobReference(reference);
			block.Properties.ContentType = fileType;
			await block.UploadFromStreamAsync(file);
		}

		/// <summary>Deletes the specified identifier.</summary>
		/// <param name="id">The identifier.</param>
		public async Task Delete(string id)
		{
			await this.Init();
			await Container.GetBlockBlobReference(id).DeleteIfExistsAsync();
		}

		/// <summary>Deletes the container.</summary>
		public async Task DeleteContainer()
		{
			await Container.DeleteAsync();
		}

		/// <summary>Initializes this instance.</summary>
		private async Task Init()
		{
			if (_isInitialized)
			{
				return;
			}

			var created = await Container.CreateIfNotExistsAsync();
			if (created)
			{
				var permissions = await Container.GetPermissionsAsync();
				permissions.PublicAccess = _config.AccessType;
				await Container.SetPermissionsAsync(permissions);

				var properties = await Client.GetServicePropertiesAsync();
				properties.Cors.CorsRules.Add(new CorsRule
				{
					AllowedHeaders =
					{
						"*"
					},
					AllowedOrigins =
					{
						"*"
					},
					AllowedMethods =
						CorsHttpMethods.Options |
						CorsHttpMethods.Get |
						CorsHttpMethods.Put |
						CorsHttpMethods.Post |
						CorsHttpMethods.Head |
						CorsHttpMethods.Delete |
						CorsHttpMethods.Merge,
					ExposedHeaders =
					{
						"*"
					},
					MaxAgeInSeconds = (int)TimeSpan.FromHours(1).TotalSeconds
				});
				await Client.SetServicePropertiesAsync(properties);
			}
			_isInitialized = true;
		}
	}
}