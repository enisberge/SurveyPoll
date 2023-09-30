using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
    public class AppRole:IdentityRole<int>
    {
        public DateTime CreatedDate{ get; set; }
    }
}
