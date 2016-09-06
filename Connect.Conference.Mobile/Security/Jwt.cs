using Newtonsoft.Json;

namespace Connect.Conference.Mobile.Security
{
    public class Jwt
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("renewalToken")]
        public string RenewalToken { get; set; }
    }
}
