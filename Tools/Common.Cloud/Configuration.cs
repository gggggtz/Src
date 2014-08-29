using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Common.Cloud
{
    public class CloudUtility
    {
        private const string AzureStorageConnectionStringSettingName = "AzureStorageConnectionString";
        private const string AzureBlobRetryPolicyTimeOutSettingName = "AzureBlobRetryPolicyTimeOut";
        private const string AzureBlobRetryPolicyAttemptsSettingName = "AzureBlobRetryPolicyAttempts";
        private const string AzureQueueRetryPolicyTimeOutSettingName = "AzureQueueRetryPolicyTimeOut";
        private const string AzureQueueRetryPolicyAttemptsSettingName = "AzureQueueRetryPolicyAttempts";
        private const string AzureSQLServerDatabaseConnectionStringSettingName = "AzureSQLServerDatabaseConnectionString";

        public static readonly string AzureStorageConnectionString;

        public static readonly int AzureBlobRetryPolicyTimeOut;

        public static readonly int AzureBlobRetryPolicyAttempts;

        public static readonly int AzureQueueRetryPolicyTimeOut;

        public static readonly int AzureQueueRetryPolicyAttempts;

        public static readonly string AzureSQLServerDatabaseConnectionString;

        static CloudUtility()
        {
            AzureStorageConnectionString = RoleEnvironment.GetConfigurationSettingValue(AzureStorageConnectionStringSettingName);
            AzureBlobRetryPolicyAttempts = int.Parse(RoleEnvironment.GetConfigurationSettingValue(AzureBlobRetryPolicyAttemptsSettingName));
            AzureBlobRetryPolicyTimeOut = int.Parse(RoleEnvironment.GetConfigurationSettingValue(AzureBlobRetryPolicyTimeOutSettingName));
            AzureQueueRetryPolicyAttempts = int.Parse(RoleEnvironment.GetConfigurationSettingValue(AzureQueueRetryPolicyAttemptsSettingName));
            AzureQueueRetryPolicyTimeOut = int.Parse(RoleEnvironment.GetConfigurationSettingValue(AzureQueueRetryPolicyTimeOutSettingName));
            AzureSQLServerDatabaseConnectionString = RoleEnvironment.GetConfigurationSettingValue(AzureSQLServerDatabaseConnectionStringSettingName);
        }

        public static CloudStorageAccount AzureStorageAccount { get; private set; }

        public static CloudBlobClient AzureBlobClient { get; private set; }

        public static CloudQueueClient AzureQueueClient { get; private set; }

        public static void InitializeAllServices()
        {
            AzureStorageAccount = CloudStorageAccount.Parse(AzureStorageConnectionString);
            
            AzureBlobClient = AzureStorageAccount.CreateCloudBlobClient();
            AzureBlobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(AzureBlobRetryPolicyTimeOut), AzureBlobRetryPolicyAttempts);

            AzureQueueClient = AzureStorageAccount.CreateCloudQueueClient();
            AzureQueueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(AzureQueueRetryPolicyTimeOut), AzureQueueRetryPolicyAttempts);
        }

        static Dictionary<string, CloudBlobContainer> containers = new Dictionary<string, CloudBlobContainer>();

        public static CloudBlobContainer GetAzureBlobContainer(string containerName)
        {
            lock (containers)
            {
                if (!containers.ContainsKey(containerName))
                {
                    var container = AzureBlobClient.GetContainerReference(containerName);
                    if (container.CreateIfNotExists())
                    {
                        container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                    }
                    containers[containerName] = container;
                }
                return containers[containerName];
            }
        }

        static Dictionary<string, CloudQueue> queues = new Dictionary<string, CloudQueue>();
        public static CloudQueue GetAzureQueue(string queueName)
        {
            lock (queues)
            {
                if (!queues.ContainsKey(queueName))
                {
                    var queue = AzureQueueClient.GetQueueReference(queueName);
                    queue.CreateIfNotExists();
                    queues[queueName] = queue;
                }
                return queues[queueName];
            }
        }
    }
}
