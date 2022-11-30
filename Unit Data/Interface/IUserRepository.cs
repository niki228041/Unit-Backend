using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.ImpUserRepository;
using Unit_Data.Models;
using Unit_Data.Models.Models;

namespace Unit_Data.Interface
{
    public interface IUserRepository
    {
        public Task<IdentityResult> RegisterUserAsync(AppUser model, string password);
        public Task<bool> LoginByEmailAsync(LoginUserVM model);
        public Task<AppUser> GetUserByEmailAsync(string email);
        public Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        public void SetRefreshToken(RefreshToken newRefreshToken);
        public Task<IList<string>> GetRolesByUserAsync(AppUser model);
        public Task<IdentityResult> AddRoleToUserAsync(AppUser model, string role);
        public Task CreateRoleAsync(string role);
        public Task<RefreshToken> CheckRefreshTokenAsync(string refreshToken);
        public Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
        public Task<AppUser> GetUserByIdAsync(string id);
        public List<AppUser> GetAllUsersAsync();
        public Task<IEnumerable<Chat>> GetAllChatsByUserAsync(GetChatsVM model);
        public Task<bool> AddChatAsync(AddChatVM model);
        public Task<IEnumerable<MessageResponse>> GetAllMessagesByChatAsync(GetMessagesByChatIdVM model);
        public Task<bool> AddMessageAsync(AddMessageVM model);
        public Task<bool> AddContactToUserAsync(AppUserContactVM model);
        public Task<List<GetUserAsContactsVM>> GetContactsByUserAsync(GetContactsByUserVM model);


    }
}
