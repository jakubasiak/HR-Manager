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
    public class PeopleController : BaseController
    {

        // GET: People
        public ActionResult Index()
        {
            return View("PeopleList", dao.GetPeopleList());
        }

        public async Task<ActionResult> FindPerson(string term)
        {
            var tagsList = dao.GetTagList()
                .Where(x => x.Tag.Contains(term))
                .Select(x => x.Tag)
                .Distinct()
                .Take(5)
                .ToList();

            var peopleList = dao.GetPeopleList()
                .Where(x => x.Name.Contains(term) || x.Surname.Contains(term) || x.City.Contains(term))
                .Select(x => x.GetFullName())
                .Distinct().Take(5)
                .ToList();

            var resultList = tagsList.Union(peopleList);

            return Json(resultList, JsonRequestBehavior.AllowGet);

        }
        public async Task<ActionResult> SelectPersonList(string candidateSearch)
        {
            if (!string.IsNullOrEmpty(candidateSearch))
            {
                var peopleList = dao.GetPeopleList()
                    .Where(x => x.Name.Contains(candidateSearch) || x.Surname.Contains(candidateSearch) || x.City.Contains(candidateSearch) || x.GetFullName().Contains(candidateSearch));

                var tagContainTerm = dao.GetTagList()
                    .Where(x => x.Tag.Contains(candidateSearch));

                var peopleWithTag = dao.GetPeopleList().Where(x => x.Tags.Intersect(tagContainTerm).Any());

                IEnumerable<Person> model;
                if (peopleWithTag != null && peopleList != null)
                {
                    model = peopleList.Union(peopleWithTag);
                }
                else if (peopleWithTag != null)
                    model = peopleWithTag;
                else
                    model = peopleList;

                if (model == null)
                {
                    model = dao.GetPeopleList();
                    return PartialView("_SelectedPeopleList", model);
                }
                else
                {
                    return PartialView("_SelectedPeopleList", model);
                }

            }
            else
            {
                var model = dao.GetPeopleList();
                return PartialView("_SelectedPeopleList", model);
            }
        }

                // GET: People/Details/5
                public ActionResult Details(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Person person = dao.GetPersonById((int) id);
                    if (person == null)
                    {
                        return HttpNotFound();
                    }
                    return View("PersonDetails", person);
                }
        /*
                // GET: People/Create
                public ActionResult Create()
                {
                    return View();
                }

                // POST: People/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create([Bind(Include = "Id,Name,Surname,Email,PhoneNumber,Street,Zip,City,CVPath,PersonalDataProcessing,CanContact")] Person person)
                {
                    if (ModelState.IsValid)
                    {
                        db.People.Add(person);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(person);
                }

                // GET: People/Edit/5
                public ActionResult Edit(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Person person = db.People.Find(id);
                    if (person == null)
                    {
                        return HttpNotFound();
                    }
                    return View(person);
                }

                // POST: People/Edit/5
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit([Bind(Include = "Id,Name,Surname,Email,PhoneNumber,Street,Zip,City,CVPath,PersonalDataProcessing,CanContact")] Person person)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(person).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(person);
                }

                // GET: People/Delete/5
                public ActionResult Delete(int? id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Person person = db.People.Find(id);
                    if (person == null)
                    {
                        return HttpNotFound();
                    }
                    return View(person);
                }

                // POST: People/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public ActionResult DeleteConfirmed(int id)
                {
                    Person person = db.People.Find(id);
                    db.People.Remove(person);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        db.Dispose();
                    }
                    base.Dispose(disposing);
                }

            */
    }
}
