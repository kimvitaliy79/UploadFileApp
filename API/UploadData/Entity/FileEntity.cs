using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadData.Entity
{  
    public class FileEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime UploadDate { get; set; }
        public string Type { get; set; }
        public byte[] FileData { get; set; }
    }
}