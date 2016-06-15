using ShoppingListApp.Models;
using ShoppingListApp.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ShoppingListApp.Data;

namespace ShoppingListApp.Web.Controllers.WebApi
{
    [RoutePrefix("api/Items")]
    public class ShoppingListItemAppController : ApiController
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

        [Route]
        public IEnumerable<ShoppingListItemModel> Get(int id)
        {
            return _svc.Value.GetItems(id);
        }

        [Route]
        public void Post(ShoppingListItemCreateModel vm, int Id, File file)
        {
             _svc.Value.CreateItem(vm, Id, file);
        }

        [Route("{id}")]
        public ShoppingListItemModel Get(int Eid, int Sid)
        {
            return _svc.Value.GetItemById(Eid, Sid);
        }

        [Route("{id}")]
        public bool Put(int id, ShoppingListItemEditModel vm)
        {
            return _svc.Value.UpdateItem(vm);
        }

        [Route("{id}")]
        public void Delete(int Eid, int Sid, File file)
        {
            _svc.Value.DeleteItem(Eid, Sid);
        }

        private bool SetStarState(int Eid, int Sid, bool state)
        {
            var detail =
                _svc
                    .Value
                    .GetItemById(Eid, Sid);

            var item =
                new ShoppingListItemEditModel
                {
                    Id = detail.Id,
                    ShoppingListId = detail.ShoppingListId,
                    Contents = detail.Contents,
                    Priority = (ShoppingListItemEditModel.PriorityMessage)detail.Priority,
                    IsChecked = state
                };
            return _svc.Value.UpdateItem(item);
        }

        [Route("{id}/Checked")]
        [HttpPost]
        public bool ToggleStarOn(int Eid, int Sid) => SetStarState(Eid, Sid, true);

        [Route("{id}/Checked")]
        [HttpDelete]
        public bool ToggleStarOff(int Eid, int Sid) => SetStarState(Eid, Sid , false);
    }
}