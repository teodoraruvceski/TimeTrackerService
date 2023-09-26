using TimeTrackerService.Models;
using Task = TimeTrackerService.Models.Task;

namespace TimeTrackerService.Services.Interfaces
{
    public interface ITasksService
    {
        Task<List<Task>> GetAllTasks();
        Task<Task> AddTask(Task task);
        System.Threading.Tasks.Task GenerateReport();
    }
}
