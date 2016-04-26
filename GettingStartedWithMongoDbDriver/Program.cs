using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GettingStartedWithMongoDbDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            var mongoClient = CreateMongoClient();

            var mongoDbProvider = new MongoDbProvider(mongoClient.GetDatabase("asyncTest"));

            const int numberOfInserts = 400;
            for (int i = 0; i < numberOfInserts; i++)
            {
                var doc = CreateRestaurant();
                mongoDbProvider.InsertOne("restaurants", doc);
            }

            Task.Run(async () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    long numberOfDocuments = await mongoDbProvider.CountDocuments("restaurants");
                    Console.WriteLine(numberOfDocuments + " documents counted");
                }
            }).Wait();

            mongoDbProvider.DeleteMany("restaurants", new BsonDocument { { "borough", "Manhattan" } });

            Thread.Sleep(1000);
        }

        protected static void GettingStarted()
        {
            var mongoClient = CreateMongoClient();

            var mongoDbProvider = new MongoDbProvider(mongoClient.GetDatabase("test"));

            mongoDbProvider.InsertOne("restaurants", CreateRestaurant());

            Task.Run(async () =>
            {
                long numberOfDocuments = await mongoDbProvider.CountDocuments("restaurants");
                Console.WriteLine(numberOfDocuments + " documents counted");
            }).Wait();
        }

        protected static IMongoClient CreateMongoClient()
        {
            var mongoClient = new MongoClient();
            if (mongoClient == null)
            {
                Console.WriteLine("The connection has not been created...");
            }
            Console.WriteLine("The connection with the local mongod has been created...");
            return mongoClient;
        }



        protected static BsonDocument CreateRestaurant()
        {
            return new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "zipcode", "10075" },
                        { "building", "1480" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
                { "borough", "Manhattan" },
                { "cuisine", "Italian" },
                { "grades", new BsonArray
                    {
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 10, 1, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "A" },
                            { "score", 11 }
                        },
                        new BsonDocument
                        {
                            { "date", new DateTime(2014, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
                            { "grade", "B" },
                            { "score", 17 }
                        }
                    }
                },
                { "name", "Vella" }
            };
        }
    }
}