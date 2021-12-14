using System;
using System.Linq;
using System.Text;
using AutoMapper;
using EgressProject.API.Business.Interfaces;
using EgressProject.API.Models;
using EgressProject.API.Models.Enums;
using EgressProject.API.Models.InputModel;
using EgressProject.API.Models.Utils;
using EgressProject.API.Models.ViewModel;
using EgressProject.API.Repositories.Interfaces;
using System.Security.Cryptography;

namespace EgressProject.API.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserBusiness(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public PagedList<UserViewModel> GetPaginate(PaginationParameters paginationParameters)
        {
            var usersPagedList = _userRepository.GetPaginate(paginationParameters);

            PagedList<UserViewModel> usersViewModelPagedList = new PagedList<UserViewModel>(
                usersPagedList.Select(_mapper.Map<User, UserViewModel>),
                usersPagedList.CurrentPage,
                usersPagedList.TotalPages,
                usersPagedList.PageSize,
                usersPagedList.TotalCount
            );

            return usersViewModelPagedList;
        }

        public UserViewModel GetById(int id)
            => _mapper.Map<User, UserViewModel>(_userRepository.GetById(id));

        public UserViewModel Update(UserInputModel userInputModel)
        {
            User user = _userRepository.GetById(userInputModel.Id);
            user.Id = userInputModel.Id;
            user.Email = userInputModel.Email ?? user.Email;
            user.IsValidated = userInputModel.IsValidated ?? user.IsValidated;
            user.Password = (string.IsNullOrEmpty(userInputModel.Password)) ? user.Password
                : BitConverter.ToString(new SHA256CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(userInputModel.Password)));
            user.Role = string.IsNullOrEmpty(userInputModel.Role)? user.Role : Enum.Parse<Role>(userInputModel.Role);
            user.PersonId = userInputModel.PersonId ?? user.PersonId;

            return _mapper.Map<User, UserViewModel>(_userRepository.Update(user));
        }
            
        public bool Delete(int id)
            => _userRepository.Delete(id);
    }
}