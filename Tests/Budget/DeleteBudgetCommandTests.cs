using System;
using System.Linq.Expressions;
using Application.Features.Budget.Commands.Delete;
using Application.Features.Budget.Rules;
using Application.Services.Repositories;
using static Application.Features.Budget.Commands.Delete.DeleteBudgetCommand;

namespace Tests.Budget
{
    public class DeleteBudgetCommandHandlerTests
    {
        private readonly Mock<IBudgetRepository> _mockBudgetRepository;
        private readonly Mock<IBudgetBusinessRules> _mockBudgetBusinessRules;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DeleteBudgetCommandHandler _handler;

        public DeleteBudgetCommandHandlerTests()
        {
            _mockBudgetRepository = new Mock<IBudgetRepository>();
            _mockBudgetBusinessRules = new Mock<IBudgetBusinessRules>();
            _mockMapper = new Mock<IMapper>();
            _handler = new DeleteBudgetCommandHandler(_mockBudgetRepository.Object, _mockBudgetBusinessRules.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCallRepositoryAndReturnResponse()
        {
            // Arrange
            var request = new DeleteBudgetCommand { Id = 1 };
            var budgetEntity = new Domain.Entities.Budget { Id = 1 };
            var response = new DeleteBudgetResponse();

            _mockBudgetBusinessRules.Setup(x => x.IsBudgetExists(request.Id, It.IsAny<CancellationToken>()));
            _mockBudgetRepository.Setup(repo => repo.GetAsync(
                It.IsAny<Expression<Func<Domain.Entities.Budget, bool>>>(), // predicate icin It.IsAny kullaniliyor
                null, // include icin varsayilan deger null
                It.IsAny<bool>(), // withDeleted icin varsayilan deger It.IsAny kullaniliyor, spesifik bir deger gerekiyorsa degistirilebilir
                It.IsAny<bool>(), // enableTracking icin varsayilan deger It.IsAny kullaniliyor, spesifik bir deger gerekiyorsa degistirilebilir
                It.IsAny<CancellationToken>() // cancellationToken icin It.IsAny kullaniliyor
                )).ReturnsAsync(budgetEntity);
            _mockMapper.Setup(x => x.Map<DeleteBudgetResponse>(budgetEntity)).Returns(response);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockBudgetBusinessRules.Verify(x => x.IsBudgetExists(request.Id, It.IsAny<CancellationToken>()), Times.Once);
            _mockBudgetRepository.Verify(x => x.GetAsync(It.IsAny<Expression<Func<Domain.Entities.Budget, bool>>>(), // predicate icin It.IsAny kullaniliyor
                null, // include icin varsayilan deger null
                It.IsAny<bool>(), // withDeleted icin varsayilan deger It.IsAny kullaniliyor, spesifik bir deger gerekiyorsa degistirilebilir
                It.IsAny<bool>(), // enableTracking icin varsayilan deger It.IsAny kullaniliyor, spesifik bir deger gerekiyorsa degistirilebilir
                It.IsAny<CancellationToken>()), Times.Once);
            _mockBudgetRepository.Verify(x => x.DeleteAsync(It.IsAny<Domain.Entities.Budget>(), It.IsAny<bool>()), Times.Once);
            _mockMapper.Verify(x => x.Map<DeleteBudgetResponse>(It.IsAny<Domain.Entities.Budget>()), Times.Once);
            Assert.Equal(response, result);
        }
    }
}
