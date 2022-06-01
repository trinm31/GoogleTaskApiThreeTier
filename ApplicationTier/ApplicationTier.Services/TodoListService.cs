using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Interfaces;
using ApplicationTier.Domain.Interfaces.Services;
using ApplicationTier.Domain.Entities;
using AutoMapper;

namespace ApplicationTier.Services;

public class TodoListService : ITodoListService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TodoListService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<IList<TodoList>> GetAll()
    {
        var todoList = await _unitOfWork.Repository<TodoList>().GetAllAsync(x=> x.IsDeleted == false);
        return todoList.ToList();
    }

    public async Task<TodoList> GetOne(int todoId)
    {
        return await _unitOfWork.Repository<TodoList>().GetFirstOrDefaultAsync(x=> x.Id == todoId);
    }

    public async Task Update(TodoUpSertDto todoDto, User user)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var todoItem = _mapper.Map<TodoList>(todoDto);

            var todoRepos = _unitOfWork.Repository<TodoList>();
            var todoDb = await todoRepos.FindAsync(todoItem.Id);
            if (todoDb == null)
                throw new KeyNotFoundException();

            todoDb.Content = todoItem.Content;
            todoDb.TaskId = todoItem.TaskId;
            todoDb.UpdatedDate = DateTime.Now;
            todoDb.UpdatedById = user.Id;

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task Add(TodoUpSertDto todoDto, User user)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var todoItem = _mapper.Map<TodoList>(todoDto);

            var todoRepos = _unitOfWork.Repository<TodoList>();

            todoItem.CreatedDate = DateTime.Now;
            todoItem.CreatedById = user.Id;

            await todoRepos.InsertAsync(todoItem);

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task Delete(int todoId, User user)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var todoRepos = _unitOfWork.Repository<TodoList>();
            var todoItem = await todoRepos.FindAsync(todoId);
            if (todoItem == null)
                throw new KeyNotFoundException();

            todoItem.IsDeleted = true;

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task IsDone(int todoId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var todoRepos = _unitOfWork.Repository<TodoList>();
            var todoItem = await todoRepos.FindAsync(todoId);
            if (todoItem == null)
                throw new KeyNotFoundException();

            todoItem.IsDone = true;

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }
}