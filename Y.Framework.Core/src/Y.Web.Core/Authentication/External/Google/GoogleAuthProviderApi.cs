using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.AspNetCore.Authentication.Google;
using Newtonsoft.Json.Linq;
using Y.Authentication.External;

namespace Y.Authentication.External.Google
{
    public class GoogleAuthProviderApi : ExternalAuthProviderApiBase
    {
        public const string Name = "Google";

        public override async Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core OAuth middleware");
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB

                var request = new HttpRequestMessage(HttpMethod.Get, GoogleDefaults.UserInformationEndpoint);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessCode);

                var response = await client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

                var results = new ExternalAuthUserInfo
                {
                    Name = (string)payload.SelectToken("family_name"),
                    EmailAddress = (string)payload.SelectToken("email"),
                    Surname = $"{(string)payload.SelectToken("given_name")}",
                    Provider = Name,
                    ProviderKey = (string)payload.SelectToken("id")
                };

                return results;

                //throw new UserFriendlyException("This Feature is not available in this sample.");
            }
        }
    }
}