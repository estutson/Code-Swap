using System;
using System.Web;
using System.Web.Mvc;
using ShoppingListApp.Models;
using ShoppingListApp.Services;
using Microsoft.AspNet.Identity;
using ShoppingListApp.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace ShoppingList.Web.Controllers
{
    public class ShoppingListItemAppController : Controller
    {
        private readonly Lazy<ShoppingListItemAppService> _svc;

        public ShoppingListItemAppController()
        {
            _svc =
                new Lazy<ShoppingListItemAppService>(
                    () =>
                    {
                        return new ShoppingListItemAppService();
                    });
        }

        public ActionResult Index(string sortOrder, string curentFilter, int id)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ContentsSortOrder = String.IsNullOrEmpty(sortOrder) ? "ContentsDesc" : "";
            ViewBag.PrioritySortOrder = sortOrder == "Priority" ? "PriorityDesc" : "Priority";
            ViewBag.IsCheckedSortOrder = sortOrder == "IsChecked" ? "IsCheckedDesc" : "IsChecked";




            var ShoppingListItems = _svc.Value.GetItems(id);

            var shoppingListItems = from items in ShoppingListItems
                                    select items;
            //Sorting
            switch (sortOrder)
            {
                case "ContentsDesc":
                    ShoppingListItems = ShoppingListItems.OrderByDescending(s => s.Contents);
                    break;
                case "Priority":
                    ShoppingListItems = ShoppingListItems.OrderBy(s => s.Priority);
                    break;
                case "PriorityDesc":
                    ShoppingListItems = ShoppingListItems.OrderByDescending(s => s.Priority);
                    break;
                case "IsChecked":
                    ShoppingListItems = ShoppingListItems.OrderBy(s => s.IsChecked);
                    break;
                case "IsCheckedDesc":
                    ShoppingListItems = ShoppingListItems.OrderByDescending(s => s.IsChecked);
                    break;
                default:
                    ShoppingListItems = ShoppingListItems.OrderBy(s => s.Contents);
                    break;
            }

            return View(ShoppingListItems);

        }

        public ActionResult Create()
        {
            var vm = new ShoppingListItemCreateModel();

            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingListItemCreateModel vm, int Id, HttpPostedFileBase upload, File file)
        {
            if (!ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var image = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Image,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        image.Content = reader.ReadBytes(upload.ContentLength);
                    }
                }
            }

            try
            {
                _svc.Value.CreateItem(vm, Id, file);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to create note");
                return View(vm);
            }
            return RedirectToAction("Index/" + Url.RequestContext.RouteData.Values["id"]);
        }

            
        

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult DeleteGet(int Id, int ShoppingListId)
        {
            var detail = _svc.Value.GetItemById(Id, ShoppingListId);

            return View(detail);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int Id, int ShoppingListId)
        {
            _svc.Value.DeleteItem(Id, ShoppingListId);

            return RedirectToAction("Index", new { Id = ShoppingListId });
        }

        public ActionResult DeleteAllItems(int id)
        {
            _svc.Value.DeleteAllItems();

            return RedirectToAction("Index/" + Url.RequestContext.RouteData.Values["id"]);
        }

        public ActionResult Edit(int Id, int ShoppingListId)
        {
            var detail = _svc.Value.GetItemById(Id, ShoppingListId);
            var list =
                new ShoppingListItemEditModel
                {
                    Id = detail.Id,
                    ShoppingListId = detail.ShoppingListId,
                    Contents = detail.Contents,
                    IsChecked = detail.IsChecked,
                    Priority = (ShoppingListItemEditModel.PriorityMessage)detail.Priority
                };

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ShoppingListItemEditModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (!_svc.Value.UpdateItem(vm))
            {
                ModelState.AddModelError("", "Unable to update list");
                return View(vm);
            }

            return RedirectToAction("Index", new { Id = vm.ShoppingListId});
        }

        public ActionResult DeleteAllChecked(int id, int[] IdsToBeDeleted)
        {
            if (IdsToBeDeleted != null && IdsToBeDeleted.Length > 0)
                _svc.Value.DeleteAllChecked(IdsToBeDeleted);

            return RedirectToAction("Index", new { Id = Url.RequestContext.RouteData.Values["id"] });
        }
    }
}
