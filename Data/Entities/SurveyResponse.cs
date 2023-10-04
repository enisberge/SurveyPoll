using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Entities
{
   public class SurveyResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public int QuestionId{ get; set; }
        public int QuestionOptionId { get; set; }
        public string SurveyCode { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
