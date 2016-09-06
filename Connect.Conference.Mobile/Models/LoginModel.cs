using Newtonsoft.Json;

namespace Connect.Conference.Mobile.Models
{
    public class LoginModel
    {
        [JsonProperty("u")]
        public string Username { get; set; }
        [JsonProperty("p")]
        public string Password { get; set; }
    }
}
