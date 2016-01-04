using HR_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Controllers
{
    public class RecruitmentController : BaseController
    {
        //// GET: Recruitment
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: Recruitment/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Recruitment/Create
        public ActionResult Create()
        {
            Recruitment recr = new Recruitment();
            recr.Id = Utils.Utils.GenerateJobOfferId();

            return View("CreateAndShow", recr);
        }

        // POST: Recruitment/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Recruitment/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Recruitment/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Recruitment/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Recruitment/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
