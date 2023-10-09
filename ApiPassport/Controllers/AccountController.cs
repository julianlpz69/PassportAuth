using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ApiPassport.Services;
using Auth0.AspNetCore.Authentication;
using Dominio.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;
using Newtonsoft.Json;
using Dominio.Entities;
using ApiPassport.Dto;

namespace ApiPassport.Controllers
{
    
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILoginUser _login_user;


        public AccountController(IUserService userService, IUnitOfWork unitOfWork, ILoginUser loginUser)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _login_user = loginUser;
    }


        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { 
                RedirectUri = Url.Action("GoogleResponse"), 
                 Items =
                {
                    { "prompt", "select_account" } // Esto fuerza la selección de cuenta de Google
                }
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

       [Route("google-response")]
public async Task<IActionResult> GoogleResponse()
{
    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);



    var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Type,
                    claim.Value
                });

    


    var correoElectronicoClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
    var pictureClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value;
    var NombreClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;





    var user = await _unitOfWork.Users.GetByUserGmailAsync(correoElectronicoClaim);

    if (user == null)
    {
        // Redirigir a la página de inicio para usuarios no registrados
            return Redirect("http://127.0.0.1:5500/Pagina%20Reto/Html/Pagina_Registrar.html");    }
    else
    {
      var usuarioLogiado =  new LoginUser{
            UserName = NombreClaim,
            UserEmail = correoElectronicoClaim,
            Picture = pictureClaim,
        };
        _unitOfWork.LoginUsers.Add(usuarioLogiado);
        await _unitOfWork.SaveAsync();

        // Redirigir a la página de inicio para usuarios registrados
        return Redirect("http://127.0.0.1:5500/Pagina%20Reto/Html/Pagina_Inicio_Admin.html");
          
    }
    }






    [Route("google-logout")]
    public async Task<IActionResult> Logout()
{
    

    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    return Redirect("http://127.0.0.1:5500/Pagina%20Reto/Html/Index.html");
}



    [HttpGet]
    [Route("ultimo-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<LoginUser>> Get()
    {
        var Ciudad = await _unitOfWork.LoginUsers.Ultimo();
        return Ok(Ciudad);

    }

    



    [Route("google-register")]
        public IActionResult GoogleRegister()
        {
            var properties = new AuthenticationProperties { 
                RedirectUri = Url.Action("GoogleResponseDos"), 
                 Items =
                {
                    { "prompt", "select_account" } // Esto fuerza la selección de cuenta de Google
                }
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

       [Route("google-response-Dos")]
public async Task<IActionResult> GoogleResponseDos()
{
    var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);


    var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Type,
                    claim.Value
                });

    


    var correoElectronicoClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
    var pictureClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value;
    var NombreClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;





    var user = await _unitOfWork.Users.GetByUserGmailAsync(correoElectronicoClaim);

    if (user == null)
    {
        // Redirigir a la página de inicio para usuarios no registrados
             var usuarioLogiado =  new RegisterDto{
            UserName = NombreClaim,
            UserEmail = correoElectronicoClaim,
             UserPassword = "123456",
        };
        var usuarioJson = JsonConvert.SerializeObject(usuarioLogiado);
        var data2 = new StringContent(usuarioJson, System.Text.Encoding.UTF8, "application/json");
        var httpClient2 = new HttpClient();
        var response2 = await httpClient2.PostAsync("https://localhost:7051/api/user/register", data2);
        

        // Redirigir a la página de inicio para usuarios registrados
        return Redirect("http://127.0.0.1:5500/Pagina%20Reto/Html/Pagina_Login.html");
    }
    else
    {

        return Redirect("http://127.0.0.1:5500/Pagina%20Reto/Html/Pagina_Login.html");
     
          
    }
    }


    }
    







    
}