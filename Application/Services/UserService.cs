
using Application.Services.Contracts;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        public UserEntity GetById(int id)
        {
            return new UserEntity
            {
                UserName = "Matheus",
                PasswordHash = "10101"
            };
        }
    }
}