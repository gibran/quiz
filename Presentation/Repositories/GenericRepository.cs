using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Presentation.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        public async Task<bool> Save(T data)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> List()
        {
            throw new NotImplementedException();
        }
    }
}
