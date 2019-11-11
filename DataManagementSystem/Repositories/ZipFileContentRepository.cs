using DataManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementSystem.Repositories
{
    public interface IZipFileContentRepository
    {
        ZipFileContent Add(ZipFileContent file);
    }
    public class ZipFileContentRepository : IZipFileContentRepository
    {
        private ZipContext _context;
        public ZipFileContentRepository(ZipContext context)
        {
            _context = context;
        }

        public ZipFileContent Add(ZipFileContent file)
        {
            _context.ZipFileContents.Add(file);
            _context.SaveChanges();
            return file;
        }
    }
}
