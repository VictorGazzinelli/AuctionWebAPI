using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AuctionWebAPI.Tests.Base
{
    public class AuctionWebAPICaller : IAuctionWebAPICaller
    {
        const string LOCALHOST_API_URL = @"https://localhost:44325/api/";

        public T CallEndpoint<T>(string endpointPath, object requestBody, HttpMethod httpMethod, string contentType, HttpClient httpClient)
        {
            HttpResponseMessage HttpResponseMessage;
            string ResponseBody = string.Empty;

            switch (httpMethod)
            {
                case HttpMethod m when m == HttpMethod.Get:
                    HttpResponseMessage = httpClient.GetAsync(LOCALHOST_API_URL + @endpointPath + ParseInputIntoQueryParams(requestBody)).GetAwaiter().GetResult();
                    break;
                case HttpMethod m when m == HttpMethod.Post:
                    HttpResponseMessage = httpClient.PostAsync(LOCALHOST_API_URL + @endpointPath, ParseInputAsRequestBody(requestBody, contentType)).GetAwaiter().GetResult();
                    break;
                case HttpMethod m when m == HttpMethod.Put:
                    HttpResponseMessage = httpClient.PutAsync(LOCALHOST_API_URL + @endpointPath, ParseInputAsRequestBody(requestBody, contentType)).GetAwaiter().GetResult();
                    break;
                case HttpMethod m when m == HttpMethod.Delete:
                    HttpResponseMessage = httpClient.SendAsync(GetDeleteHttpRequestMessage(endpointPath, requestBody, contentType)).GetAwaiter().GetResult();
                    break;
                default:
                    throw new Exception("HTTP Method not found");
            }

            if (!HttpResponseMessage.IsSuccessStatusCode)
                return default(T);

            ResponseBody = HttpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<T>(ResponseBody);
        }

        public async Task<T> CallEndpointAsync<T>(string endpointPath, object requestBody, HttpMethod httpMethod, string contentType, HttpClient httpClient)
        {
            HttpResponseMessage HttpResponseMessage;
            string ResponseBody = string.Empty;

            switch (httpMethod)
            {
                case HttpMethod m when m == HttpMethod.Get:
                    HttpResponseMessage = await httpClient.GetAsync(LOCALHOST_API_URL + @endpointPath + ParseInputIntoQueryParams(requestBody));
                    break;
                case HttpMethod m when m == HttpMethod.Post:
                    HttpResponseMessage = await httpClient.PostAsync(LOCALHOST_API_URL + @endpointPath, ParseInputAsRequestBody(requestBody, contentType));
                    break;
                case HttpMethod m when m == HttpMethod.Put:
                    HttpResponseMessage = await httpClient.PutAsync(LOCALHOST_API_URL + @endpointPath, ParseInputAsRequestBody(requestBody, contentType));
                    break;
                case HttpMethod m when m == HttpMethod.Delete:
                    HttpResponseMessage = await httpClient.SendAsync(GetDeleteHttpRequestMessage(endpointPath, requestBody, contentType));
                    break;
                default:
                    throw new Exception("HTTP Method not found");
            }

            if (!HttpResponseMessage.IsSuccessStatusCode)
                return default(T);

            ResponseBody = await HttpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(ResponseBody);
        }

        private static HttpRequestMessage GetDeleteHttpRequestMessage(string endpointPath, object input, string contentType)
        {
            return new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(LOCALHOST_API_URL + endpointPath),
                Content = ParseInputAsRequestBody(input, contentType)
            };
        }

        private static string ParseInputIntoQueryParams(object input)
        {
            IEnumerable<string> properties = from p in input.GetType().GetProperties()
                                             where p.GetValue(input, null) != null
                                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(input, null).ToString()).Replace("%2c", ".");
            string queryString = String.Join("&", properties.ToArray());
            return '?' + queryString;
        }

        private static HttpContent ParseInputAsRequestBody(object input, string contentType)
        {
            if (string.Equals("application/json", contentType))
                return new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
            else if (string.Equals("multipart/form-data", contentType))
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                foreach (PropertyInfo propertyInfo in input.GetType().GetProperties())
                    content.Add(new StringContent(propertyInfo.GetValue(input, null).ToString()), propertyInfo.Name);
                return content;
            }
            else
                throw new Exception($"contentType {contentType} not supported");

            //        Image newImage = Image.FromFile(@"Absolute Path of image");
            //        ImageConverter _imageConverter = new ImageConverter();
            //        byte[] paramFileStream = (byte[])_imageConverter.ConvertTo(newImage, typeof(byte[]));
            //        {new StreamContent(new MemoryStream(paramFileStream)),"imagekey","filename.jpg"}
        }
    }
}
