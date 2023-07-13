using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Extesnsions;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;
using FluentAssertions;
using Moq;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Commands.OpenAI;

public class OpenAIQueryHandlerTests
{
    private readonly OpenAIQueryHandler _handler;

    private readonly Mock<ISearchHistoryRepository> _mockSearchHistoryRepository;
    private readonly Mock<IOpenAIClient> _mockOpenAIClient;
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    private readonly Mock<IUserContextService> _mockUserContextService;

    public OpenAIQueryHandlerTests()
    {
        _mockSearchHistoryRepository = new Mock<ISearchHistoryRepository>();
        _mockOpenAIClient = new Mock<IOpenAIClient>();
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _mockUserContextService = new Mock<IUserContextService>();

        _handler = new OpenAIQueryHandler(
            _mockSearchHistoryRepository.Object,
            _mockOpenAIClient.Object,
            _mockDateTimeProvider.Object,
            _mockUserContextService.Object);
    }

    [Fact]
    public async Task OpenAiQuery_ReturnsOk()
    {
        var query = OpenAIQueryHandlerUtils.CreateQuery();

        _mockUserContextService.Setup(_mockUserContextService => _mockUserContextService.UserId)
            .Returns(UserId.Create(UserConstants.Id));

        _mockOpenAIClient.Setup(x => x.GetMostPopularQuoteByAuthorNameAsync(It.IsAny<string>()))
            .ReturnsAsync(OpenAIQueryHandlerUtils.GetOpenAIResponse());

        var result = await _handler.Handle(query, CancellationToken.None);

        result.IsError.Should().BeFalse();
        result.Value.ValidateOpenAIResponse();
    }
}