using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Presentation.Models;
using Presentation.Repositories;

namespace Presentation.Services
{
    public class EventService
    {
        IGenericRepository<Event> _genericRepository;

        public EventService(IGenericRepository<Event> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<string> Save(Event data)
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

                data.IsPublished = false;
                data.IsActive = false;

                return await _genericRepository.Save(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Event> GetById(string id)
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

        public async Task<List<Event>> List()
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
