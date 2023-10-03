using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Repositories
{
    public class CorrectAnswerRepository : Repository<CorrectAnswer>
    {
        public CorrectAnswerRepository(Context context) : base(context)
        {
        }
       
    }
}
