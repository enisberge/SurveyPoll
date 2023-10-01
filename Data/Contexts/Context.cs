using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurveryPoll.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Contexts
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {

      
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Question>Questions { get; set; }
        public DbSet<QuestionOption>QuestionOptions { get; set; }
        public DbSet<Survey> Surveys{ get; set; }
        public DbSet<CorrectAnswer>CorrectAnswers { get; set; }


        //public Context(DbContextOptions<Context> options):base(options) { 

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(LocalDb)\\MSSQLLocalDB;database=SurveyPoll; integrated security=true");
        }
    }
}
