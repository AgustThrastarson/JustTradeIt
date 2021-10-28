using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;

namespace JustTradeIt.Software.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        UserDto CreateUser(RegisterInputModel inputModel);
        UserDto AuthenticateUser(LoginInputModel loginInputModel);
        void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile);
        UserDto GetProfileInformation(string email);
        UserDto GetUserInformation(string userIdentifier);
    }
}