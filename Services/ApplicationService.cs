using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MentorShip.Services
{
    public class ApplicationService : MongoDBService
    {
        private readonly IMongoCollection<Application> _applicationCollection;

        public ApplicationService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _applicationCollection = database.GetCollection<Application>("applications");
        }

        public async Task<List<Application>> GetAllApplication()
        {
            return await _applicationCollection.Find(a => true).ToListAsync();
        }

        public async Task<Application> GetApplicationById(string id)
        {
            return await _applicationCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Application> UpdateApplicationStatus(string id, ApplicationStatus status)
        {
            var filter = Builders<Application>.Filter.Eq(a => a.Id, id);
            var update = Builders<Application>.Update.Set(a => a.Status, status);
            return await _applicationCollection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task DeleteApplication(string id)
        {
            await _applicationCollection.DeleteOneAsync(a => a.Id == id);
        }
        public async Task<Application> CreateApplication(Application application)
        {
            await _applicationCollection.InsertOneAsync(application);
            return application;
        }
    }
}
