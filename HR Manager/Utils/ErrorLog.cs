using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Manager.Utils
{
    public class ErrorLog:HR_Manager.Models.IEntity
    {
        public DateTime Tiem { get; set; }
        public string Error { get; set; }
    }
}