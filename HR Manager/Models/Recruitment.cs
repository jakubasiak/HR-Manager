using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class Recruitment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Display(Name = "Id Ogłoszenia")]
        public virtual JobOffer JobOffer { get; set; }
        [Display(Name = "Dta rozpoczęcia")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Dta zakończenia")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Kandydaci")]
        public virtual IEnumerable<Candidate> Candidate { get; set; }
        [Display(Name = "Zdarzenia")]
        public virtual IEnumerable<RecruitmentEvent> Events { get; set; }

       
    }
}