using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MentorShip.Services
{
    public class MenteeApplicationService : MongoDBService
    {
        private readonly IMongoCollection<MenteeApplicationModel> _menteeApplicationCollection;

        public MenteeApplicationService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _menteeApplicationCollection = database.GetCollection<MenteeApplicationModel>("menteeApplications");
        }

        public async Task<List<MenteeApplicationModel>> GetAllMenteeApplication()
        {
            return await _menteeApplicationCollection.Find(a => true).ToListAsync();
        }

        public async Task<List<MenteeApplicationModel>> GetMenteeApplicationByMenteeId(string menteeId)
        {
            return await _menteeApplicationCollection.Find(a => a.MenteeProfile.Id == menteeId).ToListAsync();
        }

        public async Task<MenteeApplicationModel> UpdateMenteeApplicationStatus(string id, ApprovalStatus status)
        {
            var filter = Builders<MenteeApplicationModel>.Filter.Eq(a => a.Id, id);
            var update = Builders<MenteeApplicationModel>.Update.Set(a => a.Status, status);
            return await _menteeApplicationCollection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task DeleteMenteeApplication(string id)
        {
            await _menteeApplicationCollection.DeleteOneAsync(a => a.Id == id);
        }

        public async Task<MenteeApplicationModel> CreateMenteeApplication(MenteeApplicationModel menteeApplication)
        {
            await _menteeApplicationCollection.InsertOneAsync(menteeApplication);
            return menteeApplication;
        }

        public async Task<List<MenteeApplicationModel>> GetMenteeApplicationByMentorId(string mentorId)
        {
            return await _menteeApplicationCollection.Find(a => a.MentorId == mentorId).ToListAsync();
        }
    }
}
