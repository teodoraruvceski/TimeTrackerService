using TimeTrackerService.Db;
using TimeTrackerService.Models;
using TimeTrackerService.Repository.Interfaces;

namespace TimeTrackerService.Repository
{
    public class ProjectsRepository : IRepository<Project>
    {
        private readonly AppDbContext _database;
        public ProjectsRepository(AppDbContext database)
        {
            _database = database;
        }
        public Project Create(Project entity)
        {
            try
            {
                _database.Projects.Add(entity);
                _database.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Project> Get()
        {
            try
            {
               return _database.Projects.ToList();
            }
            catch
            { 
                return null; 
            }
        }

        public Project Get(int id)
        {
            throw new NotImplementedException();
        }

        public Project Update(Project entity)
        {
            throw new NotImplementedException();
        }
    }
}
