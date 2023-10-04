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
        //GetAllQuestionsWithOptions
        public List<Question> GetAllApprovedQuestions()
        {
            var context = new Context();
            var approvedQuestions = context.Questions
                .Include(q => q.AppUser)
                .Include(q => q.QuestionOptions)
                .Where(q => !q.IsDeleted && q.Status == 2)
                .OrderByDescending(q => q.CreatedDate)
                .ToList();
            return approvedQuestions;
        }
        public List<Question> GetAllRejectQuestions()
        {
            var context = new Context();
            var rejectQuestions = context.Questions
                .Include(q => q.AppUser)
                .Include(q => q.QuestionOptions)
                .Where(q => !q.IsDeleted && (q.Status == 3))
                .OrderByDescending(q => q.CreatedDate)
                .ToList();
            return rejectQuestions;
        }
        public List<Question> GetAllApprovedOrPendingQuestions()
        {
            var context = new Context();
            var approvedOrPendingQuestions = context.Questions
                .Include(q => q.AppUser)
                .Include(q => q.QuestionOptions)
                .Where(q => !q.IsDeleted && (q.Status == 1 || q.Status == 2))
                .OrderByDescending(q => q.CreatedDate)
                .ToList();
            return approvedOrPendingQuestions;
        }
        public List<Question> GetQuestionsForUser(int userId)
        {
            var context = new Context();
            var userQuestions = context.Questions
         .Include(q => q.AppUser)
         .Include(q => q.QuestionOptions)
         .Where(q => !q.IsDeleted && (q.Status == 1 || q.Status == 2))
         .Where(q => q.AppUserId == userId && !q.IsDeleted)
         .OrderByDescending(q => q.CreatedDate)
         .ToList();

            return userQuestions;
        }
        public List<Question> GetRejectQuestionForUser(int userId)
        {
            var context = new Context();
            var userQuestions = context.Questions
         .Include(q => q.AppUser)
         .Include(q => q.QuestionOptions)
         .Where(q => !q.IsDeleted && ( q.Status == 3))
         .Where(q => q.AppUserId == userId && !q.IsDeleted)
         .OrderByDescending(q => q.CreatedDate)
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
