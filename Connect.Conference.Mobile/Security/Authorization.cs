using Connect.Conference.Mobile.Common;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Connect.Conference.Mobile.Security
{
    public class Authorization
    {
        private class LoginDTO
        {
            [JsonProperty("u")]
            public string Username { get; set; }
            [JsonProperty("p")]
            public string Password { get; set; }
        }
        public static async Task<Jwt> GetTokenAsync(string host, string username, string password)
        {
            var login = new LoginDTO { Username = username, Password = password };
            var loginJson = JsonConvert.SerializeObject(login).ToString();
            string url = string.Format("http://{0}/DesktopModules/JwtAuth/API/mobile/login", host);
            return await ApiHelper.PostEncodedData<Jwt>(url, "", JsonConvert.SerializeObject(login).ToString());
        }

        private class ExtendTokenDTO
        {
            [JsonProperty("rtoken")]
            public string RefreshToken { get; set; }
        }
        public static async Task<Jwt> RenewTokenAsync(string host, string oldToken, string refreshToken)
        {
            var rtoken = new ExtendTokenDTO { RefreshToken = refreshToken };
            var rTokenJson = JsonConvert.SerializeObject(rtoken).ToString();
            string url = string.Format("http://{0}/DesktopModules/JwtAuth/API/mobile/extendtoken", host);
            return await ApiHelper.PostEncodedData<Jwt>(url, oldToken, JsonConvert.SerializeObject(rtoken).ToString());
        }
    }
}
