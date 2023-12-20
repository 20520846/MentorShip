using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class PlanService : MongoDBService
    {
        private readonly IMongoCollection<Plan> _planCollection;
        private readonly IMongoCollection<Mentor> _mentorCollection;

        public PlanService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _planCollection = database.GetCollection<Plan>("plan");
            _mentorCollection = database.GetCollection<Mentor>("mentor");
        }

        public async Task<Plan> CreatePlan(Plan plan)
        {
            await _planCollection.InsertOneAsync(plan);
            return plan;
        }

        public async Task<Plan> GetPlanById(string id)
        {
            var plan = await _planCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return plan;
        }

        public async Task<List<Plan>> GetAllPlanByMentorId(string id)
        {
            var plans = await _planCollection.Find(u => u.MentorId == id).ToListAsync();
            return plans;
        }
    }
}