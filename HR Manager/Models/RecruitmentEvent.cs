using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class RecruitmentEvent: IEntity
    {
        public virtual Recruitment Recruitment { get; set; }

        [Display(Name = "Czas")]
        [Required]
        public DateTime Time { get; set; }

        [Display(Name = "Autor")]
        [Required]
        [DefaultValue("HR Manager")]
        public string Author { get; set; }

        [Required]
        [DefaultValue("HR Manager")]
        [Display(Name = "Id Autora")]
        public string AuthorId { get; set; }

        [Display(Name = "Treść")]
        [Required]
        public string Event { get; set; }

    }
}