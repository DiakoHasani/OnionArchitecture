using AutoMapper;
using OA.Data.Entities;
using OA.Repository.Repositories;
using OA.Service.DTO.Todo;
using OA.Service.General;

namespace OA.Service.Services;
internal class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    public TodoService(ITodoRepository todoRepository,
    IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<MessageModel> AddTodo(PostTodoModel model, string userId)
    {
        var todo = _mapper.Map<TodoEntity>(model);
        todo.UserId = userId;
        _todoRepository.Add(todo);
        if (await _todoRepository.SaveChangeAsync() > 0)
        {
            return new MessageModel
            {
                Message = "added todo to database",
                Result = true
            };
        }
        else
        {
            return new MessageModel
            {
                Message = "error in add todo to database"
            };
        }
    }

    public List<TodoModel> GetTodos(string userId, int page = 0)
    {
        var query = _todoRepository.GetAll(a => a.IsDelete == false & a.UserId == userId).OrderByDescending(a => a.Id).Skip(page * 20).Take(20);
        return _mapper.Map<List<TodoModel>>(query.ToList());
    }

    public async Task<MessageModel> RemoveTodo(int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        if (todo is null)
        {
            return new MessageModel { Message = "notfound todo in database", Code = 404 };
        }
        todo.IsDelete = true;
        _todoRepository.Edit(todo);
        if (await _todoRepository.SaveChangeAsync() > 0)
        {
            return new MessageModel { Result = true, Code = 200, Message = "succeful removed" };
        }
        else
        {
            return new MessageModel { Message = "error in server", Code = 500 };
        }
    }
}

public interface ITodoService
{
    Task<MessageModel> AddTodo(PostTodoModel model, string userId);
    Task<MessageModel> RemoveTodo(int id);
    List<TodoModel> GetTodos(string userId, int page = 0);
}