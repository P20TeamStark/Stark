using Newtonsoft.Json;

namespace RentAnything.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        public string address { get; set; } 
    }

}
