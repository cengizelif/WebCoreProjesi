using AutoMapper;
using WebCoreProjesi.Models.Entities;
using WebCoreProjesi.Models.ViewModel;

namespace WebCoreProjesi
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User,UserModel>().ReverseMap();
            CreateMap<User,CreateUserModel>().ReverseMap();
            CreateMap<User,EditUserModel>().ReverseMap();   
        }
    }
}
