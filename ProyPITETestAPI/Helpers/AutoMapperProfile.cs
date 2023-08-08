using AutoMapper;
using Proyecto.PiteApi.Dtos;
using Proyecto.PiteApi.Models;

namespace Proyecto.PiteApi.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        #region User
        CreateMap<CreateUserDto, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }));

        CreateMap<UpdatePersonUserDto, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }));
        
        CreateMap<User, UserDto>()
            .ForAllMembers(x => x.Condition(
                                  (src, dest, prop) =>
                                  {
                                      if (prop == null) return false;
                                      if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                                      return true;
                                  }));
        #endregion

    }
}
