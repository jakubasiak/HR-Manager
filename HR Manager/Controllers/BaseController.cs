using HR_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Controllers
{
    public class BaseController : Controller
    {
        public IDataAccess dao;

        public BaseController()
        {
            dao = new DAO();
        }
    }
}