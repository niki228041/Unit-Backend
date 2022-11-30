using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_Data.Models.Models
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<Chat> Chats { get; set; }
        public IEnumerable<MessageResponse> Messages { get; set; }
        public IEnumerable<GetUserAsContactsVM> Contacts { get; set; }
        
    }
}
