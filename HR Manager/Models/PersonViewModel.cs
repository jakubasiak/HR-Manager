using HR_Manager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class PersonViewModel
    {

      
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Surname { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ulica, nr. domu, nr.lokalu")]
        public string Street { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string Zip { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        //[Display(Name = "CV")]
        public HttpPostedFileBase CVFile { get; set; }

        [Display(Name = "Tagi")]
        public string Tags { get; set; }

        [TrueRequired(ErrorMessage = "Zgoda na przetwarzanie danych jest wymagana")]
        [Display(Name = "Zgoda na przetwarzanie danych osobowych")]
        public bool PersonalDataProcessing { get; set; }

        [Display(Name = "Zgoda na kontakt")]
        public bool CanContact { get; set; }
    }
}