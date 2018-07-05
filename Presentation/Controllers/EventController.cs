using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Microsoft.Extensions.Configuration;
using Presentation.Services;
using Presentation.Repositories;
using Presentation.Configurations;

namespace Presentation.Controllers
{

    [Route("api/[controller]")]
    public class EventController : Controller
    {
        EventService _eventService;

        public EventController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("default");
            var databaseName = configuration.GetSection("databaseName").Value;

            var options = new MongoDbOptions(connectionString, databaseName, CollectionIds.Event);

            var repository = new GenericRepository<Event>(options);

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
        public async Task<string> Create([FromBody] Event @event)
        {
            try
            {
                var result = await _eventService.Save(@event);
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
        public async Task<string> Update([FromBody] Event @event)
        {
            try
            {
                var result = await _eventService.Save(@event);
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}