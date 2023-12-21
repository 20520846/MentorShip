using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using RabbitMQ.Client.Events;

namespace MentorShip.Services
{
    public class MenteeExamService : MongoDBService
    {
        private readonly IMongoCollection<MenteeExam> _menteeExamCollection;
        public MenteeExamService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _menteeExamCollection = database.GetCollection<MenteeExam>("menteeExam");
        }

        public async Task<MenteeExam> GetMenteeExamById(string id)
        {
            var menteeExam = await _menteeExamCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return menteeExam;
        }

        public async Task<List<MenteeExam>> GetMenteeExamByMenteeId(string id)
        {
            var menteeExam = await _menteeExamCollection.Find(u => u.MenteeId == id).ToListAsync();
            return menteeExam;
        }

        public async Task<List<MenteeExam>> GetMenteeExamByMentorId(string id)
        {
            var menteeExam = await _menteeExamCollection.Find(u => u.MentorId == id).ToListAsync();
            return menteeExam;
        }

        public async Task<MenteeExam> CreateMenteeExam(MenteeExam menteeExam)
        {
            await _menteeExamCollection.InsertOneAsync(menteeExam);
            return menteeExam;
        }
        public async Task<MenteeExam> UpdateNumberAns(string id, int number)
        {
            var menteeExam = await _menteeExamCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            menteeExam.NumberAns += number;
            await _menteeExamCollection.ReplaceOneAsync(u => u.Id == id, menteeExam);
            return menteeExam;
        }
    }

    public class AnswerService : MongoDBService
    {
        private readonly IMongoCollection<Answer> _answerCollection;
        public AnswerService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _answerCollection = database.GetCollection<Answer>("answer");
        }

        public async Task<Answer> CreateAnswer(Answer answer)
        {
            await _answerCollection.InsertOneAsync(answer);
            return answer;
        }

        public async Task<Answer> GetAnswerById(string id)
        {
            var answer = await _answerCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return answer;
        }

        public async Task<List<Answer>> GetAnswerByMenteeExamId(string id)
        {
            var answer = await _answerCollection.Find(u => u.MenteeExamId == id).ToListAsync();
            return answer;
        }

        public async Task<Answer> GetAnswerByQuestionId(string id)
        {
            var answer = await _answerCollection.Find(u => u.MenteeExamId == id).FirstOrDefaultAsync();
            return answer;
        }

        public async Task<Answer> UpdateAnswer(Answer answer)
        {
            await _answerCollection.ReplaceOneAsync(u => u.Id == answer.Id, answer);
            return answer;
        }
    }
}