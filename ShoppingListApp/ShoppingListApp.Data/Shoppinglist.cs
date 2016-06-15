using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Data
{
    public class ShoppingList
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}