using MentorShip.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public class FolderService : MongoDBService
    {
        private readonly IMongoCollection<FolderModel> _folderCollection;
        private readonly FileService _fileService;

        public FolderService(IOptions<MongoDBSettings> mongoDBSettings, FileService fileService) : base(mongoDBSettings)
        {
            _folderCollection = database.GetCollection<FolderModel>("folders");
            _fileService = fileService;
        }

        public async Task<FolderModel> CreateFolder(FolderModel folder)
        {
            await _folderCollection.InsertOneAsync(folder);
            return folder;
        }

        public async Task<FolderModel> AddFileToFolder(string folderId, string fileId)
        {
            var folder = await _folderCollection.Find(folder => folder.Id == folderId).FirstOrDefaultAsync();
            if (folder != null)
            {
                folder.FileIds.Add(new FileId { Id = fileId });
                await _folderCollection.ReplaceOneAsync(folder => folder.Id == folderId, folder);
                return folder;
            }
            return null;
        }

        public async Task<List<FolderModel>> GetFoldersByMentorId(string mentorId)
        {
            return await _folderCollection.Find(folder => folder.MentorId == mentorId).ToListAsync();
        }

        public async Task<FolderModel> GetFolderById(string id)
        {
            return await _folderCollection.Find(folder => folder.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteFolderById(string id)
        {
            var folder = await _folderCollection.Find(folder => folder.Id == id).FirstOrDefaultAsync();
            if (folder != null)
            {
                foreach (var fileId in folder.FileIds)
                {
                    // Access the Id property of the FileId object
                    await _fileService.DeleteFileById(fileId.Id);
                }
                await _folderCollection.DeleteOneAsync(folder => folder.Id == id);
            }


        }
    }
}