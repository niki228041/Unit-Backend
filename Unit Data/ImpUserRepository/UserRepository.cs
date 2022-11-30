using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Interface;
using Unit_Data.Models;
using Unit_Data.Models.Models;
using System.Web;
using Unit_Data.Db;
using System.Data;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Unit_Data.ImpUserRepository
{
    public class UserRepository : IUserRepository
    {

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        //private HttpResponse Response;
        private IConfiguration _configuration;

        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            //,HttpResponse _response
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            //Response = _response;
        }

        public async Task<bool> LoginByEmailAsync(LoginUserVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var login = await _userManager.CheckPasswordAsync(user, model.Password);

            if (login)
            {
                await _signInManager.SignInAsync(user,isPersistent: model.RememberMe);
            }

            return login;
        }

        public async Task<IdentityResult> RegisterUserAsync(AppUser model,string password)
        {
            var result = await _userManager.CreateAsync(model,password);

            return result;
        }


        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            using (var _context = new UnitDbContext())
            {
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            //Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        }

        public async Task<IList<string>> GetRolesByUserAsync(AppUser model)
        {
            var roles = await _userManager.GetRolesAsync(model);
            return roles;
        }

        public async Task<IdentityResult> AddRoleToUserAsync(AppUser model, string role)
        {
            var roles = await _userManager.AddToRoleAsync(model,role);
            return roles;
        }

        public async Task CreateRoleAsync(string role)
        {
            using(UnitDbContext dbContext = new UnitDbContext())
            {
                var newRole = new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper(),
                };

                await dbContext.Roles.AddAsync(newRole);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken> CheckRefreshTokenAsync(string refreshToken)
        {
            using (var _context = new UnitDbContext())
            {
                var result = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
                if (result != null)
                {
                    return result;
                }
            }
            return new RefreshToken();
        }
        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            using (var _context = new UnitDbContext())
            {
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return result;
        }

        public List<AppUser> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            return users;
        }

    //{
    //  "name": "string",
    //  "usersId": [
    //      "f8a7d1ed-5964-4e51-9b86-6075ad3954cc",
    //      "fa7f1f20-3ca6-4f91-ae0e-707cf380f614"
    //  ]
    //}

    public async Task<IEnumerable<Chat>> GetAllChatsByUserAsync(GetChatsVM model)
        {
            List<Chat> chats = new List<Chat>();
            var user = await _userManager.FindByIdAsync(model.UserId);

            List<AppUserVM> vms_;

            using (var _context = new UnitDbContext())
            {
                vms_ = _context.UserVMs.ToList();
            }

            using (var _context = new UnitDbContext())
            {
                var chats_ = _context.Chats;

                foreach (var chat in chats_)
                {
                    foreach (var userInChat in vms_)
                    {
                        if (userInChat.ChatId == chat.Id)
                        {
                            if (user.Id == userInChat.UserId)
                            {
                                chats.Add(chat);
                                break;
                            }
                        }
                    }
                }
            }
            return chats;

        }

        public async Task<bool> AddChatAsync(AddChatVM model)
        {
            try
            {
                List<AppUser> users = new List<AppUser>();

                foreach (var item in model.UsersId)
                {
                    users.Add(await _userManager.FindByIdAsync(item));
                }

                //Message mess = new Message { Text = "hi bitch", User = users[0] };

                List<Message> mesages = new List<Message>();
                //mesages.Add(mess);

                //IEnumerable<AppUser> users_ienumerable = users;

                using (var _context = new UnitDbContext())
                {
                    Chat chat = new Chat
                    {
                        Name = model.Name,
                        Created = DateTime.Now,
                        Messages = mesages
                    };

                    var listOfUserToAddChat = new List<AppUserVM>();

                    foreach (var item in users)
                    {
                        listOfUserToAddChat.Add(new AppUserVM { UserId = item.Id });
                    }

                    chat.Users = listOfUserToAddChat;

                    _context.Chats.Add(chat);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception ex)
            {

                return false;
            }
        }

        public async Task<IEnumerable<MessageResponse>> GetAllMessagesByChatAsync(GetMessagesByChatIdVM model)
        {
            List<MessageResponse> result_messages = new List<MessageResponse>();
            List<Message> messages;

            using (var _context = new UnitDbContext())
            {
                messages = _context.Messages.ToList();
            }


            using (var _context = new UnitDbContext())
            {
                var messVm = _context.MessageUserVMs;
                List<Message> messages_founded = new List<Message>();


                foreach (var message in messages)
                {
                    var myMessageVm = messVm.Find(message.UserAndChatId);

                    if (myMessageVm != null && model.ChatId == myMessageVm.ChatId)
                    {
                        result_messages.Add(new MessageResponse {message=message,userId= myMessageVm.UserId });
                    }
                }

                return result_messages;
            }
        }

        public async Task<bool> AddMessageAsync(AddMessageVM model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                Chat chat;


                if (user != null)
                {
                    using (var _context = new UnitDbContext())
                    {
                        chat = _context.Chats.Find(model.ChatId);

                        if (chat != null)
                        {
                            var UserAndChat = new MessageUserVM { UserId = user.Id, ChatId = chat.Id };

                            Message newMessage = new Message
                            {
                                Created = DateTime.Now,
                                Text = model.Text,
                                UserAndChat = UserAndChat,
                            };

                            if (chat.Messages == null)
                            {
                                chat.Messages = new List<Message>();
                            }

                            chat.Messages.Add(newMessage);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> AddContactToUserAsync(AppUserContactVM model)
        {
            try
            {
                using (var _context = new UnitDbContext())
                {
                    var user = await _userManager.FindByIdAsync(model.userId);
                    var contact = await _userManager.FindByIdAsync(model.contactId);

                    var newContactReleicheschip = new AppUserContact
                    {
                        UserId = user.Id,
                        ContactId = contact.Id,
                    };

                    await _context.AppUserContacts.AddAsync(newContactReleicheschip);
                    await _context.SaveChangesAsync();
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<GetUserAsContactsVM>> GetContactsByUserAsync(GetContactsByUserVM model)
        {
            List<GetUserAsContactsVM> list = new List<GetUserAsContactsVM>();
            try
            {
                using (var _context = new UnitDbContext())
                {
                    var user = await _userManager.FindByIdAsync(model.userId);


                    var contacts = _context.AppUserContacts.ToList();
                    
                    foreach (var contact in contacts)
                    {
                        if(contact.UserId == user.Id)
                        {
                            var user_tmp = await _userManager.FindByIdAsync(contact.ContactId);
                            list.Add(
                                new GetUserAsContactsVM{
                                    Id = user_tmp.Id,
                                    Email = user_tmp.Email,
                                    Username = user_tmp.UserName
                                }
                                );
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                return list;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
    public class Tokens
    {
        public string token { get; set; }
        public RefreshToken refreshToken { get; set; }
    }
}
