using HR_Manager.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Models
{
    public class DAO : IDataAccess
    {
        private ApplicationDbContext db;


        public DAO()
        {
            db = new ApplicationDbContext();
        }


        #region JobOffer
        public bool SaveJobOffer(JobOffer jobOffer)
        {
            try
            {
                db.JobOffers.Add(jobOffer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //WriteErrorLog("Nieudany zapis ogłoszenia do bazy danych " + DateTime.Now);
                return false;
            }

        }

        public bool UpdatateJobOffer(JobOffer model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                //WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }

        public JobOffer GetJobOfferByOfferNumber(long id)
        {
            //return db.JobOffers.Where(oferr => oferr.OfferNumber.Equals(id)).First();
            return db.JobOffers.Find(id);
        }

        public IEnumerable<JobOffer> GetJobOfferList()
        {
            return db.JobOffers;
        }

        #endregion

        #region Recruitment

        public bool SaveRecruitment(Recruitment recr)
        {
            try
            {
                foreach (var ev in recr.Events)
                {
                    if (GetRecruitmentEventById(ev.Id) == null)
                    {
                        SaveRecruitmentEvwnt(ev);
                    }
                    else
                    {
                        UpdatateRecruitmentEvent(ev);
                    }
                }

                db.Recruitments.Add(recr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //WriteErrorLog("Nieudany zapis rekrutacji");
                return false;
            }
        }
        public Recruitment GetRecruitmentById(long id)
        {
            return db.Recruitments.First(x => x.Id == id);
        }

        public bool RemoveRecruitmentById(long id)
        {
            try
            {
                JobOffer jo = GetJobOfferByOfferNumber(id);
                db.JobOffers.Remove(jo);

                Recruitment r = db.Recruitments.Find(id);

                db.Recruitments.Remove(r);
                db.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                string s = e.StackTrace;
                //WriteErrorLog("Nieudany usunięcie ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        public bool UpdatateRecruitment(Recruitment rec)
        {
            try
            {
                db.Entry(rec).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                //WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }


        public IEnumerable<Recruitment> GetRecruitmentsList()
        {
            return db.Recruitments;
        }

        #endregion

        #region RecruitmentEvent
        public bool SaveRecruitmentEvwnt(RecruitmentEvent ev)
        {
            try
            {
                db.RecruitmentEvents.Add(ev);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //WriteErrorLog("Nieudany zapis ogłoszenia do bazy danych " + DateTime.Now);
                return false;
            }
        }

        public bool UpdatateRecruitmentEvent(RecruitmentEvent ev)
        {
            try
            {
                db.Entry(ev).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch
            {
                //WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        public bool RemoveRecruitmentEvent(int id)
        {
            try
            {
                RecruitmentEvent re = db.RecruitmentEvents.Find(id);
                db.RecruitmentEvents.Remove(re);
                db.SaveChanges();
                return true;

            }
            catch
            {
                //WriteErrorLog("Nieudany usunięcie ogłoszenia " + DateTime.Now);
                return false;
            }

        }

        public RecruitmentEvent GetRecruitmentEventById(int id)
        {
            return db.RecruitmentEvents.Find(id);
        }
        #endregion

        #region Candidate
        public bool SaveCandidate(Candidate candidate)
        {
            try
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public Candidate GetCandidateById(int id)
        {
            return db.Candidates.First(x => x.Id == id);
        }
        public bool UpdatateCandidate(Candidate candidate)
        {
            try
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        #endregion

        #region Person
        public bool SavePerson(Person person)
        {
            try
            {
                db.People.Add(person);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public Person GetPersonById(int id)
        {
            return db.People.First(x => x.Id == id);
        }
        public bool UpdatatePerson(Person person)
        {
            try
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        public IEnumerable<Person> GetPeopleList()
        {
            return db.People;
        }
        public bool RemovePersonById(int id)
        {
            try
            {
                Person pe = GetPersonById(id);
                IEnumerable<PersonNote> pn = pe.Notes;
                db.Notes.RemoveRange(pn);

                IEnumerable<SkillTag> st = pe.Tags;
                db.Tags.RemoveRange(st);

                IEnumerable<Candidate> cn = pe.Candidates;
                db.Candidates.RemoveRange(cn);

                db.People.Remove(pe);
                db.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                string s = e.StackTrace;
                //WriteErrorLog("Nieudany usunięcie ogłoszenia " + DateTime.Now);
                return false;
            }
        }

        #endregion

        #region PersonNote
        public PersonNote GetPersonNoteById(int id)
        {
            return db.Notes.First(x => x.Id == id);
        }

        public bool RemovePersonNote(int id)
        {
            try
            {
                PersonNote pn = db.Notes.Find(id);
                db.Notes.Remove(pn);
                db.SaveChanges();
                return true;

            }
            catch
            {
                //WriteErrorLog("Nieudany usunięcie ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        public bool UpdatatePersonNote(PersonNote pn)
        {
            try
            {
                db.Entry(pn).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        #endregion

        #region Pozostałe publiczne

        public bool SaveFileOnServer(string path, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        #endregion

        #region Tag
        public SkillTag GetTagByTagName(string TagName)
        {
            try
            {
                SkillTag tag = db.Tags.Where(x => x.Tag == TagName).FirstOrDefault();

                if (tag != null)
                    return tag;
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public SkillTag GetTagById(int id)
        {
            return db.Tags.First(x => x.Id == id);
        }
        public IQueryable<SkillTag> GetTagList()
        {
            return db.Tags;
        }
        public bool RemoveTagById(int id)
        {
            try
            {
                SkillTag st = GetTagById(id);
                db.Tags.Remove(st);

                db.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                //WriteErrorLog("Nieudany usunięcie ogłoszenia " + DateTime.Now);
                return false;
            }
        }
        #endregion

        #region GIDO
        public bool SaveGIDOLog(GIDOLog gl)
        {
            try
            {
                db.GiDOlog.Add(gl);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region Pozostałe prywatne



        private void WriteErrorLog(string errorInfo)
        {
            ErrorLog el = new ErrorLog() { Tiem = DateTime.Now, Error = errorInfo };
            db.Errors.Add(el);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            db.Dispose();
        }

        #endregion

    }
}