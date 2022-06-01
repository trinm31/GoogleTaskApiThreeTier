using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Interfaces;
using ApplicationTier.Domain.Interfaces.Services;
using ApplicationTier.Domain.Entities;
using AutoMapper;

namespace ApplicationTier.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IList<TaskItem>> GetAll()
    {
        var TaskItems = await _unitOfWork.Repository<TaskItem>().GetAllAsync(x=> x.IsDeleted == false);
        return TaskItems.ToList();
    }

    public async Task<TaskItem> GetOne(int taskId)
    {
        return await _unitOfWork.Repository<TaskItem>().GetFirstOrDefaultAsync(x=> x.Id == taskId);
    }

    public async Task Update(TaskUpSertDto taskItemDto, User user)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var taskItem = _mapper.Map<TaskItem>(taskItemDto);

            var taskRepos = _unitOfWork.Repository<TaskItem>();
            var task = await taskRepos.FindAsync(taskItem.Id);
            if (task == null)
                throw new KeyNotFoundException();

            task.Name = taskItem.Name;
            task.Content = taskItem.Content;
            task.ListTaskId = taskItem.ListTaskId;
            task.UpdatedDate = DateTime.Now;
            task.UpdatedById = user.Id;

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task Add(TaskUpSertDto taskItemDto, User user)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var taskItem = _mapper.Map<TaskItem>(taskItemDto);

            var taskRepos = _unitOfWork.Repository<TaskItem>();

            taskItem.CreatedDate = DateTime.Now;
            taskItem.CreatedById = user.Id;

            await taskRepos.InsertAsync(taskItem);

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }

    public async Task Delete(int taskId, User user)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var taskRepos = _unitOfWork.Repository<TaskItem>();
            var task = await taskRepos.FindAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException();

            task.IsDeleted = true;

            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }
    
    public async Task IsDone(int taskId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();

            var taskRepos = _unitOfWork.Repository<TaskItem>();
            var task = await taskRepos.FindAsync(taskId);
            if (task == null)
                throw new KeyNotFoundException();

            task.IsDone = true;
            
            await _unitOfWork.CommitTransaction();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransaction();
            throw;
        }
    }
}