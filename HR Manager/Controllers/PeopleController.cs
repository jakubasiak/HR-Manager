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
            term = term.ToLower();
            var tagsList = dao.GetTagList()
                .Where(x => x.Tag.ToLower().Contains(term))
                .Select(x => x.Tag)
                .Distinct()
                .Take(5)
                .ToList();

            var peopleList = dao.GetPeopleList()
                .Where(x => x.Name.ToLower().Contains(term) || x.Surname.ToLower().Contains(term) || x.City.ToLower().Contains(term))
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
                candidateSearch = candidateSearch.ToLower();
                var peopleList = dao.GetPeopleList()
                    .Where(x => x.Name.ToLower().Contains(candidateSearch) || x.Surname.ToLower().Contains(candidateSearch) || x.City.ToLower().Contains(candidateSearch) || x.GetFullName().ToLower().Contains(candidateSearch));

                var tagContainTerm = dao.GetTagList()
                    .Where(x => x.Tag.ToLower().Contains(candidateSearch));

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
            Person person = dao.GetPersonById((int)id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View("PersonDetails", person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View("CreatePerson");
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonViewModel personVM)
        {
            if (ModelState.IsValid)
            {
                bool cvSaved = true;
                string path = null;
                if (personVM.CVFile != null)
                {
                    path = Utils.Utils.CreatePath(Server, personVM.CVFile, Utils.AppConfig.CVFolderRelative);
                    cvSaved = dao.SaveFileOnServer(path, personVM.CVFile);
                }
                Person person = new Person()
                {
                    CanContact = personVM.CanContact,
                    City = personVM.City,
                    CVPath = path,
                    Email = personVM.Email,
                    Name = personVM.Name,
                    PersonalDataProcessing = personVM.PersonalDataProcessing,
                    PhoneNumber = personVM.PhoneNumber,
                    Street = personVM.Street,
                    Surname = personVM.Surname,
                    Zip = personVM.Zip,
                    Candidates = new List<Candidate>(),
                    Notes = new List<PersonNote>(),
                    Tags = new List<SkillTag>(),
                };

                List<string> tagList = personVM.Tags.Split(new char[] { ';', ' ', ',' }).ToList<string>();
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

                dao.SavePerson(person);

                return RedirectToAction("Index", "People");
            }

            return View("CreatePerson", personVM);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = dao.GetPersonById((int)id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View("PersonEdit", person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                dao.UpdatatePerson(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = dao.GetPersonById((int)id);
            if (person == null)
            {
                return HttpNotFound();
            }
            dao.RemovePersonById((int)id);
            return PartialView("PeopleList", dao.GetPeopleList());
        }

        // POST: People/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Person person = db.People.Find(id);
        //    db.People.Remove(person);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}




    }
}
