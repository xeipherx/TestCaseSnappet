using Snappet_Test_Case.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Snappet_Test_Case.Drivers
{
    public static class RequestsClient
    {
        private static readonly HttpClient httpClient = new HttpClient();
        
        public static async Task<string> SendJsonPostRequestAsync(string json, string endpoint)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await httpClient.PostAsync(endpoint, content))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }

            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                throw;
            }
        }

        public static async Task<string> SendJsonPutRequestAsync(string json, string endpoint)
        {
            try
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await httpClient.PutAsync(endpoint, content))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                throw;
            }
        }

        public static async Task<string> SendGetRequestAsync(string endpoint)
        {
            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(endpoint))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                throw;
            }
        }

        public static async Task<string> GetAuthorizeToken(string uri, LoginRequest login)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri(uri);
                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();
                    allIputParams.Add(new KeyValuePair<string, string>("grant_type", login.grant_type));
                    allIputParams.Add(new KeyValuePair<string, string>("username", login.username));
                    allIputParams.Add(new KeyValuePair<string, string>("password", login.password));
                    allIputParams.Add(new KeyValuePair<string, string>("scope", login.scope));
                    allIputParams.Add(new KeyValuePair<string, string>("client_id", login.client_id));
                    allIputParams.Add(new KeyValuePair<string, string>("client_secret", login.client_secret));
                    // Convert Request Params to Key Value Pair.  
                    // URL Request parameters.  
                    HttpContent requestParams = new FormUrlEncodedContent(allIputParams);
                    using (HttpResponseMessage response = await client.PostAsync("Token", requestParams).ConfigureAwait(false))
                    {
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
                throw;
            }
        }

        public static void SetToken(LoginResponse authorization)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization.access_token);
        }
    }
}
