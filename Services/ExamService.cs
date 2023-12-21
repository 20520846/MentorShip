using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class ExamService : MongoDBService
    {
        private readonly IMongoCollection<Exam> _examCollection;
        public ExamService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _examCollection = database.GetCollection<Exam>("exam");
        }

        public async Task<Exam> CreateExam(Exam exam)
        {
            await _examCollection.InsertOneAsync(exam);
            return exam;
        }

        public async Task<Exam> GetExamById(string id)
        {
            var exam = await _examCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return exam;
        }

        public async Task<List<Exam>> GetExamByMentorId(string id)
        {
            var exam = await _examCollection.Find(u => u.MentorId == id).ToListAsync();
            return exam;
        }

        public async Task<Exam> UpdateNumberQues(string id, int number)
        {
            var exam = await _examCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            exam.NumberQues += number;
            await _examCollection.ReplaceOneAsync(u => u.Id == id, exam);
            return exam;
        }

        public async Task DeleteExamById(string id)
        {
            await _examCollection.DeleteOneAsync(u => u.Id == id);
        }
    }

    public class QuestionService : MongoDBService
    {
        private readonly IMongoCollection<Question> _questionCollection;
        public QuestionService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _questionCollection = database.GetCollection<Question>("question");
        }

        public async Task<Question> CreateQuestion(Question question)
        {
            await _questionCollection.InsertOneAsync(question);
            return question;
        }

        public async Task<Question> GetQuestionById(string id)
        {
            var question = await _questionCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            return question;
        }

        public async Task<List<Question>> GetQuestionsByExamId(string id)
        {
            var question = await _questionCollection.Find(u => u.ExamId == id).ToListAsync();
            return question;
        }

        public async Task DeleteQuestionById(string id)
        {
            await _questionCollection.DeleteOneAsync(u => u.Id == id);
        }

        public async Task DeleteQuestionByExamId(string id)
        {
            await _questionCollection.DeleteManyAsync(u => u.ExamId == id);
        }
    }
}
