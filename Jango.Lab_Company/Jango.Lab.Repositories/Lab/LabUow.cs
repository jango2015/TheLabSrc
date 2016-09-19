using System.Data.Entity;
using System.Threading.Tasks;

namespace Jango.Lab.Repositories.Lab
{
    public class LabUow : ILabUow
    {
        private readonly DbContext db = EFContextFactory.GetCurrentDbContext();
        public void Commit()
        {
            db.SaveChanges();
        }

        public Task CommitAsync()
        {
            return db.SaveChangesAsync();
        }
    }
}