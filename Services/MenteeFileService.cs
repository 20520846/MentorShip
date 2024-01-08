using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace MentorShip.Services
{
    public class MenteeFileService : MongoDBService
    {
        private readonly IMongoCollection<MenteeFile> _menteeFileCollection;
        public MenteeFileService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _menteeFileCollection = database.GetCollection<MenteeFile>("menteeFile");
        }

        public async Task<MenteeFile> GetMenteeFileById(string id)
        {
            var menteeFile = await _menteeFileCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return menteeFile;
        }

        public async Task<List<MenteeFile>> GetMenteeFileByMenteeId(string id)
        {
            var menteeFile = await _menteeFileCollection.Find(u => u.MenteeId == id).ToListAsync();
            return menteeFile;
        }

        public async Task<List<MenteeFile>> GetMenteeFileByMentorId(string id)
        {
            var menteeFile = await _menteeFileCollection.Find(u => u.MentorId == id).ToListAsync();
            return menteeFile;
        }

        public async Task<MenteeFile> CreateMenteeFile(MenteeFile menteeFile)
        {
            await _menteeFileCollection.InsertOneAsync(menteeFile);
            return menteeFile;
        }
    }
}