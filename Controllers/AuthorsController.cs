using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lab1_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lab1_api.Controllers {

    [Route ("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase {
        private readonly ILogger<AuthorsController> _logger;

        private readonly ApplicationDbContext _context;
        public AuthorsController (ILogger<AuthorsController> logger, ApplicationDbContext context) {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get () {
            _logger.LogInformation("get all authors");
            return await _context.Authors.Include(element => element.Books).ToListAsync();
        }

        [HttpGet ("{_id}", Name = "getAuthor")]
        public ActionResult<Author> getAuthor (int _id) {

            var author = _context.Authors.Include(element => element.Books).FirstOrDefault (a => a.Id == _id);
            Console.WriteLine ("Author {0}", author);
            if (author == null) {
                this._logger.LogWarning("id {0} not found", _id);
                return NotFound ();
            }

            return author;
        }

        [HttpPost]
        public ActionResult Create ([FromBody] Author author) {

            _context.Authors.Add (author);
            _context.SaveChanges ();
            return new CreatedAtRouteResult ("getAuthor", new { _id = author.Id }, author);
        }

        [HttpPut ("{_id}")]
        public ActionResult<Author> Update (int _id, [FromBody] Author author) {
            if (_id != author.Id) {
                return BadRequest ();
            }

            _context.Entry (author).State = EntityState.Modified;
            _context.SaveChanges ();
            return Ok ();

        }

        [HttpDelete ("{_id}")]
        public ActionResult<Author> Delete (int _id) {
            var author = _context.Authors.FirstOrDefault (at => at.Id == _id);
            if (author == null) {
                return NotFound ();
            }

            _context.Authors.Remove (author);
            _context.SaveChanges ();
            return author;
        }
    }
}