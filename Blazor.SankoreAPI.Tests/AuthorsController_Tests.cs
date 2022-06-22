using AutoMapper;
using Blazor.SankoreAPI.Configurations;
using Blazor.SankoreAPI.Controllers;
using Blazor.SankoreAPI.Database;
using Blazor.SankoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blazor.SankoreAPI.Tests
{
    public class AuthorsController_Tests
    {
        private static IMapper? _mapper;

        public AuthorsController_Tests()
        {
            if (_mapper == null)
            {
                MapperConfiguration? mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperConfig());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task GetAuthors_Test()
        {
            Mock<ILogger<AuthorsController>>? _mockLogger = new Mock<ILogger<AuthorsController>>();
            //Arrange
            // todo: define the required assets
            DbContextOptions<BookRepoContext>? _options = new DbContextOptionsBuilder<BookRepoContext>().UseInMemoryDatabase(databaseName: "BookRepo").Options;
            using BookRepoContext? context = new BookRepoContext(_options);
            _ = context.Add(new Author()
            {
                Id = 1,
                FirstName = "Brenda",
                LastName = "Williams"
            });
            _ = context.Add(new Author()
            {
                Id = 2,
                FirstName = "Johnny",
                LastName = "Joe"
            });
            _ = context.SaveChanges();
            List<Author>? authors = await context.Authors.ToListAsync();
            //var authorsController = new AuthorsController(context, new NullLogger<AuthorsController>(), new IMapper<AuthorsController>());
            AuthorsController? authorsController = new AuthorsController(context, _mockLogger.Object, _mapper);
            // Act
            // todo: invoke the test
            Microsoft.AspNetCore.Mvc.ActionResult<IEnumerable<Models.DataTransfer.Author.AuthorReadOnlyDto>>? authorsList = await authorsController.GetAuthors();

            // Assert
            // todo: verify that conditions are met
            Assert.NotNull(authorsList);

        }
    }
}
