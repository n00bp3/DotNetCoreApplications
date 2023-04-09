using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApplication.DataAccess.Repository.IRepository;
using testApplication.Models;

namespace testApplication.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategory
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
