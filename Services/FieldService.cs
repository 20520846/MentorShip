// FieldService.cs
using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class FieldService : MongoDBService
    {
        private readonly IMongoCollection<Field> _fieldCollection;
 

        public FieldService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _fieldCollection = database.GetCollection<Field>("fields");
        }

        public async Task<List<Field>> GetAllFields()
        {
            return await _fieldCollection.Find(field => true).ToListAsync();
        }

        public async Task<Field> GetFieldById(string id)
        {
            return await _fieldCollection.Find(field => field.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteField(string id)
        {
            await _fieldCollection.DeleteOneAsync(field => field.Id == id);
        }

        public async Task<Field> CreateField(Field field)
        {
            await _fieldCollection.InsertOneAsync(field);
            return field;
        }
      

    }
}
