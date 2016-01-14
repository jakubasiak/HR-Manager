using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HR_Manager.Models
{
    public interface IDataAccess
    {
        bool SaveJobOffer(JobOffer model);
        bool UpdatateJobOffer(JobOffer model);
        //bool RemoveJobOffer(long id);
        JobOffer GetJobOfferByOfferNumber(long id);
        IEnumerable<JobOffer> GetJobOfferList();
        
        bool SaveRecruitment(Recruitment recr);
        bool RemoveRecruitmentById(long id);
        Recruitment GetRecruitmentById(long id);
        IEnumerable<Recruitment> GetRecruitmentsList();

        bool SaveRecruitmentEvwnt(RecruitmentEvent ev);
        bool UpdatateRecruitmentEvent(RecruitmentEvent ev);
        bool RemoveRecruitmentEvent(int id);
        RecruitmentEvent GetRecruitmentEventById(int id);

        bool SaveFileOnServer(HttpServerUtilityBase server, HttpPostedFileBase file, string folderPath);

    }

}
