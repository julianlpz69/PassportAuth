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

namespace ApiPassport.Controllers
{
    
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUserService userService, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
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


    foreach (var identity in result.Principal.Claims)
    
            {
                Console.WriteLine(CookieAuthenticationDefaults.AuthenticationScheme.ToString());
                Console.WriteLine($"Claim type : {identity.Type} value: {identity.Value}, ");
            }

    var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Type,
                    claim.Value
                });

    


    var correoElectronicoClaim = result.Principal?.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;



   
    HttpContext.Response.Cookies.Append("MiCookie", "valorWWWWW");
        // Crea una nueva cookie con el nombre "MiCookie" y el valor deseado.
        

    
        Response.Headers.Add("Mi-Header", "ssssssssss");

    

        // Escribir el JSON en el archivo


    var user = await _unitOfWork.Users.GetByUserGmailAsync(correoElectronicoClaim);

    if (user == null)
    {
        // Redirigir a la página de inicio para usuarios no registrados
            return Redirect("http://127.0.0.1:5500/Pagina%20Reto/Html/Pagina_Login.html");    }
    else
    {
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





  

    }
    

}