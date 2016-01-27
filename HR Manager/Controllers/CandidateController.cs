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
using System.Threading;

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
            return PartialView("_AddNote", id);
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

            return RedirectToAction("ShowNotes", new { id = person.Id });
        }
        public async Task<ActionResult> ShowNotes(int id)
        {
            Person person = dao.GetPersonById(id);
            ICollection<PersonNote> model = person.Notes;

            return PartialView("_ShowPersonNotes", model);
        }

        public async Task<ActionResult> EditPersonNote(int noteId)
        {
            PersonNote pn = dao.GetPersonNoteById(noteId);

            return PartialView("_EditPersonNote", pn);
        }
        [HttpPost]
        public async Task<ActionResult> EditPersonNote(int noteId, string text)
        {
            PersonNote pn = dao.GetPersonNoteById(noteId);
            pn.Comment = text;
            dao.UpdatatePersonNote(pn);
            return PartialView("_SinglePersonNote", pn);
        }
        [HttpPost]
        public async Task<ActionResult> DeletePersonNote(int noteId)
        {
            dao.RemovePersonNote(noteId);

            return new EmptyResult();
        }
        public async Task<ActionResult> TagList(int id)
        {
            ICollection<SkillTag> model = dao.GetPersonById(id).Tags;
            ViewBag.PersonId = id;

            return PartialView("_AddTag", model);
        }
        public async Task<ActionResult> AddTag(int id, string newTag)
        {

            Person person = dao.GetPersonById(id);

            List<string> tagList = newTag.Split(new char[] { ';', ' ', ',' }).ToList<string>();
            foreach (var tag in tagList)
            {
                if ((!string.IsNullOrEmpty(tag)) && (person.Tags.Where(x => x.Tag == tag).Count() <= 0))
                {
                    SkillTag skillTag = new SkillTag()
                    {
                        People = new List<Person>(),
                        Tag = tag
                    };
                    skillTag.People.Add(person);
                    person.Tags.Add(skillTag);
                }
            }
            dao.UpdatatePerson(person);
            ViewBag.PersonId = person.Id;
            return PartialView("_TagList", person.Tags);
        }
        public async Task<ActionResult> DeleteTag(int personId, int tagId)
        {
            Person person = dao.GetPersonById(personId);
            SkillTag tag = dao.GetTagById(tagId);
            person.Tags.Remove(tag);

            dao.UpdatatePerson(person);

            return new EmptyResult();
        }
        public async Task<ActionResult> SearchTag(string term)
        {

            string[] tagList = dao.GetTagList().Where(x => x.Tag.Contains(term) || (x.Tag == term))
                .Take(5)
                .Select(x => x.Tag)
                .Distinct()
                .ToArray();

            return Json(tagList, JsonRequestBehavior.AllowGet);
            
        }
        public async Task<ActionResult> Contact(int id)
        {
            Person person = dao.GetPersonById(id);
            ContactViewModel model = new ContactViewModel()
            {
                Email = person.Email,
                Name = person.Name,
                PhoneNumber = person.PhoneNumber,
                Surname = person.Surname,
                Id = person.Id,
            };

            return PartialView("_ContactView", model);
        }

    }
}
