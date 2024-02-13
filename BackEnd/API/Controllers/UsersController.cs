using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet("~/GetQuestions")]
        public async Task<ActionResult<IEnumerable<QuesAns>>> GetQuestions(int topicId, int exp)
        {
            var result = await _context.QuesAnswer
                .Where(c => c.Exp == exp && c.topicId == topicId)                
                .Select(c => new QuesAns { Id = c.Id, Question = c.Question }) 
                .ToListAsync();
                
            return Ok(result);
        }

        [HttpGet("~/GetAnswer/{id}")]
        public async Task<ActionResult<QuesAns>> GetAnswer(int id)
        {
            var result = await _context.QuesAnswer.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("~/GetTopics")]
        public async Task<ActionResult<IEnumerable<Topics>>> GetTopics()
        {
            var result = await _context.Topic
                .Select(c => new Topics { Id = c.Id, Name = c.Name }) 
                .ToListAsync();
            return Ok(result);
        }
    }
}