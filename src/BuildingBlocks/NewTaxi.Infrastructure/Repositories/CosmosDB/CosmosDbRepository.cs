using Microsoft.Extensions.Configuration;
using NewTaxi.Infrastructure.Factories.CosmosDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewTaxi.Infrastructure.Repositories.CosmosDB
{
    public class CosmosDbRepository<T> where T : class
    {
        private readonly ICosmosDbClientFactory _factory;

        public CosmosDbRepository(ICosmosDbClientFactory factory)
        {
            _factory = factory;
        }


    }
}
