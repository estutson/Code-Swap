using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Models
{
    public class ShoppingListItemCreateModel
    {
        public int Id { get; set; }

        public int ShoppingListId { get; set; }

        public string Contents { get; set; }

        public enum PriorityMessage
        {
            [Display(Name = "It can wait")]
            ItCanWait = 0,
            [Display(Name = "Need it soon")]
            NeedItSoon,
            [Display(Name = "Grab it now")]
            GrabItNow
        }

        public PriorityMessage Priority { get; set; }

        public bool IsChecked { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}