using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class SkillService : MongoDBService
    {
        private readonly IMongoCollection<Skill> _skillCollection;

        public SkillService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _skillCollection = database.GetCollection<Skill>("skills");
        }

        public async Task<List<Skill>> GetAllSkills()
        {
            return await _skillCollection.Find(skill => true).ToListAsync();
        }

        public async Task<Skill> GetSkillById(string id)
        {
            return await _skillCollection.Find(skill => skill.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteSkill(string id)
        {
            await _skillCollection.DeleteOneAsync(skill => skill.Id == id);
        }

        public async Task<Skill> CreateSkill(Skill skill)
        {
            await _skillCollection.InsertOneAsync(skill);
            return skill;
        }

        public async Task<List<Skill>> GetSkillsByFieldId(string fieldId)
        {
            var filter = Builders<Skill>.Filter.Eq(s => s.FieldId, fieldId);
            return await _skillCollection.Find(filter).ToListAsync();
        }

    }
}
