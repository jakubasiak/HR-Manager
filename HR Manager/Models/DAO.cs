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
    public class DAO: IDataAccess
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
                WriteErrorLog("Nieudany zapis ogłoszenia do bazy danych " + DateTime.Now);
                return false;
            }

        }

        public bool UpdatateJobOffer(JobOffer model)
        {
            try {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }catch
            {
                WriteErrorLog("Nieudana aktualizacja ogłoszenia " + DateTime.Now);
                return false;
            }
        }

        public bool RemoveJobOffer(long id)
        {
            try
            {
                JobOffer jo = db.JobOffers.Find(id);
                db.JobOffers.Remove(jo);
                db.SaveChanges();
                return true;

            }
            catch
            {
                WriteErrorLog("Nieudany usunięcie ogłoszenia " + DateTime.Now);
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
                db.Recruitments.Add(recr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                WriteErrorLog("Nieudany zapis rekrutacji " + DateTime.Now);
                return false;
            }
        }

        #endregion

        #region Pozostałe publiczne

        public bool SaveFileOnServer(HttpServerUtilityBase server, HttpPostedFileBase file, string folderPath)
        {
            try
            {
                if (IsFileCorrect(file))
                {
                    var fileExt = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid() + fileExt;

                    var path = Path.Combine(server.MapPath(folderPath), fileName);
                    file.SaveAs(path);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Pozostałe prywatne

        private bool IsFileCorrect(HttpPostedFileBase file)
        {
            bool isNotNull = file != null ? true : false;
            bool isBiggerThenZero = file.ContentLength > 0 ? true : false;
            bool isNotTooBig = file.ContentLength < (5 * 1024 * 1024) ? true : false;
            bool isImage = Path.GetExtension(file.FileName).ToLower().Equals(".jpg") ||
                Path.GetExtension(file.FileName).ToLower().Equals(".jpeg") ||
                Path.GetExtension(file.FileName).ToLower().Equals(".png") ||
                Path.GetExtension(file.FileName).ToLower().Equals(".gif");
            bool isPdf = Path.GetExtension(file.FileName).ToLower().Equals(".pdf");

            if (isNotNull && isBiggerThenZero && isNotTooBig && (isImage || isPdf))
                return true;
            else
                return false;
        }

        private void WriteErrorLog(string errorInfo)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(AppConfig.AppErrorLog, true))
            {
                file.WriteLine(errorInfo);
            }
        }

        protected void Dispose(bool disposing)
        {
            db.Dispose();
        }

        #endregion

    }
}