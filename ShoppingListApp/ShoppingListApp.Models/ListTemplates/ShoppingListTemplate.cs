using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Models.ListTemplates
{
    public class ShoppingListTemplate
    {
        public int ShoppingListId { get; set; }

        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Can only input color codes")]
        public string Color { get; set; }

        public string Contents { get; set; }

        public string Name { get; set; }

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

        public enum ListTemplates
        {
            GroceryList = 0,
            ToDoList
        }

        public ListTemplates List { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}