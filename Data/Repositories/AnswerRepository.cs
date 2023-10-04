using Microsoft.EntityFrameworkCore;
using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;

namespace SurveryPoll.DataAccess.Repositories
{
    public class AnswerRepository : Repository<Answer>
    {
        public AnswerRepository(Context context) : base(context)
        {
        }
       
    }
}
