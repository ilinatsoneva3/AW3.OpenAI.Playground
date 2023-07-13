using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;
using FluentAssertions;
using MapsterMapper;
using Moq;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public class GetSearchHistoryQueryHandlerTests
{
    private readonly GetSearchHistoryQueryHandler _handler;

    private readonly Mock<ISearchHistoryRepository> _mockSearchHistoryRepository;
    private readonly Mock<IUserContextService> _mockUserContextService;
    private readonly Mock<IMapper> _mockMapper;

    public GetSearchHistoryQueryHandlerTests()
    {
        _mockSearchHistoryRepository = new Mock<ISearchHistoryRepository>();
        _mockUserContextService = new Mock<IUserContextService>();
        _mockMapper = new Mock<IMapper>();

        _handler = new GetSearchHistoryQueryHandler(
            _mockSearchHistoryRepository.Object,
            _mockUserContextService.Object,
            _mockMapper.Object);
    }

    [Fact]
    public async Task GetSearchHistoryQuery_ReturnsOk()
    {
        var request = GetSearchHistoryQueryHandlerTestUtils.GetSearchHistoryQuery();

        var searchHistories = GetSearchHistoryQueryHandlerTestUtils.GetSearchHistory();

        _mockUserContextService.Setup(x => x.UserId)
            .Returns(UserId.Create(UserConstants.Id));

        _mockSearchHistoryRepository
            .Setup(x => x.GetAllByUserIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(searchHistories);

        _mockMapper
            .Setup(m => m.Map<It.IsAnyType>(It.IsAny<Domain.SearchHistories.SearchHistory>()))
            .Returns(GetSearchHistoryQueryHandlerTestUtils.GetSearchHistoryResponse);

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsError.Should().BeFalse();
        result.Value.Should().NotBeEmpty();

        _mockUserContextService.Verify(x => x.UserId, Times.Once);
        _mockSearchHistoryRepository.Verify(x => x.GetAllByUserIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockMapper.Verify(m => m.Map<It.IsAnyType>(It.IsAny<Domain.SearchHistories.SearchHistory>()), Times.Exactly(searchHistories.Count));
    }

    [Fact]
    public async Task GetSearchHistoryQuery_ReturnsUserNotFound()
    {
        var request = GetSearchHistoryQueryHandlerTestUtils.GetSearchHistoryQuery();

        var searchHistories = GetSearchHistoryQueryHandlerTestUtils.GetSearchHistory();

        _mockUserContextService.Setup(x => x.UserId)
            .Returns(null as UserId);

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsError.Should().BeTrue();

        _mockUserContextService.Verify(x => x.UserId, Times.Once);
        _mockSearchHistoryRepository.Verify(x => x.GetAllByUserIdAsync(It.IsAny<UserId>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockMapper.Verify(m => m.Map<It.IsAnyType>(It.IsAny<Domain.SearchHistories.SearchHistory>()), Times.Never);
    }
}
