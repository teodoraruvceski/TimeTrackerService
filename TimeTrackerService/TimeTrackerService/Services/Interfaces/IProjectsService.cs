using TimeTrackerService.Models;

namespace TimeTrackerService.Services.Interfaces
{
    public interface IProjectsService
    {
        Task<List<Project>> GetAllProjects();
        Task<Project> AddTask(Project project);
    }
}
