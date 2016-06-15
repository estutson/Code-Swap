using ShoppingListApp.Models;
using ShoppingListApp.Services;
using System;
using System.Web.Mvc;

namespace ShoppingListApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly Lazy<NotesService> _svc;

        public NotesController()
        {
            _svc =
                new Lazy<NotesService>(
                    () =>
                    {
                        return new NotesService();
                    });
        }

        public ActionResult Index(int id)
        {
            var vm = _svc.Value.GetNotes(id);

            return View(vm);

        }

        public ActionResult Create()
        {
            var vm = new NoteCreateModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreateModel vm, int Id)
        {
            if (!ModelState.IsValid) return View(vm);

            _svc.Value.CreateNote(vm, Id);
            //if (!_svc.Value.CreateNote(vm, Id))
            //{
            //    ModelState.AddModelError("", "Unable to create note");
            //    return View(vm);
            //}

            return RedirectToAction("Index/" + Url.RequestContext.RouteData.Values["id"]);
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int Id, int ShoppingListItemId)
        {
            var detail = _svc.Value.GetNoteById(Id, ShoppingListItemId);

            return View(detail);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int Id, int ShoppingListItemId)
        {
            _svc.Value.DeleteNote(Id, ShoppingListItemId);

            return RedirectToAction("Index/" + ShoppingListItemId);
        }

        public ActionResult DeleteAllNotes()
        {
            _svc.Value.DeleteAllNotes();

            return RedirectToAction("Index/" + Url.RequestContext.RouteData.Values["id"]);
        }

        public ActionResult Edit(int Id, int ShoppingListItemId)
        {
            var detail = _svc.Value.GetNoteById(Id, ShoppingListItemId);
            var list =
                new NoteEditModel
                {
                    Id = detail.Id,
                    ShoppingListItemId = detail.ShoppingListItemId,
                    Body = detail.Body
                };

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NoteEditModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.UpdateNote(vm))
            {
                ModelState.AddModelError("", "Unable to update note");
                return View(vm);
            }

            return RedirectToAction("Index/" + vm.ShoppingListItemId);
        }
    }
}