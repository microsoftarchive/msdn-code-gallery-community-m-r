using DataAccessEfCore.DTOs;

namespace DataAccessEfCore.Repositories.Abstractions
{
    public interface IUserRepo
    {
        UserDTO GetUserByEmail(string email);
    }
}
