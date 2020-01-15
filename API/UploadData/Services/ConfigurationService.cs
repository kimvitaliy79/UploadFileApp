using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UploadData.Models;

namespace Services.UploadData
{
    public static class ConfigurationService
    {
      static string  restrictFileTypes = string.Empty;
      static int restrictFileSize = 0;
         static ConfigurationService()
        {
          restrictFileTypes = ConfigurationManager.AppSettings["RestrictFileTypes"];
          string restrSize=  ConfigurationManager.AppSettings["RestrictFileSize"];
          restrictFileSize = !string.IsNullOrEmpty(restrSize) ? Convert.ToInt32(ConfigurationManager.AppSettings["RestrictFileSize"]): 0;
        }


        
       public static List<RestrictFile> GetFileRestriction(bool isConfig)
        {
            if(isConfig)
            {
                restrictFileTypes = null;
                restrictFileSize = 0;
            }

            if (string.IsNullOrEmpty(restrictFileTypes))
                throw new Exception("Problem with config files in API server");

            List<RestrictFile> returnFileTypes = new List<RestrictFile>();
            string[] fileTypesList = restrictFileTypes.Split(',');

            foreach (string item in fileTypesList)
                returnFileTypes.Add(new RestrictFile { Name = item.ToLower().Trim(), Size = restrictFileSize});

            return returnFileTypes;
        }
    }
}