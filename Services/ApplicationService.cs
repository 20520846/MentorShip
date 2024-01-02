using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MentorShip.Services
{
    public class ApplicationService : MongoDBService
    {
        private readonly IMongoCollection<Application> _applicationCollection;
        private readonly IMongoCollection<Mentor> _mentorCollection;
        private readonly MentorService _mentorService;

        public ApplicationService(IOptions<MongoDBSettings> mongoDBSettings, MentorService mentorService) : base(mongoDBSettings)
        {
            _applicationCollection = database.GetCollection<Application>("applications");
            _mentorCollection = database.GetCollection<Mentor>("mentors");
            _mentorService = mentorService;
        }

        public async Task<List<Application>> GetAllApplication()
        {
            return await _applicationCollection.Find(a => true).ToListAsync();
        }

        public async Task<Application> GetApplicationById(string id)
        {
            return await _applicationCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Application> UpdateApplicationStatus(string id, ApprovalStatus status)
        {
            // var filter = Builders<Application>.Filter.Eq(a => a.Id, id);
            // var update = Builders<Application>.Update.Set(a => a.Status, status);

            // var application = await _applicationCollection.FindOneAndUpdateAsync(filter, update);

            //// If the application is approved, register the mentor
            //if (status == ApprovalStatus.Approved)
            //{                               
            //    application.MentorProfile.ApplicationId = application.Id;
            //    await _mentorCollection.InsertOneAsync(application.MentorProfile);
            //}

            var application = await _applicationCollection.FindSync(a => a.Id == id).FirstOrDefaultAsync();
            application.Status = status;
            if (status == ApprovalStatus.Approved)
            {
                Mentor mentorProfile = new Mentor
                {
                    ApplicationId = application.Id,
                    JobTitle = application.MentorProfile.JobTitle,
                    Avatar = application.MentorProfile.Avatar,
                    Email = application.MentorProfile.Email,
                    FirstName = application.MentorProfile.FirstName,
                    LastName = application.MentorProfile.LastName,
                    PhoneNumber = application.MentorProfile.PhoneNumber,
                    DateOfBirth = application.MentorProfile.DateOfBirth,
                    Bio = application.MentorProfile.Bio,
                    LinkedIn = application.MentorProfile.LinkedIn,
                    Twitter = application.MentorProfile.Twitter,
                    SkillIds = application.MentorProfile.SkillIds,
                    ImageUrls = application.MentorProfile.ImageUrls,
                    CreatedAt = DateTime.Now,
                    RatingStar = application.MentorProfile.RatingStar,
                    Introduction = application.MentorProfile.Introduction,
                    IsUnavailable = application.MentorProfile.IsUnavailable,
                };
                var mentor = await _mentorService.RegisterMentor(mentorProfile);
                application.MentorProfile.Id = mentor.Id;
                application.MentorProfile.ApplicationId = mentor.ApplicationId;
            }
            await _applicationCollection.ReplaceOneAsync(a => a.Id == id, application);
            return application;
        }

        public async Task DeleteApplication(string id)
        {
            await _applicationCollection.DeleteOneAsync(a => a.Id == id);
        }
        public async Task<Application> CreateApplication(Application application)
        {

            await _applicationCollection.InsertOneAsync(application);
            return application;
        }
    }
}
