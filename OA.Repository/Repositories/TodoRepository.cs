using OA.Data.Entities;
using OA.Repository.Context;

namespace OA.Repository.Repositories;
internal class TodoRepository : BaseRepository<TodoEntity>, ITodoRepository
{
    public TodoRepository(DataContext dataContext) : base(dataContext)
    {
    }
}

public interface ITodoRepository : IBaseRepository<TodoEntity>
{

}