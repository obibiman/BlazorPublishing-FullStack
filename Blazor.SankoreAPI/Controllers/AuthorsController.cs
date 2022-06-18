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
using Blazor.SankoreAPI.Static;
using Blazor.SankoreAPI.Models.DataTransfer.Author;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
                var authors = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(await _context.Authors.ToListAsync());
                return Ok(authors);
            }
            catch(Exception exep)
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
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(GetAuthor)} - Id {id}");
                    return NotFound();
                }             

                var authorReadOnlyDto = _mapper.Map<AuthorReadOnlyDto>(author);            

                return authorReadOnlyDto;
            }
            catch(Exception exep)
            {
                _logger.LogError(exep, $"Error performing GET operation in {nameof(GetAuthor)}");
                return BadRequest();
            }
        }

// PUT: api/Authors/5
// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorUpdateDto)
        {      
            try
            {
                if (id != authorUpdateDto.Id)
                {
                    _logger.LogWarning($"Update ID invalid in {nameof(PutAuthor)} - Id {id}");
                    return BadRequest();
                }

                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PutAuthor)} - Id {id}");
                    return BadRequest();
                }

                _mapper.Map(authorUpdateDto, author);

                _context.Entry(author).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exep)
            {
                if (! await AuthorExists(id))
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
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorCreateDto)
        {
            try
            {
                var author = _mapper.Map<Author>(authorCreateDto);

                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PostAuthor)}");
                    return BadRequest();
                }
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch(Exception exep)
            {
                _logger.LogError(exep, $"Error performing POST operation in {nameof(PostAuthor)}");
                return BadRequest();
            }
        }

        // DELETE: api/Authors/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(DeleteAuthor)} - Id {id}");
                    return NotFound();
                }
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

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
