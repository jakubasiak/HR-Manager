using HR_Manager.Models;
using HR_Manager.Utils;
using iTextSharp.text;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Controllers
{
    [Authorize]
    public class CareerController : BaseController
    {
        public ActionResult AddJobOffer(long jobOfferId)
        {
            JobOffer model = new JobOffer();
            model.OfferNumber = jobOfferId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddJobOffer(JobOffer model)
        {
            if (ModelState.IsValid)
            {
                dao.SaveJobOffer(model);

                return RedirectToAction("Create", "Recruitment", new { id = model.OfferNumber });
            }
            else
            {
                return View(model);
            }

        }
        [AllowAnonymous]
        public ActionResult Show(long? id)
        {
            if (id != null)
            {
                JobOffer model = dao.GetJobOfferByOfferNumber((long) id);
                if (model != null)
                {
                    return View(model);
                }

            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult ShowJobOfferList()
        {
            IEnumerable<JobOffer> model = dao.GetJobOfferList();
            if (model.Count() > 0)
                return View(model);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(long? id)
        {
            if (id != null)
            {
                JobOffer model = dao.GetJobOfferByOfferNumber((long) id);
                if (model != null)
                {
                    return View(model);
                }

            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobOffer model)
        {

            if (ModelState.IsValid)
            {

                bool test = dao.UpdatateJobOffer(model);

                return RedirectToAction("Show", "Career", new { id = model.OfferNumber });
            }
            else
            {
                return View(model);
            }

        }

        //public ActionResult Delete(long id)
        //{

        //        dao.RemoveJobOffer((long) id);

        //    return RedirectToAction("ShowJobOfferList", "Career", null);
        //}
        public ActionResult Apply(long id)
        {
            JobOffer jo = dao.GetJobOfferByOfferNumber(id);
            ApplyViewModel avm = new ApplyViewModel();
            avm.OfferNumber = jo.OfferNumber;
            avm.Location = jo.Location;
            avm.OfferName = jo.Name;
            avm.JobDescription = jo.JobDescription;
            return View(avm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplyViewModel model)
        {


            //bool letterSaved = dao.SaveFileOnServer(Server, model.LetterFile, AppConfig.LettersFolderRelative);
            //bool photoSaved = dao.SaveFileOnServer(Server, model.PhotoFile, AppConfig.PhotosFolderRelative);
            if (ModelState.IsValid)
            {
                
                bool cvSaved = true;
                string path = null;
                if (model.CVFile != null)
                {
                    path = Utils.Utils.CreatePath(Server, model.CVFile, Utils.AppConfig.CVFolderRelative);
                    cvSaved = dao.SaveFileOnServer(path, model.CVFile);
                }
                Person person = new Person()
                {
                    CanContact = model.CanContact,
                    City = model.City,
                    CVPath = path,
                    Email = model.Email,
                    Name = model.Name,
                    PersonalDataProcessing = model.PersonalDataProcessing,
                    PhoneNumber = model.PhoneNumber,
                    Street = model.Street,
                    Surname = model.Surname,
                    Zip = model.Zip,
                    Candidates = new List<Candidate>(),
                    Notes = new List<PersonNote>(),
                    Tags = new List<SkillTag>(),                 
                };

                Recruitment recruitment = dao.GetRecruitmentById(model.OfferNumber);

                Candidate candidate = new Candidate()
                {
                    FinancialExpectations = model.FinancialExpectations,
                    Invited = false,
                    MeetsRequirements = true,
                    Person = person,
                    ReadyToMove = model.ReadyToMove,
                    Recruitment = recruitment,
                    ApplyTime = DateTime.Now
                };

                RecruitmentEvent ev = new RecruitmentEvent()
                {
                    Author = "HR Manager",
                    AuthorId = "HR Manager",
                    Recruitment = recruitment,
                    Time = DateTime.Now,
                    Event = person.GetFullName() + " zaaplikował/a na stanowisko"

                };

                
                GIDOLog gl = new GIDOLog()
                {
                    Author = person.Name+" "+person.Surname,
                    AuthorId = "",
                    Date = DateTime.Now,
                    Changes = "Zmodyfikowano dane użytkownika:" + person.ToString()
                };
                dao.SaveGIDOLog(gl);

                recruitment.Events.Add(ev);
                recruitment.Candidate.Add(candidate);

                //dao.SavePerson(person);
                //dao.SaveCandidate(candidate);
                dao.UpdatateRecruitment(recruitment);

                return View("ApplyComplete", cvSaved);
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult GetRaport(long id)
        {

            var model = dao.GetRecruitmentById(id);
            var pdf = new ViewAsPdf("RecruitmentRaportView", model);
            pdf.FileName = "Raport z rekrutacji " + model.Id;
            pdf.MasterName = "Raport z rekrutacji " + model.Id;
            pdf.RotativaOptions.IsBackgroundDisabled = false;

            return new ViewAsPdf("RecruitmentRaportView", model);
        }
    }
}
