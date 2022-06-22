using AutoMapper;
using Blazor.SankoreAPI.Database;
using Blazor.SankoreAPI.Models.DataTransfer.Author;
using Blazor.SankoreAPI.Models.Domain;
using Blazor.SankoreAPI.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.SankoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly BookRepoContext _context;
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;

        public AuthorsController(BookRepoContext context, ILogger<AuthorsController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
            _logger.LogInformation($"making request call to {nameof(GetAuthors)}");
            try
            {

                if (_context.Authors == null)
                {
                    _logger.LogWarning($"Records not found for GET operation in {nameof(GetAuthors)}");
                    return NotFound();
                }
                IEnumerable<AuthorReadOnlyDto>? authors = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(await _context.Authors.ToListAsync());
                return Ok(authors);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Mesage);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnlyDto>> GetAuthor(int id)
        {
            try
            {
                Author? author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(GetAuthor)} - Id {id}");
                    return NotFound();
                }

                AuthorReadOnlyDto? authorReadOnlyDto = _mapper.Map<AuthorReadOnlyDto>(author);

                return authorReadOnlyDto;
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetAuthor)}");
                return BadRequest();
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorUpdateDto)
        {
            try
            {
                if (id != authorUpdateDto.Id)
                {
                    _logger.LogWarning($"Update ID invalid in {nameof(PutAuthor)} - Id {id}");
                    return BadRequest();
                }

                Author? author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PutAuthor)} - Id {id}");
                    return BadRequest();
                }

                _ = _mapper.Map(authorUpdateDto, author);

                _context.Entry(author).State = EntityState.Modified;
                _ = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exep)
            {
                if (!await AuthorExists(id))
                {
                    _logger.LogError(exep, $"{nameof(Author)} record not found in {nameof(PutAuthor)} for ID - {id}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorCreateDto)
        {
            try
            {
                Author? author = _mapper.Map<Author>(authorCreateDto);

                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PostAuthor)}");
                    return BadRequest();
                }
                _ = await _context.Authors.AddAsync(author);
                _ = await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing POST operation in {nameof(PostAuthor)}");
                return BadRequest();
            }
        }

        // DELETE: api/Authors/5

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                Author? author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(DeleteAuthor)} - Id {id}");
                    return NotFound();
                }
                _ = _context.Authors.Remove(author);
                _ = await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception exep)
            {
                _logger.LogError(exep, $"Error performing DELETE operation in {nameof(DeleteAuthor)} for ID: {id}");
                return NotFound();
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _context.Authors.AnyAsync(e => e.Id == id);
        }
    }
}
