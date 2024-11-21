using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books  = await _context.books.ToListAsync();
            return Ok(books);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        { 
            var book = await _context.books.FindAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBook book)
        {
            _context.books.Add(new Model.Book { Title = book.title,Auther=book.auther,Genre= book.genre,PublishedYear = book.publishedYear});
            _context.SaveChanges();

            return Ok();    
        }

        [HttpPut]
        public async Task<IActionResult> Put(Model.Book model)
        {
            
            var book =await _context.books.FindAsync(model.Id);
            if (book != null)
            {
                book.Title = model.Title;
                book.Auther = model.Auther;
                book.Genre = model.Genre;
                book.PublishedYear = model.PublishedYear;

                _context.Update(book);
                _context.SaveChanges();
                return Ok();
                
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.books.FindAsync(id);
            if (book != null)
            {
                _context.books.Remove(book);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }

    public record AddBook(string title,string auther,string genre,int publishedYear);
}
