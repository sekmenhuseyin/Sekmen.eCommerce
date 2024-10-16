// ReSharper disable UnusedType.Global
namespace Sekmen.Commerce.Services.Auth.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        _ = CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}