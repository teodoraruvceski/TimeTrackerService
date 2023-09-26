using TimeTrackerService.Models;
using Task = TimeTrackerService.Models.Task;

namespace TimeTrackerService.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        List<TEntity> Get();
        TEntity Get(int id);
    }
}
