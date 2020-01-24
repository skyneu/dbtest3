using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace dbtest3
{
    class Program
    {
        static void Main(string[] args)
        {
            CallMain(args).Wait();
            Console.ReadLine();
        }
        static async Task CallMain(string[] args)
        {
            Console.WriteLine("Hello World");

            var client = new MongoClient("mongodb+srv://consumerteam:01292020@cluster0-tkvaq.azure.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("datatest1");

            var collection = database.GetCollection<BsonDocument>("dbt1");

            //Method 1
            using (var cursor = await collection.Find(new BsonDocument()).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }

        }
    }
}

