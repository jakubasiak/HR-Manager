using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class TagListViewModel
    {
        public ICollection<SkillTag> Tags { get; set; }
        public int PersonId { get; set; }
    }
}