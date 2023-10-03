using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;

namespace SurveryPoll.DataAccess.Repositories
{
    public class SurveyRepository : Repository<Survey>
    {
        public SurveyRepository(Context context) : base(context)
        {
        }
      
    }
}
