using HR_Manager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Controllers
{
    [Authorize]
    public class RecruitmentController : BaseController
    {

        //GET: Recruitment/Show/5
        public ActionResult Show(long? id)
        {
            if (id != null)
            {
                Recruitment model = dao.GetRecruitmentById((long)id);

                return View("CreateAndShow", model);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        

        public ActionResult List()
        {
            IEnumerable<Recruitment> recr = dao.GetRecruitmentsList();
            List<RecruitmentListViewModel> model = new List<RecruitmentListViewModel>();
            if(recr.Count()>0)
            { 
            foreach(var r in recr)
            {
                RecruitmentListViewModel rlvm = new RecruitmentListViewModel()
                {
                    Id = r.Id,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    OfferNumber = r.JobOffer.OfferNumber,
                    Name = r.JobOffer.Name,
                    Location = r.JobOffer.Location,
                    Current = r.JobOffer.Current,
                };
                model.Add(rlvm);
            }
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Recruitment/Create
        public ActionResult Create(long? id)
        {
            if (id == null)
            {
                Recruitment recr = new Recruitment();
                recr.Id = Utils.Utils.GenerateJobOfferId();

                return View("CreateAndShow", recr);
            }
            else
            {
                Recruitment recr = new Recruitment()
                {
                    Id = (long)id,
                    JobOffer = dao.GetJobOfferByOfferNumber((long)id),
                    StartTime = DateTime.Now,
                    Candidate = new List<Candidate>(),
                    Events = new List<RecruitmentEvent>(),
                    EndTime = DateTime.MaxValue,
                };
                RecruitmentEvent ev = new RecruitmentEvent()
                {
                    Event = "Rozpoczęcie rekrutacji",
                    Time = DateTime.Now,
                    Author = "HR Manager",
                    AuthorId = "HR Manager",
                    Recruitment = recr,                   
                };

                ((List<RecruitmentEvent>)recr.Events).Add(ev);
                dao.SaveRecruitment(recr);

                return View("CreateAndShow", recr);
            }
        }
        public ActionResult Delete(long id)
        {
            dao.RemoveRecruitmentById(id);

            return RedirectToAction("List", "Recruitment");
        }

        [HttpPost]
        public async Task<ActionResult> AddRecruitmentEvent(string text, long recruitmentId)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            Recruitment recr = dao.GetRecruitmentById(recruitmentId);

            RecruitmentEvent re = new RecruitmentEvent()
            {
                Author = user.FirstName + " " + user.Surname,
                AuthorId = user.Id,
                Recruitment = recr,
                Time = DateTime.Now,
                Event = text,
            };

            recr.Events.Add(re);
            dao.SaveRecruitmentEvwnt(re);


            return PartialView("_ShowEventView", recr.Events);
        }
        public async Task<ActionResult> EditRecruitmentEvent(int eventId)
        {
            RecruitmentEvent ev = dao.GetRecruitmentEventById(eventId);

            return PartialView("_EditRecruitmentEvent", ev);
        }
        [HttpPost]
        public async Task<ActionResult> EditRecruitmentEvent(int eventId, string text)
        {
            RecruitmentEvent ev = dao.GetRecruitmentEventById(eventId);
            ev.Event = text;
            dao.UpdatateRecruitmentEvent(ev);

            return PartialView("_SingleRecruitmentEvent", ev);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRecruitmentEvent(int eventId)
        {
            dao.RemoveRecruitmentEvent(eventId);

            return new EmptyResult();
        }


    }
}
