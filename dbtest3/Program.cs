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
        static MongoClient client = null;
        static IMongoDatabase database = null;
        static IMongoCollection<BsonDocument> collection = null;

        static void Main(string[] args)
        {
            SetupDatabase();
            LoadData().Wait();
            while (true)
            {
                Console.Write("> ");
                string line = Console.ReadLine();
                if (line.ToLower() == "load" || line.ToLower() == "read")
                {
                    LoadData().Wait();
                } else if (line.ToLower() == "exit" || line.ToLower() == "quit")
                {
                    break;
                } else
                {
                    BsonDocument document;
                    try
                    {
                        document = BsonDocument.Parse(line);
                    }
                    catch(FormatException e)
                    {
                        Console.WriteLine("Invalid JSON, please try again.");
                        continue;
                    }

                    collection.InsertOne(document);
                    Console.WriteLine("Document inserted");
                }
            }
        }

        static void SetupDatabase()
        {
            Console.WriteLine("Setup Database Hello World");

            if (client == null)
            {
                client = new MongoClient("mongodb+srv://consumerteam:01292020@cluster0-tkvaq.azure.mongodb.net/test?retryWrites=true&w=majority");
                database = client.GetDatabase("datatest1");
                collection = database.GetCollection<BsonDocument>("dbt1");
            }

        }

        static async Task LoadData()
        {

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

