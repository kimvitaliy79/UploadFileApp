using Services.UploadData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using UploadData.Context;
using UploadData.Entity;
using UploadData.Models;

namespace UploadData.Controllers
{
    public class FileController : ApiController
    {
        public FileController()
        {

        }
        FileContext _fileContext = new FileContext();    

        [HttpGet]
        public IHttpActionResult GetFileList()
        {
            List<FileItem> returnItems = new List<FileItem>();

            foreach (FileEntity entity in _fileContext.Files)
                returnItems.Add(new FileItem { Name = entity.Name, Size = entity.Size, Type = entity.Type, UploadDate = entity.UploadDate.ToString() });
                        

            return Ok(new ResultFiles { Items= returnItems});
        }
        
        [HttpGet]
        public IHttpActionResult GetFileValidation(bool isConfig=false)
        {
            return  Ok(new ResultRestrictFile { RestrictFileData= ConfigurationService.GetFileRestriction(isConfig) });
        }
      
        [HttpPost]
        public async Task<HttpResponseMessage> UploadFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current?.Server?.MapPath("~/App_Data");
            if (string.IsNullOrEmpty(root))
                root = AppDomain.CurrentDomain?.SetupInformation?.ApplicationBase;  

            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
               

                foreach (MultipartFileData file in provider.FileData)
                {
                    byte[] fileData= File.ReadAllBytes(file.LocalFileName);

                    _fileContext.AddFile(new FileEntity { Name = file.Headers?.ContentDisposition?.FileName,
                                              Type= file.Headers?.ContentType?.MediaType ,
                                              UploadDate=DateTime.Now, Size = fileData.Length,
                                              FileData= fileData
                    });
                    File.Delete(file.LocalFileName);                 
                      
                } 

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }     
    }
}
