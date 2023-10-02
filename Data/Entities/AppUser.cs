using Microsoft.AspNetCore.Identity;
using SurveryPoll.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string? AvatarPath { get; set; }
        public string? Address { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Question> Questions { get; set; }
    }
}
