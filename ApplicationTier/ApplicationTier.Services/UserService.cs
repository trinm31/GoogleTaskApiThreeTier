using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTier.Domain.Entities;
using ApplicationTier.Domain.Exceptions;
using ApplicationTier.Domain.Interfaces;
using ApplicationTier.Domain.Interfaces.Authorization;
using ApplicationTier.Domain.Interfaces.Services;
using ApplicationTier.Domain.Models.Users;
using AutoMapper;

namespace ApplicationTier.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            IJwtUtils jwtUtils,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _unitOfWork.Repository<User>().GetFirstOrDefaultAsync(x => x.Username == model.Username);

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Repository<User>().GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.Repository<User>().FindAsync(id);
        }

        public async Task Register(RegisterRequest model)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                // validate
                var userList = await _unitOfWork.Repository<User>().GetAllAsync(x => x.Username == model.Username);
                if (userList.Any())
                    throw new AppException("Username '" + model.Username + "' is already taken");

                // map model to new user object
                var user = _mapper.Map<User>(model);

                // hash password
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // save user
                await _unitOfWork.Repository<User>().InsertAsync(user);

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Update(int id, UpdateRequest model)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var user = await _unitOfWork.Repository<User>().FindAsync(id);

                // validate
                var userList = await _unitOfWork.Repository<User>().GetAllAsync(x => x.Username == model.Username);
                if (userList.Any())
                    throw new AppException("Username '" + model.Username + "' is already taken");

                // hash password if it was entered
                if (!string.IsNullOrEmpty(model.Password))
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // copy model to user and save
                _mapper.Map(model, user);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                var user = await _unitOfWork.Repository<User>().FindAsync(id);
                if (user == null) throw new KeyNotFoundException("User not found");
                await _unitOfWork.Repository<User>().DeleteAsync(user);

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
