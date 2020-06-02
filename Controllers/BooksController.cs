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
    public class BooksController : ControllerBase {
        private readonly ILogger<BooksController> _logger;

        private readonly ApplicationDbContext _context;
        public BooksController (ILogger<BooksController> logger, ApplicationDbContext context) {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get () {
            return await _context.Books.Include(element => element.Author).ToListAsync();
        }

        [HttpGet ("{_id}", Name = "getBook")]
        public ActionResult<Book> getBook (int _id) {

            var book = _context.Books.Include(element => element.Author).FirstOrDefault (a => a.Id == _id);
            if (book == null) {
                return NotFound ();
            }

            return book;
        }

        [HttpPost]
        public ActionResult Create ([FromBody] Book book) {

            _context.Books.Add (book);
            _context.SaveChanges ();
            return new CreatedAtRouteResult ("getBook", new { _id = book.Id }, book);
        }

        [HttpPut ("{_id}")]
        public ActionResult<Book> Update (int _id, [FromBody] Book book) {
            if (_id != book.Id) {
                return BadRequest ();
            }

            _context.Entry (book).State = EntityState.Modified;
            _context.SaveChanges ();
            return Ok ();

        }

        [HttpDelete ("{_id}")]
        public ActionResult<Book> Delete (int _id) {
            var book = _context.Books.FirstOrDefault (at => at.Id == _id);
            if (book == null) {
                return NotFound ();
            }

            _context.Books.Remove (book);
            _context.SaveChanges ();
            return book;
        }
    }
}