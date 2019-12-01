using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {

            _userRepo = userRepository;
            _mapper = mapper;
        }

        public UserResource CreateUser(CreateUserResource createUserResource)
        {
            if (createUserResource == null)
                throw new ArgumentNullException("createUserResource cannot be null");

            var user = _mapper.Map<User>(createUserResource);

            var hashResult = AppSecurity.HashPassword(createUserResource.Password);
            user.Password = hashResult.HashedPassword;

            _userRepo.Add(user);
            _userRepo.SaveChanges();

            return _mapper.Map<UserResource>(user);

        }

        public UserResource FindByUsername(string username)
        {
            return _mapper.Map<UserResource>(_userRepo.FindByUserName(username));
        }

    }
}
