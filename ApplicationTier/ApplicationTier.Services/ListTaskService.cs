using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTier.Domain.Dtos;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Exceptions;
using ApplicationTier.Domain.Interfaces;
using ApplicationTier.Domain.Interfaces.Services;
using AutoMapper;

namespace ApplicationTier.Services
{
    public class ListTaskService: IListTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IList<ListTask>> GetAll()
        {
            var listTasks = await _unitOfWork.Repository<ListTask>().GetAllAsync(x=> x.IsDeleted == false);
            return listTasks.ToList();
        }

        public async Task<ListTask> GetOne(int listTaskId)
        {
            return await _unitOfWork.Repository<ListTask>().GetFirstOrDefaultAsync(x=> x.Id == listTaskId);
        }

        public async Task Update(ListTaskUpSertDto listTask, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var listTaskRepos = _unitOfWork.Repository<ListTask>();

                var listTaskInput = _mapper.Map<ListTask>(listTask);

                var listTaskDb = await listTaskRepos.FindAsync(listTaskInput.Id);
                if (listTaskDb == null)
                    throw new KeyNotFoundException();

                listTaskDb.Name = listTaskInput.Name;
                listTaskDb.ProjectId = listTaskInput.ProjectId;
                listTaskDb.UpdatedDate = DateTime.Now;
                listTaskDb.UpdatedById = user.Id;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(ListTaskUpSertDto listTask, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var listTaskRepos = _unitOfWork.Repository<ListTask>();

                var listTaskInput = _mapper.Map<ListTask>(listTask);

                listTaskInput.CreatedDate = DateTime.Now;
                listTaskInput.CreatedById = user.Id;

                await listTaskRepos.InsertAsync(listTaskInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int listTaskId, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var listTaskRepos = _unitOfWork.Repository<ListTask>();

                var listTaskDb = await listTaskRepos.FindAsync(listTaskId);
                if (listTaskDb == null)
                    throw new KeyNotFoundException();

                listTaskDb.IsDeleted = true;

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
