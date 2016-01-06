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
            long id = 1000000000000*date.Year + 1000000*date.Month+ 10000*date.Day+ 100 * date.Hour+ 10*date.Second+date.Millisecond;

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
    }
}