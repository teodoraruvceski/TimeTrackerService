using Moq;
using TimeTrackerService.Services.Implementations;
using TimeTrackerService.Models;
using TimeTrackerService.Repository.Interfaces;
using Task = TimeTrackerService.Models.Task;
using Xunit;
using System;

namespace TaskTrackerTest
{
    public class TasksServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task AddTask_HasName_ReturnsUpdatedTask()
        {
            // Arrange
            var taskRepositoryMock = new Mock<IRepository<Task>>();
            var tasksService = new TasksService(taskRepositoryMock.Object);
            var inputTask = new Task { 
                Name = "Test Task", 
                ProjectName = "project", 
                Id = 0,
                StartTime = null,
                EndTime = null,
                Duration = null,
            };

            taskRepositoryMock.Setup(repo => repo.Update(inputTask))
                .Returns(new Task(1, "Test Task", "project"));

            // Act
            var resultTask = await tasksService.AddTask(inputTask);

            // Assert
            Assert.NotEqual(0, resultTask.Id);
        }

        [Fact]
        public async void AddTask_UndefinedName_ReturnsUpdatedTaskWithGeneratedName()
        {
            // Arrange
            var taskRepositoryMock = new Mock<IRepository<Task>>();
            var tasksService = new TasksService(taskRepositoryMock.Object);
            var inputTask = new Task {
                Name = "undefined", 
                StartTime = It.IsAny<DateTime>(), 
                EndTime = It.IsAny<DateTime>(), 
                Duration = It.IsAny<float>(),
                Id = 0,
            };
            var createdTask = inputTask;
            createdTask.Name = "Task123";
            createdTask.Id = 123;
            taskRepositoryMock.Setup(repo => repo.Create(It.IsAny<Task>()))
                .Returns(createdTask);

            taskRepositoryMock.Setup(repo => repo.Update(It.IsAny<Task>()))
                .Returns((Task task) => task);

            // Act
            var resultTask = await tasksService.AddTask(inputTask);

            // Assert
            Assert.True(resultTask.Name.Contains("Task"));
            Assert.NotEqual(resultTask.Name, "undefined");
            Assert.NotEqual(0, resultTask.Id);
        }

        [Fact]
        public async void GetAllTasks_ReturnsTasksList()
        {
            // Arrange
            var taskRepositoryMock = new Mock<IRepository<Task>>();
            var tasksService = new TasksService(taskRepositoryMock.Object);
            var expectedTasks = new System.Collections.Generic.List<Task>
            {
                new Task { Id = 1, Name = "Task 1" },
                new Task { Id = 2, Name = "Task 2" },
            };
            taskRepositoryMock.Setup(repo => repo.Get()).Returns(expectedTasks);

            // Act
            var actualTasks = await tasksService.GetAllTasks();

            // Assert
            Assert.NotNull(actualTasks);
            Assert.Equal(expectedTasks.Count, actualTasks.Count);
        }
    }
}
