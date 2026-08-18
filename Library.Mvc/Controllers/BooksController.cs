using System.Web.Mvc;
using Library.Core.Models;
using Library.Core.Data;
using System.Configuration;

namespace Library.Mvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookRepository _repository;

        public BooksController()
        {
            _repository = new BookRepository(ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString);
        }

        public ActionResult Index()
        {
            var books = _repository.GetAll();

            return View(books);
        }

        public ActionResult Details(int id) 
        { 
            var book = _repository.GetById(id); 
            if (book == null) 
                return HttpNotFound();

            var contents = _repository.GetContents(id);

            ViewBag.Contents = contents;

            return View(book); 
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Book book)
        {         
            if (!ModelState.IsValid)
                return View(book);

            var id = _repository.Insert(book);

            return RedirectToAction("Details", new { id });
        }

        public ActionResult Edit(int id)
        {
            var book = _repository.GetById(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
                return View(book);

            _repository.Update(book);

            return RedirectToAction("Details", new { id = book.Id });
        }

        public ActionResult Delete(int id)
        {
            var book = _repository.GetById(id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}