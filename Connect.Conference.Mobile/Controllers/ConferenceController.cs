using Connect.Conference.Mobile.Common;
using Connect.Conference.Mobile.Security;
using System.Threading.Tasks;

namespace Connect.Conference.Mobile.Controllers
{
    public class ConferenceController
    {
        public static async Task<Models.Conference> LoadConference(AppLink site, Jwt token)
        {
            var response = await ApiHelper.ExecuteAsync<Models.Conference>(site.Host, site.Scheme, "Conferences", "Get", site.ConferenceId, site.TabId, site.ModuleId, null, token);
            response.result.Host = site.Host;
            response.result.ModuleId = site.ModuleId;
            response.result.Scheme = site.Scheme;
            response.result.TabId = site.TabId;
            response.result.Username = site.Username;
            App.Data.db.Insert(response.result);
            return response.result;
        }
    }
}
