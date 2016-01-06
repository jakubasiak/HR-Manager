using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class RecruitmentListViewModel
    {
        [Display(Name = "Id Ogłoszenia")]
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long OfferNumber { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool Current { get; set; }
    }
}