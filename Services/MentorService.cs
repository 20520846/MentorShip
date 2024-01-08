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
        private readonly SkillService _skillService;
        private readonly PlanService _planService;

        public MentorService(IOptions<MongoDBSettings> mongoDBSettings, SkillService skillService, PlanService planService) : base(mongoDBSettings)
        {
            _mentorCollection = database.GetCollection<Mentor>("mentors");
            _skillService = skillService;
            _planService = planService;
        }
        public async Task<Mentor> GetMentorById(string id)
        {
            var mentor = await _mentorCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return mentor;
        }

        public async Task<Mentor> RegisterMentor(Mentor mentor)
        {
            await _mentorCollection.InsertOneAsync(mentor);
            Plan newPlan = new Plan
            {
                MentorId = mentor.Id,
                Name = 0,
                Price = 100000,
                CallTimes = 1,
                Weeks = 4,
                Description = "",
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            var plan = await _planService.CreatePlan(newPlan);
            mentor.Price = plan.Price;
            await _mentorCollection.ReplaceOneAsync(m => m.Id == mentor.Id, mentor);
            return mentor;
        }
        public async Task<List<Mentor>> GetAllMentors()
        {
            var mentor = await _mentorCollection.Find(a => true).ToListAsync();
            return mentor;
        }
        public async Task<List<Mentor>> SearchMentor(string? name = null, string[]? skillIds = null, int? minPrice = null, int? maxPrice = null)
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
                var skillFilter = builder.All("SkillIds", skillIds);
                filter = builder.And(filter, skillFilter);
            }
            Console.WriteLine(filter);
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
        public async Task<Mentor> UpdateMentor(Mentor mentor)
        {
            await _mentorCollection.ReplaceOneAsync(m => m.Id == mentor.Id, mentor);
            return mentor;
        }
        public async Task<List<Skill>> GetSkillsByMentorId(string mentorId)
        {
            var mentor = await _mentorCollection.Find(m => m.Id == mentorId).FirstOrDefaultAsync();
            if (mentor == null)
            {
                throw new Exception("Mentor not found");
            }

            var skillIds = mentor.SkillIds;
            var skills = new List<Skill>();

            foreach (var skillId in skillIds)
            {
                var skill = await _skillService.GetSkillById(skillId);
                if (skill != null)
                {
                    skills.Add(skill);
                }
            }

            return skills;
        }
    }
}