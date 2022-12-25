using AutoMapper;
using Compass.Data.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Interface;
using Unit_Data.Models;
using Unit_Data.Models.Models;

namespace Unit_Services.Services
{
    public class UserService
    {
        IUserRepository _userRepository;
        JwtService _jwtService;
        IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, JwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse> RegisterUserAsync(RegisterUserVM model)
        {

            AppUser user = _mapper.Map<AppUser>(model);
            var result = await _userRepository.RegisterUserAsync(user, model.Password);

            if (result.Succeeded)
            {
                var new_user = await _userRepository.GetUserByEmailAsync(model.Email);

                var tokens = await _jwtService.CreateTokenAsync(new_user);

                return new ServiceResponse
                {
                    Success = true,
                    Message = "A new user was added!",
                    AccessToken = tokens.token,
                    RefreshToken = tokens.refreshToken.Token,
                };
            }

            return new ServiceResponse { Success = false, Message = "User are not created", Errors = result.Errors.Select(e => e.Description) };
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginUserVM model)
        {
            var result = await _userRepository.LoginByEmailAsync(model);

            if (result == true)
            {
                var user = await _userRepository.GetUserByEmailAsync(model.Email);


                var tokens = await _jwtService.CreateTokenAsync(user);



                return new ServiceResponse
                {
                    Success = true,
                    Message = "Tokens:",
                    AccessToken = tokens.token,
                    RefreshToken = tokens.refreshToken.Token,
                };
            }

            return new ServiceResponse { Success = false, Message = "User are not logined" };
        }

        public async Task<ServiceResponse> RefreshTokenAsync(TokenRequestVM model)
        {
            var result = await _jwtService.VerifyTokenAsync(model);
            return result;
        }

        public async Task<ServiceResponse> GetAllUsersAsync()
        {
            var result = _userRepository.GetAllUsersAsync();
            List<GetUserAsContactsVM> newList = new List<GetUserAsContactsVM>();

            foreach (var user in result)
            {
                newList.Add(new GetUserAsContactsVM { Id=user.Id,Email=user.Email,Username=user.UserName});
            }

            if (result.Count != 0)
            {

                return new ServiceResponse
                {
                    Success = true,
                    Contacts = newList,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "no users",
            };
        }

        public async Task<ServiceResponse> GetAllChatsByUserAsync(GetChatsVM model)
        {
            var result = await _userRepository.GetAllChatsByUserAsync(model);

            if (result.Count() != 0)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Gotcha the chats of your user",
                    Chats = result,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "no chats",
            };
        }

        public async Task<ServiceResponse> AddChatAsync(AddChatVM model)
        {
            var result = await _userRepository.AddChatAsync(model);

            if (result == true)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Added chat",
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Chat was not added",
            };
        }

        public async Task<ServiceResponse> GetAllMessagesByChatAsync(GetMessagesByChatIdVM model)
        {
            var result = await _userRepository.GetAllMessagesByChatAsync(model);

            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Founded messages",
                    Messages = result,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "No messages founded",
            };
        }

        public async Task<ServiceResponse> AddMessageAsync(AddMessageVM model)
        {
            var result = await _userRepository.AddMessageAsync(model);

            if (result == true)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Added message",
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Not added message",
            };
        }

        public async Task<ServiceResponse> AddContactToUserAsync(AppUserContactVM model)
        {
            var result = await _userRepository.AddContactToUserAsync(model);

            if (result == true)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Added contact",
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "contact was not added",
            };
        }

        public async Task<ServiceResponse> GetContactsByUserAsync(GetContactsByUserVM model)
        {
            var result = await _userRepository.GetContactsByUserAsync(model);

            if (result.Count != 0)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Gotcha contacts by user",
                    Contacts = result,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "DONT Gotcha contacts by user",
                Contacts = null,
            };
        }

        public async Task<ServiceResponse> GetUserById(GetUserByIdVM model)
        {
            var result = await _userRepository.GetUserById(model);

            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Gotcha user",
                    SingleUser = result,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "DONT Gotcha user",
            };
        }

        public async Task<ServiceResponse> UploadImage(UploadImageVM model)
        {
            var result = _userRepository.UploadImage(model);

            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Image was added",
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Image wasn't added! Error!",
            };
        }

        public async Task<ServiceResponse> UploadAvatar(UploadAvatarVM model)
        {
            var result = await _userRepository.UploadAvatarAsync(model);

            if (result)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Image was added",
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Image wasn't added! Error!",
            };
        }

        public async Task<ServiceResponse> GetAvatar(GetUserByIdVM model)
        {
            var result = await _userRepository.GetAvatar(model);

            if (result != null)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = result,
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Image wasn't founded!",
            };
        }

    }
}
