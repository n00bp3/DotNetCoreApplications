using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApplication.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategory categoryRepository { get; }
        ICoverTypeRepository coverTypeRepository { get; }
        IProduct productRepository { get; }
        void save();
    }
}
