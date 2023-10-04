using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int QuestionOptionId { get; set; }
        public int SurveyResponseId{ get; set; }
        public SurveyResponse SurveyResponse { get; set; }
    }
}
