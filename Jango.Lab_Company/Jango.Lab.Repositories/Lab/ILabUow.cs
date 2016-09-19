using System.Threading.Tasks;

namespace Jango.Lab.Repositories.Lab
{
    public interface ILabUow
    {
        void Commit();
        Task CommitAsync();
    }
}