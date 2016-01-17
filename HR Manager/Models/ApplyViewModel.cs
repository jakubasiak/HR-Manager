using HR_Manager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Models
{
    public class ApplyViewModel
    {
        [Display(Name = "Nr. Ogłoszenia")]
        public long OfferNumber { get; set; }

        [Display(Name = "Nazwa stanowiska")]
        public string OfferName { get; set; }

        [Display(Name = "Miejsce pracy")]
        public string Location { get; set; }

        [AllowHtml]
        [Display(Name = "Opis stanowiska")]
        public string JobDescription { get; set; }

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

        //[Display(Name = "List motywacyjny")]
        //public HttpPostedFileBase LetterFile { get; set; }

        //[Display(Name = "Zdjęcie")]
        //public HttpPostedFileBase PhotoFile { get; set; }

        [Display(Name = "Oczekiwania finansowe (brutto)")]
        public string FinancialExpectations { get; set; }

        [Display(Name = "Zgoda na relokacje")]
        public bool ReadyToMove { get; set; }

        
        [TrueRequired(ErrorMessage ="Zgoda na przetwarzanie danych jest wymagana")]
        [Display(Name = "Zgoda na przetwarzanie danych osobowych")]
        public bool PersonalDataProcessing { get; set; }

        [Display(Name = "Zgoda na kontakt")]
        public bool CanContact { get; set; }
    }
}