using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
    public class CorrectAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int QuestionOptionId { get; set; }
        public int SurveyId { get; set; }
    }
}
