using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HR_Manager.Utils
{
    public static class Utils
    {
        public static long GenerateJobOfferId()
        {
            DateTime date = DateTime.Now;
            long id = 10000000000*date.Year + 100000000*date.Month+ 1000000*date.Day+ 10000 * date.Hour+ 100*date.Second;

            return id;
        }

        public static string CVPath(string cvFileName)
        {
            var cvPathFolder = AppConfig.CVFolderRelative;
            var path = Path.Combine(cvPathFolder, cvFileName);
            return path;
        }

        public static string LetterPath(string letterFileName)
        {
            var cvPathFolder = AppConfig.LettersFolderRelative;
            var path = Path.Combine(cvPathFolder, letterFileName);
            return path;
        }

        public static string PhotoPath(string photoFileName)
        {
            var cvPathFolder = AppConfig.PhotosFolderRelative;
            var path = Path.Combine(cvPathFolder, photoFileName);
            return path;
        }

        public static string CreatePath(HttpServerUtilityBase server, HttpPostedFileBase file, string folderPath)
        {
            if (IsFileCorrect(file))
            {
                var fileExt = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid() + fileExt;

                var path = Path.Combine(server.MapPath(folderPath), fileName);
                return path;
            }
            else
            {
                return null;
            }

        }

        private static bool IsFileCorrect(HttpPostedFileBase file)
        {
            bool isNotNull = file != null ? true : false;
            bool isBiggerThenZero = file.ContentLength > 0 ? true : false;
            bool isNotTooBig = file.ContentLength < (5 * 1024 * 1024) ? true : false;
            bool isImage = Path.GetExtension(file.FileName).ToLower().Equals(".jpg") ||
                Path.GetExtension(file.FileName).ToLower().Equals(".jpeg") ||
                Path.GetExtension(file.FileName).ToLower().Equals(".png") ||
                Path.GetExtension(file.FileName).ToLower().Equals(".gif");
            bool isPdf = Path.GetExtension(file.FileName).ToLower().Equals(".pdf");

            if (isNotNull && isBiggerThenZero && isNotTooBig && (isImage || isPdf))
                return true;
            else
                return false;
        }
    }
}