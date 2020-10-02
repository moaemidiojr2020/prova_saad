using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Domain.EasyDelivery.EasyUploadImages.Queries;
using Domain.EasyDelivery.EasyUploadImages.Repositories;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace Infra.CrossCutting.UploadImages.Services
{
	public class UploadImagesService : IUploadImagesService
	{
		private readonly IConfiguration _configuration;

		public UploadImagesService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clinicId"></param>
		/// <param name="file"></param>
		/// <param name="bucketName"></param>
		/// <param name="fileFormat"></param>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public async Task<ImageUploadResultQuery> UploadImage(Guid clinicId, Stream file, string bucketName, string fileFormat = null, string fileName = null)
		{
			if (fileName == null)
			{
				fileName = $"{clinicId.ToString().Replace("-", String.Empty)}.{fileFormat}";
			}

			var accountName = _configuration.GetSection("AzureStorage:AZURE_ACCOUNT_NAME").Value;
			var acessKey = _configuration.GetSection("AzureStorage:AZURE_BLOB_KEY").Value;

			StorageCredentials storageCreds = new StorageCredentials(accountName, acessKey);

			CloudStorageAccount account = new CloudStorageAccount(storageCreds, true);

			CloudBlobClient blobClient = account.CreateCloudBlobClient();
			CloudBlobContainer container = blobClient.GetContainerReference(bucketName);

			CloudBlockBlob fileBlob = container.GetBlockBlobReference(fileName);
			fileBlob.Properties.ContentType = "image/jpg";

			await fileBlob.UploadFromStreamAsync(file);
			return new ImageUploadResultQuery
			{
				FileName = fileName,
				UrlImage = fileBlob.Uri.AbsoluteUri
			};
		}

		public async Task<ImageUploadResultQuery> UploadBase64Image(string base64Image)
		{
			// gera um nome rodomico par a imagem
			var fileName = Guid.NewGuid().ToString().Replace("-", String.Empty) + ".jpg";

			var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

			// Gera uma array de bytes
			byte[] iamgeBytes = Convert.FromBase64String(data);

			// Define o BLOB no qual a imagem será armazenada
			var blobClient = new BlobClient(_configuration.GetSection("AzureStorage:AZURE_BLOB_CONNECTION").Value, _configuration.GetSection("AzureStorage:AZURE_CONTAINER").Value, fileName);

			// Envia a imagem
			using (var stream = new MemoryStream(iamgeBytes))
			{
				await blobClient.UploadAsync(stream);
			}

			// retorna a URL da imagem
			return new ImageUploadResultQuery
			{
				FileName = fileName,
				UrlImage = blobClient.Uri.AbsoluteUri
			};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="bucketName"></param>
		/// <returns></returns>
		public async Task DeleteImage(string fileName)
		{
			var accountName = _configuration.GetSection("AzureStorage:AZURE_ACCOUNT_NAME").Value;
			var acessKey = _configuration.GetSection("AzureStorage:AZURE_BLOB_KEY").Value;
			var containerName = _configuration.GetSection("AzureStorage:AZURE_CONTAINER").Value;
			

			StorageCredentials storageCreds = new StorageCredentials(accountName, acessKey);

			CloudStorageAccount account = new CloudStorageAccount(storageCreds, true);

			CloudBlobClient blobClient = account.CreateCloudBlobClient();
			CloudBlobContainer container = blobClient.GetContainerReference(containerName);

			CloudBlockBlob fileInBlob = container.GetBlockBlobReference(fileName);

			await fileInBlob.DeleteIfExistsAsync();
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
