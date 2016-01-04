using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class Candidate:IEntity
    {

        [Display(Name = "Dane osobowe")]
        public virtual Person Person { get; set; }

        [Display(Name = "Oczekiwania finansowe")]
        public string FinancialExpectations { get; set; }

        [Display(Name = "Zgoda na relokacje")]
        public bool ReadyToMove { get; set; }

        [Display(Name = "Spełnia wymagania")]
        public bool MeetsRequirements { get; set; }

        [Display(Name = "Zaproszony na spotkanie")]
        public bool Invited { get; set; }


        public virtual ICollection<Recruitment> Recruitments { get; set; }
        public virtual ICollection<Candidate> Comments { get; set; }
    }
}