using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class PersonNote: IEntity
    {
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Display(Name = "Id autora")]
        public string AuthorId { get; set; }
        [Display(Name = "Komentarz")]
        public string Comment { get; set; }

        public virtual Candidate Candidate { get; set; }

    }
}