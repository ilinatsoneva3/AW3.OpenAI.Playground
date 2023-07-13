using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;
using AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Extensions;
using AW3.GR.OpenAI.Domain.Authors;
using FluentAssertions;
using MapsterMapper;
using Moq;

namespace AW3.GR.OpenAI.Application.UnitTests.Modules.Authors.Queries.GetAuthors;

public class GetAuthorsQueryHandlerTests
{
    private readonly GetAuthorsQueryHandler _handler;
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;

    public GetAuthorsQueryHandlerTests()
    {
        _mockAuthorRepository = new Mock<IAuthorRepository>();
        _mockMapper = new Mock<IMapper>();

        _handler = new GetAuthorsQueryHandler(_mockAuthorRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAuthors_ReturnsOK()
    {
        var command = new GetAuthorsQuery();

        var authors = GetAuthorQueryTestUtils.GetAuthorsList(1);

        _mockAuthorRepository.Setup(ar => ar.GetAllAsync(default))
                             .ReturnsAsync(authors);

        _mockMapper.Setup(m => m.Map<It.IsAnyType>(It.IsAny<Author>())).Returns(GetAuthorQueryTestUtils.GetAuthorDTO);

        var result = await _handler.Handle(command, default);

        result.IsError.Should().BeFalse();
        result.Value.ValidateGetAuthorList(authors);

        _mockAuthorRepository.Verify(ar => ar.GetAllAsync(default), Times.Once);
    }
}
