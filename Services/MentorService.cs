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
            _mentorCollection = database.GetCollection<Mentor>("mentors");
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
        public async Task<List<Mentor>> GetAllMentors()
        {
            return await _mentorCollection.Find(a => true).ToListAsync();
        }
        public async Task<List<Mentor>> SearchMentor(string? name = null, List<string>? skillIds = null, int? minPrice = null, int? maxPrice = null)
        {
            var builder = Builders<Mentor>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(name))
            {
                var nameFilter = builder.Where(m => m.FirstName.Contains(name) || m.LastName.Contains(name));
                filter = builder.And(filter, nameFilter);
            }
            if (skillIds != null && skillIds.Any())
            {
                var skillFilter = builder.AnyIn(m => m.SkillIds, skillIds);
                filter = builder.And(filter, skillFilter);
            }
            if (minPrice.HasValue)
            {
                var minPriceFilter = builder.Gte(m => m.Price, minPrice.Value);
                filter = builder.And(filter, minPriceFilter);
            }
            if (maxPrice.HasValue)
            {
                var maxPriceFilter = builder.Lte(m => m.Price, maxPrice.Value);
                filter = builder.And(filter, maxPriceFilter);
            }
            return await _mentorCollection.Find(filter).ToListAsync();
        }


    }
}