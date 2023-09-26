using TimeTrackerService.Dto;
using TimeTrackerService.Models;
using Task = TimeTrackerService.Models.Task;

namespace TimeTrackerService.Services.Interfaces
{
    public interface ITasksService
    {
        Task<List<Task>> GetAllTasks();
        Task<Task> AddTask(Task task);
        Task<Task> StartTask(int id);
        System.Threading.Tasks.Task GenerateReport();
    }
}
