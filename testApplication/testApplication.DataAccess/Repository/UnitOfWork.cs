using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApplication.DataAccess.Repository.IRepository;

namespace testApplication.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            categoryRepository = new CategoryRepository(_db);
            coverTypeRepository = new CoverTypeRepository(_db);
            productRepository = new ProductRepository(_db);
        }
        public ICategory categoryRepository { get; private set; }
        public ICoverTypeRepository coverTypeRepository { get; private set; }
        public IProduct productRepository { get; private set; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
