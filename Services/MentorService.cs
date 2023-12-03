using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class MentorService : MongoDBService
    {
        private readonly IMongoCollection<Mentor> _mentorCollection;
        public MentorService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _mentorCollection = database.GetCollection<Mentor>("mentor");
        }

        public async Task<Mentor> GetMentorById(string id)
        {
            var mentor = await _mentorCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return mentor;
        }

        public async Task<Mentor> RegisterMentor(Mentor mentor)
        {
            await _mentorCollection.InsertOneAsync(mentor);
            return mentor;
        }
    }
}