using DataManagementSystem.Models;
using DataManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementSystem.Services
{
    public interface IZipFileService
    {
        Task<ZipFileContent> SaveFile(byte[] fileContet);
    }
    public class ZipFileService : IZipFileService
    {
        private readonly IDecryptionService _decryptionService;
        private readonly IZipFileContentRepository _zipRepository;

        public ZipFileService(IDecryptionService decryptionService, IZipFileContentRepository zipRepository)
        {
            _decryptionService = decryptionService;
            _zipRepository = zipRepository;
        }

        public async Task<ZipFileContent> SaveFile(byte[] fileContet)
        {
            try
            {
                string decryptedContent = await Task.Run(() => _decryptionService.Decrypt(fileContet));
                ZipFileContent file = await Task.Run(() => _zipRepository.Add(new Models.ZipFileContent
                {
                    FileContent = decryptedContent,
                    CreatedDate = DateTime.Now
                }));

                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
