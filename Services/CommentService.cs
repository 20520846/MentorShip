using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class CommentService : MongoDBService
    {
        private readonly IMongoCollection<Comment> _commentCollection;
        private readonly IMongoCollection<Mentor> _mentorCollection;
        private readonly IMongoCollection<Mentee> _menteeCollection;

        public CommentService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _commentCollection = database.GetCollection<Comment>("comment");
            _mentorCollection = database.GetCollection<Mentor>("mentor");
            _menteeCollection = database.GetCollection<Mentee>("mentee");
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _commentCollection.InsertOneAsync(comment);
            return comment;
        }

        public async Task<Comment> GetCommentById(string id)
        {
            var comment = await _commentCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllCommentByMentorId(string id)
        {
            var comments = await _commentCollection.Find(u => u.MentorId == id).ToListAsync();
            return comments;
        }
    }
}