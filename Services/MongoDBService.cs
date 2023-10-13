using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MentorShip.Services;

public class MongoDBService{
    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings){
        MongoClient client = new(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
    }
}

