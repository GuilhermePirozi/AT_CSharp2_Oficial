using System.Security.Claims;
using AT_CSharp2_Oficial.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AT_CSharp2_Oficial.Pages.Login {
    public class LoginModel : PageModel {
        [BindProperty] public string Login { get; set; }
        [BindProperty] public string Senha { get; set; }
        public string Erro { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync() {
            if (LoginService.IsValidLogin(Login, Senha)) {
                var claims = new List<Claim>
                {
            new Claim(ClaimTypes.Name, Login)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    });

                return LocalRedirect(Url.Content("~/"));
            }

            Erro = "Login inválido";
            return Page();
        }
    }
}
