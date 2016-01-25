using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}