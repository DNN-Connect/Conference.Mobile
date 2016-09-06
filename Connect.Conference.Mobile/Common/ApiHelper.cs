using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Connect.Conference.Mobile.Security;

namespace Connect.Conference.Mobile.Common
{
    public class ApiHelper
    {

        public static async Task<T> ExecuteAsyncOld<T>(string url, Jwt jwt)
        {
            T returnValue = default(T);

            try
            {
                using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    if (jwt != null && !string.IsNullOrEmpty(jwt.AccessToken)) client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt.AccessToken.Replace("a", "e"));
                    var response = await Task.Run(() =>
                    {
                        var cancelSource = new CancellationTokenSource();
                        var requestTask = client.GetStringAsync(url);

                        if (Task.WaitAny(new Task[] { requestTask }, 150000) < 0)
                        {
                            cancelSource.Cancel();
                            throw new Exception("The network request timed out. Please check your network connection.");
                        }

                        return requestTask.GetAwaiter().GetResult();
                    });

                    returnValue = JsonConvert.DeserializeObject<T>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }


        public class Response<T>
        {
            public Response(T res)
            {
                this.result = res;
            }
            public System.Net.HttpStatusCode code { get; set; }
            public T result { get; set; }
        }

        public static async Task<Response<T>> ExecuteAsync<T>(string host, string scheme, string controller, string action, int? conferenceId, int tabId, int moduleId, string parameters, Jwt jwt)
        {
            var url = string.Format("{1}://{0}/DesktopModules/Connect/Conference/API/{2}/{3}", host, scheme, controller, action);
            if (conferenceId != null)
            {
                url += "/" + conferenceId.ToString();
            }
            url += string.Format("?tabId={0}&moduleId={1}", tabId, moduleId);
            if (parameters != null)
            {
                url += "&" + parameters;
            }
            return await ExecuteAsync<T>(url, jwt);
        }

        public static async Task<Response<T>> ExecuteAsync<T>(string url, Jwt jwt)
        {
            Response<T> returnValue = new Response<T>(default(T));

            using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                if (jwt != null && !string.IsNullOrEmpty(jwt.AccessToken)) client.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt.AccessToken);
                try
                {
                    var response = await client.GetAsync(url);
                    returnValue.code = response.StatusCode;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK) returnValue.result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception)
                {
                    returnValue.code = System.Net.HttpStatusCode.InternalServerError;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Posts the encoded data.
        /// </summary>
        /// <returns>The encoded data.</returns>
        /// <param name="url">URL.</param>
        /// <param name="content">Content.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static async Task<T> PostEncodedData<T>(string url, string token, string content)
        {
            string retVal = "";
            using (var handler = new HttpClientHandler())
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                if (!string.IsNullOrEmpty(token)) client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                retVal = await Task.Run(() =>
                {
                    var cancelSource = new CancellationTokenSource();
                    var requestTask = client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

                    if (Task.WaitAny(new Task[] { requestTask }, 150000) < 0)
                    {
                        cancelSource.Cancel();
                        throw new Exception("The network request timed out. Please check your network connection.");
                    }

                    var responseTask = requestTask.GetAwaiter().GetResult().Content.ReadAsStringAsync();

                    if (Task.WaitAny(new Task[] { responseTask }, 150000) < 0)
                    {
                        cancelSource.Cancel();
                        throw new Exception("The network request timed out. Please check your network connection.");
                    }

                    return responseTask.GetAwaiter().GetResult();
                });

            }
            return JsonConvert.DeserializeObject<T>(retVal);

        }
    }
}
