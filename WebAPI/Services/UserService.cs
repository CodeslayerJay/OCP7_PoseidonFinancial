using AutoMapper;
using Dot.Net.WebApi.Domain;
using System;
using System.Linq;
using System.Security.Claims;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Data;
using WebApi.ModelValidators;
using WebApi.Repositories;


namespace WebApi.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepo;
        private readonly IAccessTokenRepository _tokenRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAccessTokenRepository tokenRepository, IMapper mapper)
        {

            _userRepo = userRepository;
            _tokenRepo = tokenRepository;
            _mapper = mapper;
        }

        public ValidationResult ValidateResource(EditUserResource resource, bool isUpdate)
        {
            var result = new ValidationResult();

            if (resource != null)
            {
                var validator = new UserValidator(isUpdate);
                var vr = validator.Validate(resource);

                if (vr.IsValid)
                {
                    result.IsValid = true;
                    return result;
                }


                if (vr.Errors.Any())
                {
                    foreach (var error in vr.Errors)
                    {
                        result.ErrorMessages.Add(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            return result;
        }

        public bool ValidateUser(string username, string password)
        {
           if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                return false;

            var user = _userRepo.FindByUserName(username);

            if (user == null)
                return false;

            return AppSecurity.VerifyPasswords(password, user.Password);
        }

        public void StoreAccessToken(JsonWebToken token, int userId)
        {
            if (token != null || userId > 0)
            {
                var aToken = _tokenRepo.GetAccessToken(userId);

                if (aToken != null)
                    _tokenRepo.DeleteToken(aToken);

                _tokenRepo.StoreAccessToken(token, userId);
            }
        }


        public UserResource GetUserForAccessToken(string token)
        {
            var aToken = _tokenRepo.GetAccessToken(token);

            var user = _userRepo.FindById(aToken.UserId);

            return _mapper.Map<UserResource>(user);
        }


        public UserResource GetUserForAccessToken(JsonWebToken token)
        {
            return GetUserForAccessToken(token.Token);
        }

        public UserResource CreateUser(EditUserResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("createUserResource cannot be null");

            var user = _mapper.Map<User>(resource);

            var hashResult = AppSecurity.HashPassword(resource.Password);
            user.Password = hashResult.HashedPassword;

            _userRepo.Add(user);
            _userRepo.SaveChanges();

            return _mapper.Map<UserResource>(user);

        }

        public void DeleteUser(int id)
        {
            var user = _userRepo.FindById(id);

            if(user != null)
            {
                _userRepo.Delete(id);
                _userRepo.SaveChanges();
            }
        }

        public UserResource FindByUsername(string username)
        {
            return _mapper.Map<UserResource>(_userRepo.FindByUserName(username));
        }

        public UserResource GetUserById(int id)
        {
            return _mapper.Map<UserResource>(_userRepo.FindById(id));
        }

        public void UpdateUser(int id, EditUserResource resource)
        {
            var userToUpdate = _userRepo.FindById(id);

            if(userToUpdate != null && resource != null)
            {
                // Hash any passwords
                resource.Password = AppSecurity.HashPassword(resource.Password).HashedPassword;

                _userRepo.Update(id, _mapper.Map(resource, userToUpdate));
                _userRepo.SaveChanges();
            }
        }
    }
}
