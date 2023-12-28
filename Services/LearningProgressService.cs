using MentorShip.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MentorShip.Services
{

    public class LearningProgressService : MongoDBService
    {
        private readonly IMongoCollection<LearningProgress> _learningProgressCollection;

        public LearningProgressService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _learningProgressCollection = database.GetCollection<LearningProgress>("learningProgresses");
        }

        public async Task<LearningProgress> CreateLearningProgress(LearningProgress learningProgress)
        {
            await _learningProgressCollection.InsertOneAsync(learningProgress);
            return learningProgress;
        }

        public async Task<LearningProgress> UpdateLearningProgress(string id, LearningProgress updatedProgress)
        {
            await _learningProgressCollection.ReplaceOneAsync(lp => lp.Id == id, updatedProgress);
            return updatedProgress;
        }

        public async Task<List<LearningProgress>> GetLearningProgressByMenteeId(string menteeId)
        {
            var filter = Builders<LearningProgress>.Filter.Eq(lp => lp.MenteeId, menteeId);
            return await _learningProgressCollection.Find(filter).ToListAsync();
        }

        public async Task<List<LearningProgress>> GetLearningProgressByMentorId(string mentorId)
        {
            var filter = Builders<LearningProgress>.Filter.Eq(lp => lp.MentorId, mentorId);
            return await _learningProgressCollection.Find(filter).ToListAsync();
        }
        public async Task<LearningProgress> GetLearningProgressByApplicationId(string applicationId)
        {
            return await _learningProgressCollection.Find(lp => lp.ApplicationId == applicationId).FirstOrDefaultAsync();
        }
        public async Task<List<LearningProgress>> GetCompletedLearningProgressByMenteeId(string menteeId)
        {
            var filter = Builders<LearningProgress>.Filter.Eq(lp => lp.MenteeId, menteeId) &
                         Builders<LearningProgress>.Filter.Lt(lp => lp.EndDate, DateTime.Now) &
                         Builders<LearningProgress>.Filter.Eq(lp => lp.CallTimesLeft, 0);
            return await _learningProgressCollection.Find(filter).ToListAsync();
        }

        public async Task<List<LearningProgress>> GetCompletedLearningProgressByMentorId(string mentorId)
        {
            var filter = Builders<LearningProgress>.Filter.Eq(lp => lp.MentorId, mentorId) &
                         Builders<LearningProgress>.Filter.Lt(lp => lp.EndDate, DateTime.Now) &
                         Builders<LearningProgress>.Filter.Eq(lp => lp.CallTimesLeft, 0);
            return await _learningProgressCollection.Find(filter).ToListAsync();
        }
    }

    public class LearningTestProgressService : MongoDBService
    {
        private readonly IMongoCollection<LearningTestProgress> _learningTestProgressCollection;

        public LearningTestProgressService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _learningTestProgressCollection = database.GetCollection<LearningTestProgress>("learningTestProgresses");
        }

        public async Task<LearningTestProgress> CreateLearningTestProgress(LearningTestProgress learningTestProgress)
        {
            await _learningTestProgressCollection.InsertOneAsync(learningTestProgress);
            return learningTestProgress;
        }

        public async Task<LearningTestProgress> UpdateLearningTestProgress(string id, LearningTestProgress updatedProgress)
        {
            await _learningTestProgressCollection.ReplaceOneAsync(lp => lp.Id == id, updatedProgress);
            return updatedProgress;
        }

        public async Task<List<LearningTestProgress>> GetLearningTestProgressByMenteeId(string menteeId)
        {
            var filter = Builders<LearningTestProgress>.Filter.Eq(lp => lp.MenteeId, menteeId);
            return await _learningTestProgressCollection.Find(filter).ToListAsync();
        }

        public async Task<List<LearningTestProgress>> GetLearningTestProgressByMentorId(string mentorId)
        {
            var filter = Builders<LearningTestProgress>.Filter.Eq(lp => lp.MentorId, mentorId);
            return await _learningTestProgressCollection.Find(filter).ToListAsync();
        }
    }
}