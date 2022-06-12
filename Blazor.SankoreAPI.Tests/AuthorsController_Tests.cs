using AutoMapper;
using Blazor.SankoreAPI.Configurations;
using Blazor.SankoreAPI.Controllers;
using Blazor.SankoreAPI.Database;
using Blazor.SankoreAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blazor.SankoreAPI.Tests
{
    public class AuthorsController_Tests
    {
        private static IMapper? _mapper;

        public AuthorsController_Tests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
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
            var _mockLogger = new Mock<ILogger<AuthorsController>>();
            //Arrange
            // todo: define the required assets
            var _options = new DbContextOptionsBuilder<BookRepoContext>().UseInMemoryDatabase(databaseName: "BookRepo").Options;
            using var context = new BookRepoContext(_options);
            context.Add(new Author()
            {
                Id = 1,
                FirstName = "Brenda",
                LastName="Williams"                 
            });
            context.Add(new Author()
            {
                Id = 2,
                FirstName = "Johnny",
                LastName = "Joe"
            });
            context.SaveChanges();
            var authors = await context.Authors.ToListAsync();
            //var authorsController = new AuthorsController(context, new NullLogger<AuthorsController>(), new IMapper<AuthorsController>());
            var authorsController = new AuthorsController(context, _mockLogger.Object, _mapper);
            // Act
            // todo: invoke the test
            var authorsList = await authorsController.GetAuthors();

            // Assert
            // todo: verify that conditions are met
            Assert.NotNull(authorsList);         
            
        }
    }
}
