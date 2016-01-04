using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HR_Manager.Utils
{
    public class AppConfig
    {
        private static string _CVFolderRelative = ConfigurationManager.AppSettings["CVFolder"];
        private static string _LettersFolderRelative = ConfigurationManager.AppSettings["LettersFolder"];
        private static string _PhotosFolderRelative = ConfigurationManager.AppSettings["PhotosFolder"];
        private static string _AppErrorLog = ConfigurationManager.AppSettings["AppErrorLog"];

        public static string CVFolderRelative
        {
            get
            {
                return _CVFolderRelative;
            }
        }
        public static string LettersFolderRelative
        {
            get
            {
                return _LettersFolderRelative;
            }
        }
        public static string PhotosFolderRelative
        {
            get
            {
                return _PhotosFolderRelative;
            }
        }
        public static string AppErrorLog
        {
            get
            {
                return _AppErrorLog;
            }
        }
    }
}