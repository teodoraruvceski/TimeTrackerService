using TimeTrackerService.Models;
using TimeTrackerService.Repository.Interfaces;
using TimeTrackerService.Services.Interfaces;

namespace TimeTrackerService.Services.Implementations
{
    public class ProjectsService : IProjectsService
    {
        IRepository<Project> _repository;
        public ProjectsService(IRepository<Project> repository)
        {
            _repository = repository;
        }
        public async Task<Project> AddTask(Project project)
        {
            return _repository.Create(project);
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return _repository.Get();
        }
    }
}
