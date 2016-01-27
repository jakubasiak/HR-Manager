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
        #region JobOffer

        bool SaveJobOffer(JobOffer model);
        bool UpdatateJobOffer(JobOffer model);
        //bool RemoveJobOffer(long id);
        JobOffer GetJobOfferByOfferNumber(long id);
        IEnumerable<JobOffer> GetJobOfferList();

        #endregion

        #region Recruitment

        bool SaveRecruitment(Recruitment recr);
        bool RemoveRecruitmentById(long id);
        Recruitment GetRecruitmentById(long id);
        bool UpdatateRecruitment(Recruitment rec);
        IEnumerable<Recruitment> GetRecruitmentsList();
        #endregion

        #region RecruitmentEvent
        bool SaveRecruitmentEvwnt(RecruitmentEvent ev);
        bool UpdatateRecruitmentEvent(RecruitmentEvent ev);
        bool RemoveRecruitmentEvent(int id);
        RecruitmentEvent GetRecruitmentEventById(int id);
        #endregion

        #region Candidate
        bool SaveCandidate(Candidate candidate);
        Candidate GetCandidateById(int id);
        bool UpdatateCandidate(Candidate candidate);
        #endregion

        #region Person
        bool SavePerson(Person person);
        Person GetPersonById(int id);
        bool UpdatatePerson(Person person);
        #endregion

        #region PersonNote
        PersonNote GetPersonNoteById(int id);
        bool RemovePersonNote(int id);
        bool UpdatatePersonNote(PersonNote pn);
        #endregion

        #region Tag
        SkillTag GetTagByTagName(string TagName);
        SkillTag GetTagById(int id);
        IQueryable<SkillTag> GetTagList();
        #endregion

        bool SaveFileOnServer(string path, HttpPostedFileBase file);
        



    }

}
