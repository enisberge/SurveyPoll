using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Repositories
{
    public class SurveyQuestionRepository : Repository<SurveyQuestion>
    {
        public SurveyQuestionRepository(Context context) : base(context)
        {
        }
       
    }
}
