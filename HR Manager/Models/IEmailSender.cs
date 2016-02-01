using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Manager.Models
{
    interface IEmailSender
    {
        bool SendEmail(ContactViewModel model, ApplicationUser user, string emailReceiver);
    }
}
