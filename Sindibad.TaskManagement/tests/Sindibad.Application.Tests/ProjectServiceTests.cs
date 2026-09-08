using Sindibad.Application.IRepositories;
using Sindibad.Application.Services;
using Sindibad.Domain.Entities;
using Moq;


namespace Sindibad.Application.Tests;
public class ProjectServiceTests
{
    [Fact]
    public async System.Threading.Tasks.Task CreateAsync_ShouldCreateProjectSuccessfully()
    {
        // Arrange
        var repositoryMock = new Mock<IGenericRepository<Project>>();

        repositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Project>()))
            .ReturnsAsync((Project project) => project);

        var service = new ProjectService(repositoryMock.Object);

        var projectName = "Payment Platform";

        // Act
        var result = await service.CreateAsync(projectName);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);

        Assert.Equal(projectName, result.Value.Name);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.NotEqual(default, result.Value.CreatedAt);

        repositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Project>()),
            Times.Once);
    }
}