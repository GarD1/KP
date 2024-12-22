using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Users
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    // Другие действия (Create, Edit, Delete)...
}
