using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> ValidateExistence(UserModel user);
    Task<UserModel> ValidatePassword(int idUser, string OldPassword);
    Task SaveUser(UserModel user);
    Task<UserModel> GetUserId(int idGetUser);
    Task deleteUser(UserModel user);
    Task UpdatePassword(UserModel user);
}
