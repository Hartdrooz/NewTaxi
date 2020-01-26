using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Reflection;
using NewTaxi.Infrastructure.Attributes;
using NewTaxi.Infrastructure.Extensions;

namespace NewTaxi.Infrastructure.Factories.CosmosDB
{
    public class CosmosDbClientFactory : ICosmosDbClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _cosmosDbClient;
        private readonly Dictionary<string, Container> _containers;
        private Database _database;

        public CosmosDbClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _cosmosDbClient = new CosmosClient(configuration["CosmosDb:CnxString"]);
            _containers = new Dictionary<string, Container>();
        }

        public async Task CreateDatabaseAsync(string databaseId)
        {
            var response = await _cosmosDbClient.CreateDatabaseIfNotExistsAsync(databaseId);
            _database = response.Database;
        }

        public async Task<Container> GetContainerAsync<T>() where T : class
        {
            string containerName = typeof(T).Name;

            if (_containers.ContainsKey(containerName))
            {
                return _containers[containerName];
            }

            var instance = Activator.CreateInstance<T>();
            var partitionKey = instance.GetPartitionKeyName();

            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new Exception($"No partition key present on class {containerName}");
            }

            await _database.CreateContainerIfNotExistsAsync(containerName, $"/{partitionKey}");

            var container = _cosmosDbClient.GetContainer(_configuration["CosmosDb:Database"], typeof(T).Name);

            _containers.Add(containerName, container);

            return container;
        }




    }
}
