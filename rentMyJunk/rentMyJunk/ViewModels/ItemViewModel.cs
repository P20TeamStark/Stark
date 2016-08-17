using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rentMyJunk.Models;

namespace rentMyJunk.ViewModels
{
    public class ItemViewModel : Item
    {
        public string Type { get; set; }

        public IEnumerable<SelectListItem> CategoryTypes
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Football", Value = "Football"},
                    new SelectListItem { Text = "Rugby", Value = "Rugby"},
                    new SelectListItem { Text = "Horse Racing", Value = "Horse Racing"}
                };
            }
        }
    }
}