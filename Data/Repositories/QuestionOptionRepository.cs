using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Repositories
{
    public class QuestionOptionRepository : Repository<QuestionOption>
    {
        public QuestionOptionRepository(Context context) : base(context)
        {
        }
       
    }
}
