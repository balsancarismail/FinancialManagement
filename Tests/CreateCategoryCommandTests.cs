using System.Threading;
using System.Threading.Tasks;
using Application.Services.Repositories;

namespace Application.Features.Category.Commands.Create.Tests
{
    public class CreateCategoryCommandTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;

        public CreateCategoryCommandTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCreateCategoryResponse()
        {
            // Arrange
            var command = new CreateCategoryCommand
            {
                Name = "Test Category",
                CategoryType = 1
            };

            var category = new Domain.Entities.Category
            {
                Name = "Test Category",
                CategoryType = Domain.Enums.CategoryType.Income
            };

            var response = new CreateCategoryResponse
            {
                Id = 1,
                Name = "Test Category",
                CategoryType = Domain.Enums.CategoryType.Expense
            };

            _mockMapper.Setup(m => m.Map<Domain.Entities.Category>(command)).Returns(category);
            _mockMapper.Setup(m => m.Map<CreateCategoryResponse>(category)).Returns(response);
            _mockCategoryRepository.Setup(r => r.AddAsync(category)).ReturnsAsync(category);

            var handler = new CreateCategoryCommand.CreateCategoryCommandHandler(_mockCategoryRepository.Object, _mockMapper.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(response.Id, result.Id);
            Assert.Equal(response.Name, result.Name);
            Assert.Equal(response.CategoryType, result.CategoryType);
        }
    }
}
