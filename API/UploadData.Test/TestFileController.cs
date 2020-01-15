using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UploadData.Controllers;
using UploadData.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Net.Http.Headers;
using System.Web.Http.Routing;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using UploadData.Context;
using System.Security.Principal;
using System.Collections.Specialized;

namespace UploadData.Test
{
    [TestClass]
    public class TestFileController
    {
        [TestMethod]
        public void GetAllFiles()
        {
            FileController fileController = new FileController();
            IHttpActionResult actionResult = fileController.GetFileList();
            var contentResult = actionResult as OkNegotiatedContentResult<ResultFiles>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            ResultFiles resultItem = contentResult.Content;

            List<FileItem> items = resultItem.Items;
            Assert.IsNotNull(items);           
        }

        [TestMethod]
        public void GetFileValidation()
        {
            FileController fileController = new FileController();
            var actionResult = fileController.GetFileValidation();
            var contentResult = actionResult as OkNegotiatedContentResult<ResultRestrictFile>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            ResultRestrictFile resultItem = contentResult.Content;
            List<RestrictFile> items = resultItem.RestrictFileData;
            Assert.IsTrue(items.Any());          
        }

        [TestMethod]
        public void GetFileValidationNegative()
        {
            try
            {
                FileController fileController = new FileController();
                var actionResult = fileController.GetFileValidation(true);
                var contentResult = actionResult as OkNegotiatedContentResult<ResultRestrictFile>;
                Assert.IsNotNull(contentResult);
                Assert.IsNotNull(contentResult.Content);
                ResultRestrictFile resultItem = contentResult.Content;
                List<RestrictFile> items = resultItem.RestrictFileData;
                Assert.IsTrue(items.Any());
            }
            catch(Exception ex)
            {
                Assert.IsNotNull(ex.Message);
            }
         
        }

        [TestMethod]
        public async Task UploadFileData()
        {
            FileController fileController = new FileController();               
            var controllerContext = CreateFile("test.txt", "text/plain");
            fileController.ControllerContext = controllerContext;          
            var retVal = await fileController.UploadFile() as HttpResponseMessage;
            Assert.IsTrue(retVal.StatusCode == System.Net.HttpStatusCode.OK);  
        }

        [TestMethod]
        public async Task UploadFileDataNegative()
        {
            try
            {
                FileController fileController = new FileController();
                var controllerContext = CreateFile("test.txt", string.Empty);
                fileController.ControllerContext = controllerContext;
                var retVal = await fileController.UploadFile() as HttpResponseMessage;
                Assert.IsTrue(retVal.StatusCode == System.Net.HttpStatusCode.InternalServerError);
            }
            catch(Exception ex) 
            {
                Assert.IsNotNull(ex.Message);  
            }

           
        }


        private HttpControllerContext CreateFile(string fileName, string contentType)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "");
            var content = new MultipartFormDataContent();
            const int lengthStream = 1900;
            var contentString = "test string data";
            ByteArrayContent contentBytes = new ByteArrayContent(Encoding.UTF8.GetBytes(contentString));

            contentBytes.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            contentBytes.Headers.Add("Content-Type", contentType); 
            contentBytes.Headers.Add("Content-Length", lengthStream.ToString());
            content.Add(contentBytes);
            request.Content = content;

            return new HttpControllerContext(new HttpConfiguration(), new HttpRouteData(new HttpRoute("")), request);
        }      

    }
}
