using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Manager.Models
{
    public class JobOffer
    {
        [Key]
        [Required]
        [Display(Name ="Nr. Ogłoszenia")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long OfferNumber { get; set; }

        [Display(Name = "Nazwa stanowiska")]
        [Required(ErrorMessage ="Nazwa stanowiska jest wymagana ")]
        public string Name { get; set; }

        [Display(Name = "Miejsce pracy")]
        public string Location { get; set; }

        [Display(Name = "Informacje o pracodawcy")]
        [AllowHtml]
        public string AboutCompany { get; set; }
        
        [AllowHtml]
        [Display(Name = "Opis stanowiska")]
        [Required(ErrorMessage = "Opis stanowiska jest wymagany")]
        public string JobDescription { get; set; }

        [Display(Name = "Oczekiwania")]
        [AllowHtml]
        public string Expectations { get; set; }

        [Display(Name = "Oferujemy")]
        [AllowHtml]
        public string Offer { get; set; }

        [Display(Name = "Status")]
        [DefaultValue(true)]
        public bool Current { get; set; }


    }


}