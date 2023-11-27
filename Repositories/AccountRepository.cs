using Microsoft.EntityFrameworkCore;
using ToDoListBk.Persistence.Context;
using ToDoListBk.Persistence.Models;
using ToDoListBk.Repositories.Interfaces;

namespace ToDoListBk.Repsoitories;

public class AccountRepository: IAccountRepository
{
    private readonly AplicationDbContext _context;
    public AccountRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel> ValidateUser(UserModel user)
    {
        var usuario = await _context.User.Where(x => x.Email == user.Email
                                      && x.Password == user.Password).FirstOrDefaultAsync();
        return usuario;
    }
}
