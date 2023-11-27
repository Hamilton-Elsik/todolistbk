using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Text.RegularExpressions;
using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;
using ToDoListBk.Repositories.Interfaces;
using ToDoListBk.Services.Interfaces;

namespace ToDoListBk.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<bool> ValidateExistence(UserInsertDTO user)
    {
        var model = _mapper.Map<UserModel>(user);
        return await _userRepository.ValidateExistence(model);
    }

    public bool ValidateEmail(UserInsertDTO user)
    {
        try
        {
            return Regex.IsMatch(user.Email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
        catch
        {
            return false;
        }
    }

    public async Task<UserModel> ValidatePassword(int idUser, string OldPassword)
    {
        var data = await _userRepository.ValidatePassword(idUser, OldPassword);
        return data;
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

    public async Task SaveUser(UserInsertDTO user)
    {
        var date = DateTime.Now;
        var model = _mapper.Map<UserModel>(user);
        model.CreatedAt = date;
        await _userRepository.SaveUser(model);
    }
    public async Task<UserDTO> GetUserId(int idGetUser)
    {
        var user = await _userRepository.GetUserId(idGetUser);
        var model = _mapper.Map<UserDTO>(user);
        return model;
    }
    public async Task UpdatePassword(UserModel user)
    {
        await _userRepository.UpdatePassword(user);
    }
    public async Task deleteUser(UserDTO user)
    {
        var model = _mapper.Map<UserModel>(user);
        await _userRepository.deleteUser(model);
    }
}
