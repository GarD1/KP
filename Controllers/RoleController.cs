using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApplication7.Models;

public class RoleController : Controller
{
    private RoleManager<IdentityRole> _roleManager;

    public RoleController()
    {
        _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
    }

    // GET: Role/Index
    public ActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    // POST: Role/CreateRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateRole(string roleName)
    {
        if (!string.IsNullOrWhiteSpace(roleName))
        {
            var role = new IdentityRole(roleName);
            var result = _roleManager.Create(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            // Обработка ошибок, если роль не была создана
            ModelState.AddModelError("", string.Join(", ", result.Errors));
        }
        return View();
    }

    // GET: Role/AssignRole
    public ActionResult AssignRole()
    {
        // Получаем список пользователей и ролей для назначения
        var users = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())).Users.ToList();
        var roles = _roleManager.Roles.ToList();
        ViewBag.Users = users;
        ViewBag.Roles = roles;
        return View();
    }

    // POST: Role/AssignRole
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AssignRole(string userId, string roleName)
    {
        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        var result = userManager.AddToRole(userId, roleName);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        // Обработка ошибок, если роль не была назначена
        ModelState.AddModelError("", string.Join(", ", result.Errors));
        return View();
    }
}
