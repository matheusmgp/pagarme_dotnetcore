
using Domain.Entities;

namespace Application.Services.Contracts
{
    public interface IUserService
    {
        UserEntity GetById(int id);
    }
}