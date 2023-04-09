using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApplication.Models;

namespace testApplication.DataAccess.Repository.IRepository
{
    public interface ICategory : IRepository<Category>
    {
        void update(Category category);
    }
}
