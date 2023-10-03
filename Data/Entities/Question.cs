using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public bool IsDeleted { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<QuestionOption> QuestionOptions { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<SurveyQuestion> SurveyQuestions { get; set; }
    }
}
