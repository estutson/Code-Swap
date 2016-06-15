using System.Collections.Generic;
using System.Linq;
using ShoppingListApp.Data;
using ShoppingListApp.Models;
using System;

namespace ShoppingListApp.Services
{
    public class NotesService
    {
        public NotesService()
        {
        }

        public IEnumerable<NoteModel> GetNotes(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .Note
                    .Where(e => e.ShoppingListItemId == id)
                    .Select(
                        e =>
                            new NoteModel
                            {
                                Id = e.Id,
                                ShoppingListItemId = e.ShoppingListItemId,
                                Body = e.Body,
                                CreatedUTC = e.CreatedUTC,
                                ModifiedUTC = e.ModifiedUTC
                            })
                        .ToArray();
            }
        }
        public NoteModel GetNoteById(int Id, int ShoppingListItemId)
        {
            Note entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                        .Note
                        .SingleOrDefault(e => e.Id == Id && e.ShoppingListItemId == ShoppingListItemId);
            }

            return
                new NoteModel
                {
                    Id = entity.Id,
                    ShoppingListItemId = entity.ShoppingListItemId,
                    Body = entity.Body,
                    CreatedUTC = entity.CreatedUTC,
                    ModifiedUTC = entity.ModifiedUTC
                };
        }

        public bool CreateNote(NoteCreateModel vm, int ShoppingListItemId)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new Note
                    {
                        ShoppingListItemId = ShoppingListItemId,
                        Body = vm.Body,
                        CreatedUTC = DateTimeOffset.UtcNow,
                        ModifiedUTC = vm.ModifiedUTC
                    };

                ctx.Note.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteNote(int Id, int ShoppingListItemId)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .Note
                        .Single(e => e.ShoppingListItemId == ShoppingListItemId && e.Id == Id);

                ctx.Note.Remove(entity);

                var Item =
                    ctx
                        .ShoppingListItem
                        .SingleOrDefault(e => e.Id == ShoppingListItemId);
                Item.HasNotes = false;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAllNotes()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach (Note Notes in ctx.Note)
                    ctx.Note.Remove(Notes);

                foreach (ShoppingListItem SLI in ctx.ShoppingListItem)
                    SLI.HasNotes = false;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UpdateNote(NoteEditModel vm)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .Note
                        .Single(e => e.Id == vm.Id && e.ShoppingListItemId == vm.ShoppingListItemId);

                entity.Id = vm.Id;
                entity.ShoppingListItemId = vm.ShoppingListItemId;
                entity.Body = vm.Body;
                entity.ModifiedUTC = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}