using HR_Manager.Models;
using HR_Manager.Utils;
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
        //public ActionResult Apply(long id)
        //{
        //    JobOffer jo = dao.GetJobOfferByOfferNumber(id);
        //    ApplyViewModel avm = new ApplyViewModel();
        //    avm.OfferNumber = jo.OfferNumber;
        //    avm.Location = jo.Location;
        //    avm.OfferName = jo.Name;
        //    avm.JobDescription = jo.JobDescription;
        //    return View(avm);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplyViewModel model)
        {

            
            //bool letterSaved = dao.SaveFileOnServer(Server, model.LetterFile, AppConfig.LettersFolderRelative);
            //bool photoSaved = dao.SaveFileOnServer(Server, model.PhotoFile, AppConfig.PhotosFolderRelative);

            if (ModelState.IsValid)
            {
                bool cvSaved = true;
                if (model.CVFile!=null)
                    cvSaved = dao.SaveFileOnServer(Server, model.CVFile, AppConfig.CVFolderRelative);

                return View("ApplyComplete", cvSaved);
            }
            else
            {
                return View(model);
            }
        }
    }
}
