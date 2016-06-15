using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Data
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingListItemId { get; set; }

        public string Body { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}