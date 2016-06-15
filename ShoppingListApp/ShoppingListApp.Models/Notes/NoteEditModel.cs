using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Models
{
    public class NoteEditModel
    {
        public int Id { get; set; }

        public int ShoppingListItemId { get; set; }

        public string Body { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}