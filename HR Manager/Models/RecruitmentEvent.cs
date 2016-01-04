using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR_Manager.Models
{
    public class RecruitmentEvent: IEntity
    {
        public virtual Recruitment Recruitment { get; set;}
    }
}