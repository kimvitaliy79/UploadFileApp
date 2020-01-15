using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadData.Models
{
    public class FileItem
    {     
        public string Name { get; set; }
        public int Size { get; set; }    
        public string Type { get; set; }
        public string UploadDate { get; set; }
    }
}