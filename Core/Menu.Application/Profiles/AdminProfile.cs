using AutoMapper;
using Menu.Application.Dtos.Account;
using Menu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Application.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            // RegisterDto -> Admin
            CreateMap<RegisterDto, Admin>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Kullanıcı adı e-posta olsun
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname));
            // ID Identity tarafından atanır

            // LoginDto -> Admin (Sadece mapleme için, Identity doğrulaması için kullanılmaz)
            CreateMap<LoginDto, Admin>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
