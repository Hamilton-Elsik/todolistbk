using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListBk.DTO;
using ToDoListBk.Services.Interfaces;
using ToDoListBk.Utils;

namespace ToDoListBk.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IConfiguration _config;

    public AccountController(IAccountService accountService, IConfiguration config)
    {
        _accountService = accountService;
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AccountDTO usuario)
    {
        try
        {
            var validateEmail = _accountService.ValidateEmail(usuario.Email);
            if (!validateEmail)
            {
                return BadRequest(new { message = "El email no es válido!" });

            }
            var validatePassword = _accountService.ValidatePasswordUp(usuario.Password);
            if (!validatePassword)
            {
                return BadRequest(new { message = "El Password no es válido!" });

            }
            usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
            var user = await _accountService.ValidateUser(usuario);
            if (user == null)
            {
                return BadRequest(new { message = "Usuario o contraseña invalidos" });
            }
            string tokenString = JwtConfigurator.GetToken(user, _config);
            return Ok(new { token = tokenString });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
