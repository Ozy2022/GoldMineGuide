using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GoldMineGuide.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the GoldMineGuideUser class
    public class GoldMineGuideUser : IdentityUser
    {
        [PersonalData]
        public string StuffFullName { get; set; }

        [PersonalData]
        public string StuffAddress { get; set; }

        [PersonalData]
        public DateTime StuffDOB { get; set; }

        [PersonalData]
        public string MangerRole { get; set; }

    }
}
