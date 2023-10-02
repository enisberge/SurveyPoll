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
            var questionsWithUsers = context.Questions
    .Include(q => q.AppUser)
    .Include(q => q.QuestionOptions)
    .Where(q => !q.IsDeleted)
    .ToList();
            return questionsWithUsers;
        }
        public List<Question> GetQuestionsForUser(int userId)
        {
            var context = new Context();
            // Veritabanından kullanıcının eklediği soruları çekin
            var userQuestions = context.Questions
                .Include(q => q.AppUser)
    .Include(q => q.QuestionOptions)
    .Where(q => !q.IsDeleted)
                .Where(q => q.AppUserId == userId && !q.IsDeleted)
                .ToList();

            return userQuestions;
        }
        public Question GetQuestionWithQuestionOptionsById(int id)
        {
            var context = new Context();
            var question = context.Questions
                    .Where(q => q.Id == id)
                    .Include(q => q.QuestionOptions)
                    .FirstOrDefault();

            return question;

        }
    }
}
