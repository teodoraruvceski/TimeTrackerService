using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTrackerService.Models;
using TimeTrackerService.Repository.Interfaces;
using TimeTrackerService.Services.Implementations;
using Xunit;

namespace TaskTrackerTest
{
    public class ProjectsServiceTests
    {
        [Fact]
        public async void AddProject_ValidProject_ReturnsCreatedProject()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IRepository<Project>>();
            var projectsService = new ProjectsService(projectRepositoryMock.Object);

            var inputProject = new Project("Test Project");
            inputProject.Id = 0;

            var ceratedProject = inputProject;
            ceratedProject.Id = 1;

            projectRepositoryMock.Setup(repo => repo.Create(It.IsAny<Project>()))
                .Returns(ceratedProject);

            // Act
            var resultProject = await projectsService.AddTask(inputProject);

            // Assert
            Assert.NotNull(resultProject);
            Assert.NotEqual(0, resultProject.Id);
        }

        [Fact]
        public async void GetAllProjects_ReturnsProjectsList()
        {
            // Arrange
            var projectRepositoryMock = new Mock<IRepository<Project>>();
            var projectsService = new ProjectsService(projectRepositoryMock.Object);

            var expectedProjects = new List<Project>
            {
                new Project { Id = 1, Name = "Project 1" },
                new Project { Id = 2, Name = "Project 2" },
            };

            projectRepositoryMock.Setup(repo => repo.Get()).Returns(expectedProjects);

            // Act
            var actualProjects = await projectsService.GetAllProjects();

            // Assert
            Assert.NotNull(actualProjects);
            Assert.Equal(expectedProjects.Count, actualProjects.Count);
        }
    }
}
