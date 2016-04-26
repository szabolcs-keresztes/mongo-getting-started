using MongoDB.Bson;
using System.Threading.Tasks;

namespace GettingStartedWithMongoDbDriver
{
    interface IMongoDbProvider
    {
        void InsertOne(string collectionName, BsonDocument document);
        void InsertMultipeTimes(string collectionName, BsonDocument document, int times);

        void DeleteMany(string collectionName, BsonDocument filter);

        Task<int> CountDocuments(string collectionName);
    }
}