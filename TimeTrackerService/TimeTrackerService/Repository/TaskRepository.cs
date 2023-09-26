using Microsoft.Extensions.Options;
using TimeTrackerService.Repository.Interfaces;
using TimeTrackerService.Models;
using Task = TimeTrackerService.Models.Task;
using Newtonsoft.Json;
using Firebase.Database;
using TimeTrackerService.Db;

namespace TimeTrackerService.Repository
{
    public class TaskRepository : IRepository<Task>
    {
        private readonly AppDbContext _database;

        public TaskRepository()
        {
        }

        public TaskRepository(AppDbContext database)
        {
            _database = database;
        }

        public List<Task> Get()
        {
            return _database.Tasks.ToList();
        }

        public Task Create(Task task)
        {
            try
            {
                _database.Tasks.Add(task);
                _database.SaveChanges();
                return task;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Task Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Task task)
        {
            try
            {
                var entry = _database.Tasks.Update(task);
                _database.SaveChanges();
                return task;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
