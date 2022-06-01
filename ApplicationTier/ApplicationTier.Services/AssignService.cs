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
    public class AssignService: IAssignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(AssignUpSertDto assignDto, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var assignRepos = _unitOfWork.Repository<Assignment>();

                var assignInput = _mapper.Map<Assignment>(assignDto);

                assignInput.CreatedDate = DateTime.Now;
                assignInput.CreatedById = user.Id;

                await assignRepos.InsertAsync(assignInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int assignmentId, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var assignRepos = _unitOfWork.Repository<Assignment>();

                var assignDb = await assignRepos.FindAsync(assignmentId);
                if (assignDb == null)
                    throw new KeyNotFoundException();

                assignDb.IsDeleted = true;

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
