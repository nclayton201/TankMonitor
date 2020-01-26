using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TankMonitor.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the TankMonitorUser class
    public class TankMonitorUser : IdentityUser
    {
        [PersonalData, Required, StringLength(40)]
        public string FirstName { get; set; }

        [PersonalData, Required, StringLength(40)]
        public string LastName { get; set; }
    }
}
