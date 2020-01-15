using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UploadData.Entity;
using UploadData.Models;

namespace UploadData.Context
{
    public class FileContext:DbContext
    {
        public FileContext(): base("DBConnection")
        { }

        public DbSet<FileEntity> Files { get; set; }

        public void AddFile(FileEntity item)
        {
            this.Files.Add(item);
            this.SaveChanges();
        }
    }
}