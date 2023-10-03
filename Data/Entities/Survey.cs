using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string SurveyCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public List<CorrectAnswer> CorrectAnswers { get; set; }

        public List<SurveyQuestion> SurveyQuestions { get; set; }

    }
}
