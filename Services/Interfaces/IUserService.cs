using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Services.Interfaces;

public interface IUserService
{
    Task<bool> ValidateExistence(UserInsertDTO user);
    bool ValidateEmail(UserInsertDTO user);
    Task<UserModel> ValidatePassword(int idUser, string OldPassword);
    bool ValidatePasswordUp(String password);
    Task UpdatePassword(UserModel user);
    Task SaveUser(UserInsertDTO user);
    Task<UserDTO> GetUserId(int idUser);
    Task deleteUser(UserDTO user);
}
