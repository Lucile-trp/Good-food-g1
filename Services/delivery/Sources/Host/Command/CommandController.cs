using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Command
{
    public class CommandController : Controller
    {
        // GET: CommandController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CommandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
