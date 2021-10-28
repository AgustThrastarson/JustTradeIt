using System.Threading.Tasks;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;

namespace JustTradeIt.Software.API.Services.Interfaces
{
    public interface IAccountService
    {
        UserDto CreateUser(RegisterInputModel inputModel);
        UserDto AuthenticateUser(LoginInputModel loginInputModel);
        void Logout(int tokenId);
        Task UpdateProfile(string email, ProfileInputModel profile);
        UserDto GetProfileInformation(string name);
    }
}