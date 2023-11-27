using Microsoft.EntityFrameworkCore;
using ToDoListBk.Persistence.Context;
using ToDoListBk.Persistence.Models;
using ToDoListBk.Repositories.Interfaces;

namespace ToDoListBk.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AplicationDbContext _context;
    public UserRepository(AplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> ValidateExistence(UserModel user)
    {
        var validateExistence = await _context.User.AnyAsync(x => x.Email == user.Email);

        return validateExistence;
    }
    public async Task<UserModel> ValidatePassword(int idUser, string oldPassword)
    {
        var user = await _context.User.Where(x => x.UserId == idUser && x.Password == oldPassword).FirstOrDefaultAsync();

        return user;
    }
    public async Task SaveUser(UserModel user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserModel> GetUserId(int idGetUser)
    {
        var User = await _context.User.Where(x => x.UserId == idGetUser
                                                              )
                                                                .FirstOrDefaultAsync();
        return User;
    }

    public async Task deleteUser(UserModel user)
    {
        user.IsActive = false;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePassword(UserModel user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

}
