using MongoDB.Bson;
using System.Threading.Tasks;

namespace GettingStartedWithMongoDbDriver
{
    internal interface IMongoDbProvider
    {
        void InsertOne(string collectionName, BsonDocument document);

        void InsertMultipeTimes(string collectionName, BsonDocument document, int times);

        void DeleteMany(string collectionName, BsonDocument filter);

        Task<int> CountDocumentsManually(string collectionName);

        Task<long> CountDocuments(string collectionName);
    }
}