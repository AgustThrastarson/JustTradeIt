using System;
using System.Linq;
using System.Text;
using JustTradeIt.Software.API.Models.Dtos;
using JustTradeIt.Software.API.Models.InputModels;
using JustTradeIt.Software.API.Repositories.Contexts;
using JustTradeIt.Software.API.Repositories.Entities;
using JustTradeIt.Software.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using JustTradeIt.Software.API.Repositories.Helpers;

namespace JustTradeIt.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly TradeItDbContext _dbContext;
        private string _salt = "00209b47-08d7-475d-a0fb-20abf0872ba0";


        public UserRepository(TradeItDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        
        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == HashHelper.HashPassword(loginInputModel.Password, _salt));
            if (user == null) { return null; }

            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();

            return new UserDto
            {
                Identifier = user.PublicIdentifier,
                Email = user.Email,
                FullName = user.FullName,
                ProfileImageUrl = user.ProfileImageUrl,
                TokenId = token.Id
            };
        }
        

        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            // Check if user with email already exists
            var email = _dbContext.Users.FirstOrDefault(u => u.Email == inputModel.Email);
            if (email != null)
            {
                throw new Exception("User with email " + inputModel.Email + " found.");
            }
            

            // Create new user
            var entity = new User
            {
                Email = inputModel.Email,
                FullName = inputModel.FullName,
                HashedPassword = HashHelper.HashPassword(inputModel.Password, _salt)
            };
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();

            // Create token
            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();


            return new UserDto
            {
                Identifier = Guid.NewGuid().ToString(),
                Email = entity.Email,
                FullName = entity.FullName

            }; 
        }

        public UserDto GetProfileInformation(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            
            // Create token
            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();

            
            return new UserDto
            {
                Identifier = user.PublicIdentifier,
                Email = user.Email,
                FullName = user.FullName,
                ProfileImageUrl = user.ProfileImageUrl
            };
        }

        public UserDto GetUserInformation(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(string email, string profileImageUrl, ProfileInputModel profile)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            user.FullName = profile.FullName;
            user.ProfileImageUrl = profileImageUrl;
            _dbContext.SaveChanges();

        }
    }
}