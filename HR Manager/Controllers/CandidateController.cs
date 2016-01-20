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
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

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
        public async Task<bool> MeetsRequirementsChange(int canndidateId, bool choice)
        {
            Candidate can = dao.GetCandidateById(canndidateId);
            can.MeetsRequirements = choice;
            dao.UpdatateCandidate(can);

            return true;
        }
        public async Task<ActionResult> AddNote(int id)
        {
            return View("_AddNote", id);
        }
        [HttpPost]
        public async Task<ActionResult> AddNote(int id, string note)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            Person person = dao.GetPersonById(id);
            PersonNote pn = new PersonNote()
            {
                Author = user.FirstName + " " + user.Surname,
                AuthorId = user.Id,
                Person = person,
                Comment = note,
                Date = DateTime.Now,

            };
            person.Notes.Add(pn);
            dao.UpdatatePerson(person);

            return View("_AjaxLoaderView");
        }
    }
}
