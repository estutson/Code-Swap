using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Models
{
    public class ShoppingListModel
    {
        
        public int Id { get; set; }

        public int UserId { get; set; }

        public Guid OwnerId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}