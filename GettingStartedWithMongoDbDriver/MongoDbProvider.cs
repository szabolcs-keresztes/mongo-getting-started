using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GettingStartedWithMongoDbDriver
{
    public class MongoDbProvider : IMongoDbProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="database">A mongo database</param>
        public MongoDbProvider(IMongoDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            this.Database = database;
            Console.WriteLine("We are using the 'test' database...");
        }

        /// <summary>
        /// A protected instance of the mongo database
        /// </summary>
        protected IMongoDatabase Database { get; }
        
        /// <summary>
        /// Insert one document to the specified collection
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="document">The Bson Document</param>
        public async void InsertOne(string collectionName, BsonDocument document)
        {
            var collection = this.Database.GetCollection<BsonDocument>(collectionName);
            await collection.InsertOneAsync(document);
        }

        /// <summary>
        /// Insert a document multiple times to a specified collection
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="document">The Bson Document</param>
        /// <param name="times">How many times</param>
        public async void InsertMultipeTimes(string collectionName, BsonDocument document,
            int times)
        {
            var collection = this.Database.GetCollection<BsonDocument>(collectionName);
            for (var i = 0; i < times; i++)
            {
                await collection.InsertOneAsync(document);
            }
        }

        /// <summary>
        /// Delete many document which match the filter in a specified collection
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <param name="filter">Deltion filter</param>
        public async void DeleteMany(string collectionName, BsonDocument filter)
        {
            var collection = this.Database.GetCollection<BsonDocument>(collectionName);
            await collection.DeleteManyAsync(filter);
        }

        /// <summary>
        /// Counts the documents inside of a collection
        /// </summary>
        /// <param name="collectionName">Name of the collection</param>
        /// <returns>Number of documents</returns>
        public async Task<int> CountDocumentsManually(string collectionName)
        {
            var collection = this.Database.GetCollection<BsonDocument>(collectionName);
            var count = 0;
            using (var cursor = await collection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    count += batch.Count();
                }
            }
            return count;
        }

        public async Task<long> CountDocuments(string collectionName)
        {
            var collection = this.Database.GetCollection<BsonDocument>(collectionName);
            return await collection.CountAsync(new BsonDocument());
        }

    }
}
