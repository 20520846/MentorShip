using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class MenteeService : MongoDBService
    {
        private readonly IMongoCollection<Mentee> _menteeCollection;
        public MenteeService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _menteeCollection = database.GetCollection<Mentee>("mentee");
        }

        public async Task<Mentee> GetMenteeById(string id)
        {
            var mentee = await _menteeCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return mentee;
        }

        public async Task<Mentee> RegisterMentee(Mentee mentee)
        {
            await _menteeCollection.InsertOneAsync(mentee);
            return mentee;
        }
        public async Task<Mentee> UpdateMentee(Mentee mentee)
        {
            await _menteeCollection.ReplaceOneAsync(m => m.Id == mentee.Id, mentee);
            return mentee;
        }

    }
}