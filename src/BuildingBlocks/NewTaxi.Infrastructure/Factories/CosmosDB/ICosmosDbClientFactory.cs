using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace NewTaxi.Infrastructure.Factories.CosmosDB
{
    public interface ICosmosDbClientFactory
    {
        Task CreateDatabaseAsync(string databaseId);
        Task<Container> GetContainerAsync<T>() where T : class;
    }
}