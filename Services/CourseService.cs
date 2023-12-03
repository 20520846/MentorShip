using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class CourseService : MongoDBService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        public CourseService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _courseCollection = database.GetCollection<Course>("course");
        }
        public async Task<Course> GetCourseById(string id)
        {
            var course = await _courseCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return course;
        }

        public async Task<List<Course>> GetListAsync()
        {
            var courses = await _courseCollection.Find(new BsonDocument()).ToListAsync();
            return courses;
        }
        public async Task CreateAsync(Course course){
            await _courseCollection.InsertOneAsync(course);
            return;
        }
        public async Task UpdateAsync(string id, string mentorId, decimal price, double ratingStar){
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq("Id", id);
            UpdateDefinition<Course> update = Builders<Course>.Update.Set("MentorId", mentorId).Set("Price", price).Set("RatingStar", ratingStar);
            await _courseCollection.UpdateOneAsync(filter, update);
        }
        public async Task DeleteAsync(string id){
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq("Id", id);
            await _courseCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
