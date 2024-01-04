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
        private readonly MentorService _mentorService;

        public CommentService(IOptions<MongoDBSettings> mongoDBSettings, MentorService mentorService) : base(mongoDBSettings)
        {
            _commentCollection = database.GetCollection<Comment>("comment");
            _mentorCollection = database.GetCollection<Mentor>("mentor");
            _menteeCollection = database.GetCollection<Mentee>("mentee");
            _mentorService = mentorService;
        }

        public async Task<Comment> CreateComment(Comment comment, Mentor mentor)
        {
            await _commentCollection.InsertOneAsync(comment);
            if (mentor.RatingStar == 0)
            {
                Console.WriteLine(mentor.RatingStar);
                mentor.RatingStar = comment.RatingStar;
            }
            else
            {
                double rating = (mentor.RatingStar + comment.RatingStar) / 2;
                mentor.RatingStar = Math.Round(rating * 2) / 2;
            }
            Console.WriteLine(mentor.RatingStar);
            mentor.RatingCount++;
            await _mentorService.UpdateMentor(mentor);
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