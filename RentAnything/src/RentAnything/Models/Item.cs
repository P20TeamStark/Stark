﻿using Newtonsoft.Json;

namespace RentAnything.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string description { get; set; }
        public string imageUri { get; set; }
        public string ownerId { get; set; }       
    }
}
