using System;
using System.Threading.Tasks;
using Domain.EasyDelivery.EasyNotificacoes.Models;
using Domain.EasyDelivery.EasyNotificacoes.Services;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Abstractions;
using Newtonsoft.Json;

namespace Infra.CrossCutting.Notificacoes.Services
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> EnviarEmailAsync(EasyEmail email)
        {
            try
            {
                const string azureQueue = "queue-email-messages";

                var azureEmailConnString = configuration["ConnectionStrings:SendMailMessageConnection"];

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureEmailConnString);

                CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

                CloudQueue queue = queueClient.GetQueueReference(azureQueue);

                await queue.CreateIfNotExistsAsync();

                var emailSerialized = JsonConvert.SerializeObject(email);

                await queue.AddMessageAsync(new CloudQueueMessage(emailSerialized));

                return true;
            }
            catch (System.Exception e)
            {
                //TODO tratar logs
                throw e; 
                // return false;
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}