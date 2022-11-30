using AutoMapper;
using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit_Data.Models;
using Unit_Data.Models.Models;

namespace Unit_Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserVM, AppUser>();
            CreateMap<AppUser, RegisterUserVM>();
        }
    }
}
