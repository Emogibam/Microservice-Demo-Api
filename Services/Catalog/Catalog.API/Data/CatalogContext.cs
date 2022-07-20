using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        /// <summary>
        /// Creating a database while instantiating the constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CatalogContext(IConfiguration configuration)
        {            
            // creating a mongodb client
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            //creatind a database and if there is no database it creates a new one
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            // getting a mongobd collection from the the appsetting, the collection name is product
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
