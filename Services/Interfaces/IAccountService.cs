using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Services.Interfaces;

public interface IAccountService
{
    Task<UserModel> ValidateUser(AccountDTO user);
    bool ValidateEmail(string email);
    bool ValidatePasswordUp(string password);
}
