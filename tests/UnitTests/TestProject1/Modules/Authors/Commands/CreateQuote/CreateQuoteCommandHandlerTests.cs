using System.Linq.Expressions;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Extensions;
using AW3.GR.OpenAI.Domain.Authors;
using FluentAssertions;
using MapsterMapper;
using Moq;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Commands.CreateQuote;

public class CreateQuoteCommandHandlerTests
{
    private readonly CreateQuoteCommandHandler _handler;
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;

    public CreateQuoteCommandHandlerTests()
    {
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new CreateQuoteCommandHandler(_mockAuthorRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task CreateQuote_ReturnsOK()
    {
        var command = CreateQuoteCommandTestUtils.CreateQuote();

        _mockAuthorRepository.Setup(ar => ar.FirstOrDefaultAsync(It.IsAny<Expression<Func<Author, bool>>>(), default))
            .ReturnsAsync(CreateQuoteCommandTestUtils.GetAuthor());

        var expectedAuthorDto = CreateQuoteCommandTestUtils.CreateAuthorDto;
        _mockMapper.Setup(m => m.Map<It.IsAnyType>(It.IsAny<Author>())).Returns(expectedAuthorDto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsError.Should().BeFalse();
        result.Value.ValidateCreatedFrom(command);

        _mockAuthorRepository.Verify(ar => ar.FirstOrDefaultAsync(It.IsAny<Expression<Func<Author, bool>>>(), default), Times.Once);
    }

    [Fact]
    public async Task CreateQuote_AuthorNotFound()
    {
        var command = CreateQuoteCommandTestUtils.CreateQuoteInvalidAuthorId();

        _mockAuthorRepository.Setup(ar => ar.FirstOrDefaultAsync(It.IsAny<Expression<Func<Author, bool>>>(), default))
            .ReturnsAsync(null as Author);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsError.Should().BeTrue();

        _mockAuthorRepository.Verify(ar => ar.FirstOrDefaultAsync(It.IsAny<Expression<Func<Author, bool>>>(), default), Times.Once);
    }
}
