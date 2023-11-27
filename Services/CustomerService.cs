using ToDoListBk.Services.Interfaces;

namespace ToDoListBk.Services;

public class CustomerService: ICustomerService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetUserLogin()
    {

        return _httpContextAccessor.HttpContext.User.Identity.Name;
    }

    public string GetUserId()
    {

        if (_httpContextAccessor.HttpContext.User.HasClaim(c => c.Type == "idUser"))
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "idUser").Value;
        }

        return "";
    }
}
