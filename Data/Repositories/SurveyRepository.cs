using Microsoft.EntityFrameworkCore;
using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;

namespace SurveryPoll.DataAccess.Repositories
{
    public class SurveyRepository : Repository<Survey>
    {
        public SurveyRepository(Context context) : base(context)
        {
        }
        public Survey GetSurveyWithQuestionsByGuid(string surveyCode)
        {
            var context = new Context();
            var survey = context.Surveys
                .Where(s => s.SurveyCode == surveyCode)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                        .ThenInclude(q => q.QuestionOptions) // Bu kısmı ekleyin
                .Include(s => s.CorrectAnswers)
                .FirstOrDefault();


            return survey;
        }
    }
}
