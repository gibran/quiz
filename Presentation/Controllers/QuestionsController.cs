using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.Models;
using Presentation.Services;
using Presentation.Repositories;
using Presentation.Configurations;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        QuestionService _questionService;

        public QuestionsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("default");
            var databaseName = configuration.GetSection("AppSettings:databaseName").Value;

            var options = new MongoDbOptions(connectionString, databaseName, CollectionIds.Questions);
            var repository = new GenericRepository<Question>(options);

            _questionService = new QuestionService(repository);
        }

        [HttpGet]
        public async Task<IEnumerable<Question>> Get()
        {
            try
            {
                return await _questionService.List();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<Question> GetById(string id)
        {
            try
            {
                return await _questionService.GetById(id);
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<string> Create([FromBody] Question question)
        {
            try
            {
                var result = await _questionService.Save(question);
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
                var result = await _questionService.Delete(id);
                return result;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        public async Task<string> Update([FromBody] Question question)
        {
            try
            {
                var result = await _questionService.Save(question);
                return result;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}