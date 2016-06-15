using System.Collections.Generic;
using System.Linq;
using ShoppingListApp.Data;
using ShoppingListApp.Models;
using System;
using System.Web.Services.Description;

namespace ShoppingListApp.Services
{
    public class ShoppingListItemAppService
    { 
        public ShoppingListItemAppService()
        {
        }

        public IEnumerable<ShoppingListItemModel> GetItems(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .ShoppingListItem
                    .Where(e => e.ShoppingListId == id)
                    .Select(
                        e =>
                            new ShoppingListItemModel
                            {
                                Id = e.Id,
                                ShoppingListId = e.ShoppingListId,
                                Contents = e.Contents,
                                IsChecked = e.IsChecked,
                                HasNotes = e.HasNotes,
                                Priority = (ShoppingListItemModel.PriorityMessage)e.Priority,
                                CreatedUTC = e.CreatedUTC,
                                ModifiedUTC = e.ModifiedUTC
                            })
                        .ToArray();
            }
        }
        public ShoppingListItemModel GetItemById(int Eid, int Sid)
        {
            ShoppingListItem entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                        .ShoppingListItem
                        .SingleOrDefault(e => e.Id == Eid && e.ShoppingListId == Sid);
            }

            return
                new ShoppingListItemModel
                {
                    Id = entity.Id,
                    ShoppingListId = entity.ShoppingListId,
                    Contents = entity.Contents,
                    IsChecked = entity.IsChecked,
                    HasNotes = entity.HasNotes,
                    Priority = (ShoppingListItemModel.PriorityMessage)entity.Priority,
                    CreatedUTC = entity.CreatedUTC,
                    ModifiedUTC = entity.ModifiedUTC
                };
        }

        public void CreateItem(ShoppingListItemCreateModel vm, int Id, File file)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListItem
                    {
                        ShoppingListId = Id,
                        Contents = vm.Contents,
                        IsChecked = vm.IsChecked,
                        Priority = (ShoppingListItem.PriorityMessage)vm.Priority,
                        CreatedUTC = DateTimeOffset.UtcNow,
                        ModifiedUTC = vm.ModifiedUTC,
                        Files = new List<File> { file }
                    };

                ctx.ShoppingListItem.Add(entity);

                ctx.SaveChanges();
            }
        }

        public void DeleteItem(int Eid, int Sid)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .ShoppingListItem
                        .Single(e => e.ShoppingListId == Sid && e.Id == Eid);

                foreach (var file in entity.Files.ToArray())
                {
                    ctx.Files.Remove(file);

                }
                ctx.ShoppingListItem.Remove(entity);

                ctx.SaveChanges();
            }
        }

        public bool DeleteAllItems()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach (ShoppingListItem Sli in ctx.ShoppingListItem)
                {
                    ctx.ShoppingListItem.Remove(Sli);
                }

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateItem(ShoppingListItemEditModel vm)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .ShoppingListItem
                        .Single(e => e.Id == vm.Id && e.ShoppingListId == vm.ShoppingListId);

                entity.Id = vm.Id;
                entity.ShoppingListId = vm.ShoppingListId;
                entity.Contents = vm.Contents;
                entity.IsChecked = vm.IsChecked;
                entity.Priority = (ShoppingListItem.PriorityMessage)vm.Priority;
                entity.ModifiedUTC = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public void DeleteAllChecked(int[] IdsToBeDeleted)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach(var item in ctx.ShoppingListItem)
                {
                    foreach(var id in IdsToBeDeleted)
                    {
                        if (item.Id == id)
                            ctx.ShoppingListItem.Remove(item);
                    }
                }

                ctx.SaveChanges();
            }
        }
    }
}