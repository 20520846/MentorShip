using MentorShip.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentorShip.Services
{
    public class FileService : MongoDBService
    {
        private readonly IMongoCollection<FileModel> _fileCollection;

        public FileService(IOptions<MongoDBSettings> mongoDBSettings) : base(mongoDBSettings)
        {
            _fileCollection = database.GetCollection<FileModel>("files");
        }

        public async Task<FileModel> CreateFile(FileModel file)
        {
            await _fileCollection.InsertOneAsync(file);
            return file;
        }

        public async Task<List<FileModel>> GetAllFiles()
        {
            return await _fileCollection.Find(file => true).ToListAsync();
        }

        public async Task<List<FileModel>> GetFilesByMentorId(string mentorId)
        {
            return await _fileCollection.Find(file => file.MentorId == mentorId).ToListAsync();
        }

        public async Task<FileModel> GetFileById(string id)
        {
            return await _fileCollection.Find(file => file.Id == id).FirstOrDefaultAsync();
        }

        public async Task<FileModel> DeleteFileById(string id)
        {
            return await _fileCollection.FindOneAndDeleteAsync(file => file.Id == id);
        }
    }
}