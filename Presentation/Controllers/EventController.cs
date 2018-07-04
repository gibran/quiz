using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Microsoft.Extensions.Configuration;
using Presentation.Services;
using Presentation.Repositories;

namespace Presentation.Controllers
{

    [Route("api/[controller]")]
    public class EventController : Controller
    {
        IConfiguration _configuration;
        EventService _eventService;

        public EventController(IConfiguration configuration)
        {
            _configuration = configuration;

            var repository = new GenericRepository<Event>();

            _eventService = new EventService(repository);
        }


        [HttpGet]
        public async Task<IEnumerable<Event>> Get()
        {
            try
            {
                return await _eventService.List();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Event> GetById(string id)
        {
            try
            {
                return await _eventService.GetById(id);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<string> Create([FromBody] Event question)
        {
            try
            {
                var result = await _eventService.Save(question);
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            try
            {
                var result = await _eventService.Delete(id);
                return result;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        public async Task<string> Update([FromBody] Event question)
        {
            try
            {
                var result = await _eventService.Save(question);
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}