using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingListApp.Models;
using ShoppingListApp.Services;
using Microsoft.AspNet.Identity;
using ShoppingListApp.Models.ListTemplates;

namespace ShoppingList.Web.Controllers
{
    [Authorize]
    public class ShoppingListAppController : Controller
    {
        private readonly Lazy<ShoppingListAppService> _svc;

        public ShoppingListAppController()
        {
            _svc =
                new Lazy<ShoppingListAppService>(
                    () =>
                    {
                        var userId = Guid.Parse(User.Identity.GetUserId());
                        return new ShoppingListAppService(userId);
                    });
        }

        public ActionResult Index(string sortOrder, string curentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortOrder = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewBag.ColorSortOrder = sortOrder == "Color" ? "ColorDesc" : "Color";

            var ShoppingLists = _svc.Value.GetList();

            var shoppingLists = from lists in ShoppingLists
                                select lists;
            //Sorting
            switch (sortOrder)
            {
                case "NameDesc":
                    ShoppingLists = ShoppingLists.OrderByDescending(s => s.Name);
                    break;
                case "ColorDesc":
                    ShoppingLists = ShoppingLists.OrderByDescending(s => s.Color);
                    break;
                case "Color":
                    ShoppingLists = ShoppingLists.OrderBy(s => s.Color);
                    break;
                default:
                    ShoppingLists = ShoppingLists.OrderBy(s => s.Name);
                    break;
            }

            return View(ShoppingLists);

        }

        public ActionResult Create()
        {
            var vm = new ShoppingListCreateModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingListCreateModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.CreateList(vm))
            {
                ModelState.AddModelError("", "Unable to create list");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int id)
        {
            var detail = _svc.Value.GetListById(id);

            return View(detail);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _svc.Value.DeleteList(id);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAllLists()
        {
            _svc.Value.DeleteAllLists();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var detail = _svc.Value.GetListById(id);
            var list =
                new ShoppingListEditModel
                {
                    Id = detail.Id,
                    Name = detail.Name
                };

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShoppingListEditModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.UpdateList(vm))
            {
                ModelState.AddModelError("", "Unable to update list");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CreateListTemplate()
        {
            var vm = new ShoppingListTemplate();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateListTemplate(ShoppingListTemplate vm)
        {
            if (!ModelState.IsValid) return View(vm);

            _svc.Value.CreateListTemplate(vm);

            return RedirectToAction("Index");
        }
    }
}
