using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testApplication.DataAccess.Repository.IRepository;
using testApplication.Models;

namespace testApplication.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(CoverType coverType)
        {
            _db.CoverTypes.Update(coverType);
        }
    }
}
