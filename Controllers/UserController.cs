using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoListBk.DTO;
using ToDoListBk.Services.Interfaces;
using ToDoListBk.Utils;

namespace ToDoListBk.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserInsertDTO user)
    {
        try
        {
            var validateExistence = await _userService.ValidateExistence(user);
            if (validateExistence)
            {
                return BadRequest(new { message = "El email " + user.Email + " ya existe!" });

            }
            var validateEmail = _userService.ValidateEmail(user);
            if (!validateEmail)
            {
                return BadRequest(new { message = "El email no es válido!" });

            }
            var validatePassword = _userService.ValidatePasswordUp(user.Password);
            if (!validatePassword)
            {
                return BadRequest(new { message = "El Password no es válido!" });

            }
            user.Password = Encriptar.EncriptarPassword(user.Password);
            await _userService.SaveUser(user);
            return Ok(new { message = "Usuario registrado con Exito!" });

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Route("ChangePassword")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    public async Task<IActionResult> CambiarPassword([FromBody] ChangePasswordDTO changepassword)
    {
        try
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            int idUser = JwtConfigurator.GetTokenIdUsuario(identity);
            string passwordEncriptado = Encriptar.EncriptarPassword(changepassword.OldPassword);
            var user = await _userService.ValidatePassword(idUser, passwordEncriptado);
            if (user == null)
            {
                return BadRequest(new { message = "El password es incorrecto" });
            }
            else
            {
                user.Password = changepassword.NewPassword;
                var validatePassword = _userService.ValidatePasswordUp(user.Password);
                if (!validatePassword)
                {
                    return BadRequest(new { message = "El nuevo password no es válido!" });

                }
                user.Password = Encriptar.EncriptarPassword(changepassword.NewPassword);
                await _userService.UpdatePassword(user);
                return Ok(new { message = "El password fue actualizado con exito!" });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{idGetUser}")]
    public async Task<IActionResult> GetUserId(string idGetUser)
    {
        try
        {
            var user = await _userService.GetUserId(idGetUser.FromHashId());
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{idUser}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(int idUser)
    {
        try
        {
            var user = await _userService.GetUserId(idUser);
            if (user == null)
            {
                return BadRequest(new { message = "No se encontro ningun usuario" });
            }
            await _userService.deleteUser(user);
            return Ok(new { message = "El usuario fue eliminado con exito" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
