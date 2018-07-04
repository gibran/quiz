using System.Threading.Tasks;
using System.Collections.Generic;

namespace Presentation.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<bool> Save(T data);

        Task<T> GetById(string id);

        Task<List<T>> List();
    }
}
