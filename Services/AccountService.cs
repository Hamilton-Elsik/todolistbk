using AutoMapper;
using System.Text.RegularExpressions;
using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;
using ToDoListBk.Repositories.Interfaces;
using ToDoListBk.Services.Interfaces;

namespace ToDoListBk.Services;

public class AccountService: IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }
    public async Task<UserModel> ValidateUser(AccountDTO user)
    {
        var model = _mapper.Map<UserModel>(user);
        var data = await _accountRepository.ValidateUser(model);
        return data;
    }

    public bool ValidateEmail(string email)
    {
        try
        {
            return Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
        catch
        {
            return false;
        }
    }

    public bool ValidatePasswordUp(string password)
    {
        Regex letrasM = new Regex(@"[A-Z]");
        Regex letras = new Regex(@"[a-z]");
        Regex numeros = new Regex(@"[0-9]");
        Regex caracEsp = new Regex("[!\"#\\$%&'()*+,-./:;=?@\\[\\]^_`{|}~]");

        Boolean cumpleCriterios = false;

        if (password.Length < 10)
        {
            return false;
        }

        if (!letrasM.IsMatch(password))
        {
            return false;
        }
        if (!letras.IsMatch(password))
        {
            return false;
        }
        if (!caracEsp.IsMatch(password))
        {
            return false;
        }
        return true;
    }
}
