using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

public class AccountController : Controller
{
    private ApplicationUser Manager _userManager;

    public AccountController()
    {
    }

    public AccountController(ApplicationUser Manager userManager)
    {
        UserManager = userManager;
    }

    public ApplicationUser Manager UserManager
    {
        get
        {
            return _userManager ?? HttpContext.GetOwinContext().GetUser Manager<ApplicationUser Manager>();
}
private set
{
    _userManager = value;
}
    }

    // GET: Account/Register
    public ActionResult Register()
{
    return View();
}

// POST: Account/Register
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Register(RegisterViewModel model)
{
    if (ModelState.IsValid)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await UserManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("Index", "Home");
        }
        AddErrors(result);
    }

    return View(model);
}

// GET: Account/Login
public ActionResult Login(string returnUrl)
{
    ViewBag.ReturnUrl = returnUrl;
    return View();
}

// POST: Account/Login
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
{
    if (ModelState.IsValid)
    {
        var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        switch (result)
        {
            case SignInStatus.Success:
                return RedirectToLocal(returnUrl);
            case SignInStatus.LockedOut:
                return View("Lockout");
            case SignInStatus.RequiresVerification:
                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            case SignInStatus.Failure:
            default:
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
        }
    }

    return View(model);
}

// POST: Account/Logout
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Logout()
{
    SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
    return RedirectToAction("Index", "Home");
}

// Вспомогательные методы для обработки ошибок
private void AddErrors(IdentityResult result)
{
    foreach (var error in result.Errors)
    {
        ModelState.AddModelError("", error);
    }
}

private ActionResult RedirectToLocal(string returnUrl)
{
    if (Url.IsLocalUrl(returnUrl))
    {
        return Redirect(returnUrl);
    }
    return RedirectToAction("Index", "Home");
}
}