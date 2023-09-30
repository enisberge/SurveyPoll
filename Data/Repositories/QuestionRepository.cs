using Microsoft.EntityFrameworkCore;
using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveryPoll.DataAccess.Repositories
{
    public class QuestionRepository : Repository<Question>
    {
        public QuestionRepository(Context context) : base(context)
        {
        }
        public List<Question> GetAllQuestionsWithOptions()
        {
            var context = new Context();
            return context.Questions.Include(q => q.QuestionOptions).ToList();
        }
    }
}
