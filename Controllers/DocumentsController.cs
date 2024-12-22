using System.Linq;
using System.Web.Mvc; // Используем пространство имен для классического ASP.NET MVC
using System.Data.Entity; // Для работы с Entity Framework
using System.Data.Entity.Infrastructure;

public class DocumentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DocumentsController()
    {
        _context = new ApplicationDbContext(); // Инициализируем контекст
    }

    // GET: Documents
    public ActionResult Index()
    {
        var documents = _context.Documents.ToList();
        return View(documents);
    }

    // GET: Documents/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Documents/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Document document)
    {
        if (ModelState.IsValid)
        {
            _context.Documents.Add(document);
            _context.SaveChanges(); // Синхронный метод сохранения
            return RedirectToAction("Index");
        }
        return View(document);
    }

    // GET: Documents/Edit/5
    public ActionResult Edit(int id)
    {
        var document = _context.Documents.Find(id);
        if (document == null)
        {
            return HttpNotFound();
        }
        return View(document);
    }

    // POST: Documents/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Document document)
    {
        if (id != document.Id)
        {
            return HttpNotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Entry(document).State = EntityState.Modified; // Устанавливаем состояние как Modified
                _context.SaveChanges(); // Синхронный метод сохранения
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(document.Id))
                {
                    return HttpNotFound();
                }
                throw;
            }
            return RedirectToAction("Index");
        }
        return View(document);
    }

    // GET: Documents/Delete/5
    public ActionResult Delete(int id)
    {
        var document = _context.Documents.Find(id);
        if (document == null)
        {
            return HttpNotFound();
        }
        return View(document);
    }

    // POST: Documents/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var document = _context.Documents.Find(id);
        if (document != null)
        {
            _context.Documents.Remove(document);
            _context.SaveChanges(); // Синхронный метод сохранения
        }
        return RedirectToAction("Index");
    }

    private bool DocumentExists(int id)
    {
        return _context.Documents.Any(e => e.Id == id);
    }
}