using System.Threading.Tasks;
using System.Collections.Generic;
using Presentation.Configurations;
using Presentation.Models;

namespace Presentation.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : IDocument
    {
        protected MongoDbRepository<T> _repository;

        public GenericRepository(MongoDbOptions options)
        {
            _repository = new MongoDbRepository<T>(options);
        }

        public async Task<bool> Save(T data)
        {
            await _repository.CreateOrUpdate(data);
            return true;
        }

        public async Task<T> GetById(string id)
        {
            var result = await _repository.GetDocument(id);

            return result;
        }

        public List<T> List()
        {
            var result = _repository.GetAllDocuments();
            return result;
        }
    }
}
