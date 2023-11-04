using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class UserService : MongoDBService
    {
        private readonly IMongoCollection<User> _userCollection;
       

        public UserService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {

            _userCollection = database.GetCollection<User>("users");
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
            return user;

        }


        public async Task Register(User user)
        {
            var existingUser = await GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }
            await _userCollection.InsertOneAsync(user);
            return;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await _userCollection.Find(a => a.Email == email && a.Password == password).FirstOrDefaultAsync();
            if(user != null)
            {
                user.Password = null;
            }
            return user;

        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return user;
        }
    }
}
