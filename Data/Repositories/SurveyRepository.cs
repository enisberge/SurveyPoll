using Microsoft.EntityFrameworkCore;
using SurveryPoll.DataAccess.Contexts;
using SurveryPoll.DataAccess.Entities;
using SurveyPoll.WebUI.Models;

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
        public List<UserScoreDto> GetUserScoresBySurveyCode(string surveyCode)
        {
            var context = new Context();

            var surveyData = context.Surveys
    .Where(s => s.SurveyCode == surveyCode)
    .Include(s => s.SurveyResponses)
        .ThenInclude(sr => sr.Answers)
    .Include(s => s.CorrectAnswers)
    .FirstOrDefault();

            var userScores = new List<UserScoreDto>();

            if (surveyData != null)
            {

                foreach (var response in surveyData.SurveyResponses)
                {
                    var userScore = new UserScoreDto
                    {
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        CorrectCount = 0,
                        WrongCount = 0,
                        SurveyTitle=surveyData.Title
                    };

                    foreach (var answer in response.Answers)
                    {
                        var correctAnswer = surveyData.CorrectAnswers
                            .FirstOrDefault(cr => cr.QuestionId == answer.QuestionId);

                        if (correctAnswer != null && correctAnswer.QuestionOptionId == answer.QuestionOptionId)
                        {
                            userScore.CorrectCount++;
                        }
                        else
                        {
                            userScore.WrongCount++;
                        }
                    }

                    userScores.Add(userScore);
                }

            }

            // userScores listesini kullanarak kullanıcıların doğru ve yanlış sayılarını alabilirsiniz.

            return userScores;
        }

        public List<Survey> GetSurveyListByUserId(int userId)
        {
            var context = new Context();
            List<Survey> surveyList=context.Surveys.Where(s => s.UserId == userId).ToList();

            return surveyList;

        }
    }
}
