using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor.SankoreAPI.Database;
using Blazor.SankoreAPI.Models.Domain;
using AutoMapper;
using Blazor.SankoreAPI.Models.DataTransfer.Book;
using AutoMapper.QueryableExtensions;
using Blazor.SankoreAPI.Static;
using static System.Reflection.Metadata.BlobBuilder;
using Blazor.SankoreAPI.Models.DataTransfer;

namespace Blazor.SankoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookRepoContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BookRepoContext context, IMapper mapper, ILogger<BooksController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            try
            {
                _logger.LogInformation($"making request call to {nameof(GetBooks)}");
                var books = await _context.Books.Include(y => y.Author)
                    .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetBooks)}");
                return StatusCode(500, Messages.Error500Mesage);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            try
            {
                if (_context.Books == null)
                {
                    _logger.LogWarning($"No {nameof(Book)} record was returned for request for Id: {id} in {nameof(GetBook)}");
                    return NotFound();
                }
                var myBook = await _context.Books
                    .Include(y => y.Author)
                    .ProjectTo<BookDetailsDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (myBook==null)
                {
                    _logger.LogWarning($"No {nameof(Book)} record was returned for request for Id: {id} in {nameof(GetBook)}");
                    return NotFound();
                }
                ////var book = await _context.Books.Include(y => y.Author).Where(x => x.Id == id).SingleOrDefaultAsync();
                ////var bookReadOnlyDto = _mapper.Map<BookDetailsDto>(book);

                _logger.LogInformation($"Request for {nameof(Book)} Id: {id} with GET operation call to {nameof(GetBook)}");
                return Ok(myBook);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation for Id: {id} in {nameof(GetBook)}");
                return StatusCode(500, Messages.Error500Mesage);
            }
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookUpdateDto)
        {
            if (id != bookUpdateDto.Id)
            {
                _logger.LogWarning($"Update ID invalid in {nameof(PutBook)} - Id {id}");
                return BadRequest();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                _logger.LogWarning($"{nameof(Book)} record not found in {nameof(PutBook)} - Id {id}");
                return BadRequest();
            }
            _mapper.Map(bookUpdateDto, book);
            _context.Entry(bookUpdateDto).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exep)
            {
                if (!BookExists(id))
                {
                    _logger.LogError(exep, $"{nameof(Book)} record not found in {nameof(PutBook)} for ID - {id}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookCreateDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookCreateDto);

                if (book == null)
                {
                    _logger.LogWarning($"{nameof(Book)} record not found in {nameof(PostBook)}");
                    return BadRequest();
                }

                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error when creating {nameof(Book)} record in {nameof(PutBook)}");
                return BadRequest();
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
       try
            {
                if (_context.Books == null)
                {
                    _logger.LogWarning($"{nameof(Book)} records were not found");
                    return NotFound();
                }

                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"{nameof(Book)} record for Id: {id} was not found");
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error in DELETE operation for Id: {id} in {nameof(DeleteBook)}");
                return BadRequest();
            }
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
