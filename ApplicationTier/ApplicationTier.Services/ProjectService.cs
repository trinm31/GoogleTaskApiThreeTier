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
    public class ProjectService: IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IList<Project>> GetAll()
        {
            var projectList = await _unitOfWork.Repository<Project>().GetAllAsync(x => x.IsDeleted == false);
            return projectList.ToList();
        }

        public async Task<Project> GetOne(int projectId)
        {
            return await _unitOfWork.Repository<Project>().GetFirstOrDefaultAsync(x=> x.Id == projectId && x.IsDeleted == false);
        }

        public async Task Update(ProjectUpSertDto project, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var projectRepos = _unitOfWork.Repository<Project>();

                // validate name
                var projNameCheck = await projectRepos.GetAllAsync(x => x.Name == project.Name);
                if (projNameCheck.Any())
                    throw new AppException("Project '" + project.Name + "' is already taken");

                var projectInput = _mapper.Map<Project>(project);

                var projectDb = await projectRepos.FindAsync(projectInput.Id);
                if (projectDb == null)
                    throw new KeyNotFoundException();

                projectDb.Name = projectInput.Name;
                projectDb.Description = projectInput.Description;
                projectDb.UpdatedDate = DateTime.Now;
                projectDb.UpdatedById = user.Id;
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Add(ProjectUpSertDto project, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var projectRepos =  _unitOfWork.Repository<Project>();
                // validate
                var projCheck = await projectRepos.GetAllAsync(x => x.Name == project.Name);
                if (projCheck.Any())
                    throw new AppException("Project '" + project.Name + "' is already taken");

                var projectInput = _mapper.Map<Project>(project);

                projectInput.CreatedDate = DateTime.Now;
                projectInput.CreatedById = user.Id;

                await projectRepos.InsertAsync(projectInput);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int projectId, User user)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var projectRepos = _unitOfWork.Repository<Project>();

                var projectDb = await projectRepos.FindAsync(projectId);
                if (projectDb == null)
                    throw new KeyNotFoundException();

                projectDb.IsDeleted = true;
                
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
