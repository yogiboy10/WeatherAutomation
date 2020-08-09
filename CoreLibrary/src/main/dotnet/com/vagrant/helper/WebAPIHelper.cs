using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace helper
{
    public class WebAPIHelper
    {
        public static JObject Invoke(String method, String uri, String body=null)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            int _TimeoutSec = 90;
            client.Timeout = new TimeSpan(0, 0, _TimeoutSec);
            string _ContentType = "application/json";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_ContentType));
            HttpResponseMessage response;
            var _Method = new HttpMethod(method);
            switch (_Method.ToString().ToUpper())
            {
                case "GET":
                    response = client.GetAsync(uri).Result;
                    break;
                case "POST":
                    {
                        HttpContent _Body = new StringContent(body);
                        _Body.Headers.ContentType = new MediaTypeHeaderValue(_ContentType);
                        response = client.PostAsync(uri, _Body).Result;
                    }
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            Console.WriteLine("Response", response);
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            return JObject.Parse(content);
            
        }


    }
}
