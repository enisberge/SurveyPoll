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
    public class CategoryRepository:Repository<Category>
    {
        public CategoryRepository(Context context) : base(context)
        {
        }

        public List<Category> GetActiveCategories()
        {
            var context = new Context();
            // IsDeleted özelliği false olan kategorileri çekmek için LINQ sorgusu
            return context.Categories.Where(c => !c.IsDeleted).ToList();
        }
    }
}
