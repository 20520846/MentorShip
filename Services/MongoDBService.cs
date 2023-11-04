using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MentorShip.Services;

public class MongoDBService
{
    protected readonly IMongoDatabase database;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
    }
}