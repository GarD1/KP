using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class TagsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Tags
    public IActionResult Index()
    {
        var tags = _context.Tags.ToList();
        return View(tags);
    }

    // Другие действия (Create, Edit, Delete)...
}
