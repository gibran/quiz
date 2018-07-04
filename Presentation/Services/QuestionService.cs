using System;
using Presentation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Repositories;

namespace Presentation.Services
{
    public class QuestionService
    {
        IGenericRepository<Question> _genericRepository;

        public QuestionService(IGenericRepository<Question> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<string> Save(Question data)
        {
            try
            {
                data.Id = Guid.NewGuid().ToString();
                data.IsActive = true;

                var result = await _genericRepository.Save(data);

                return data.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var data = await _genericRepository.GetById(id);

                data.IsActive = false;

                return await _genericRepository.Save(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Question> GetById(string id)
        {
            try
            {
                var data = await _genericRepository.GetById(id);

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Question>> List()
        {
            try
            {
                var result = await _genericRepository.List();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
