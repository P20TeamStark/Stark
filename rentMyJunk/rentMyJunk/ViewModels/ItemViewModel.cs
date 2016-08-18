using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using rentMyJunk.Models;

namespace rentMyJunk.ViewModels
{
    public class ItemViewModel : Item
    {
        [JsonIgnore]
        public HttpPostedFileBase ImgFile { get; set; }
    }
}