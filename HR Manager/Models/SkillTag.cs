using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class SkillTag:IEntity
    {
        [Display(Name ="Tag")]
        [MaxLength(250,ErrorMessage ="Maksymalna długość tagu to 250 znaków.")]
        public string Tag { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}