using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagementSystem.Models
{
    public class ZipContext : DbContext
    {
        public ZipContext(DbContextOptions<ZipContext> options)
           : base(options)
        { }

        public DbSet<ZipFileContent> ZipFileContents { get; set; }
    }
}
