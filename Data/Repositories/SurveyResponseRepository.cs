using Microsoft.EntityFrameworkCore;
using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;

namespace SurveryPoll.DataAccess.Repositories
{
    public class SurveyResponseRepository : Repository<SurveyResponse>
    {
        public SurveyResponseRepository(Context context) : base(context)
        {
        }
        public int GetSurveyResponseCount(string surveyCode)
        {
            var context = new Context();
            var count = context.SurveyResponses
                .Where(e => e.SurveyCode==surveyCode)
                .Count();
            return count;
        }



    }
}
