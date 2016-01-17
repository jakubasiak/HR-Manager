using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Manager.Models;
using System.Threading.Tasks;

namespace HR_Manager.Controllers
{
    [Authorize]
    public class CandidateController : BaseController
    {
        public ActionResult GetCV(string path, string name)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = "CV - " + name + ".pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public async Task<ActionResult> PartialShow(int id)
        {
            Candidate can = dao.GetCandidateById(id);

            if (can != null)
            {
                return PartialView("_PartialShow", can);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}
