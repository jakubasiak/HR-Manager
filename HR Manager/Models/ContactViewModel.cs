using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class ContactViewModel
    {
        public int PersonId { get; set; }

        [Display(Name ="Adres zwrotny:")]
        public string ReturnAddress { get; set; }
        [Display(Name = "Temat:")]
        public string MessageSubject { get; set; }
        [Display(Name = "Wiadomość:")]
        public string Message { get; set; }
    }
}