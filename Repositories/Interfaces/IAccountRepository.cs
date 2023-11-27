using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<UserModel> ValidateUser(UserModel user);
}
