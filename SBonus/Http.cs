using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;

namespace SBonus
{
    class Http
    {
        public static string rootUrl = "";

        public static string Get(string url)
        {
            WebRequest request = WebRequest.Create(rootUrl + url);
            request.Method = WebRequestMethods.Http.Get;
            
            return encode(ref request);
        }

        public static string Post(string url, JObject body)
        {
            WebRequest request = WebRequest.Create(rootUrl + url);
            request.Method = WebRequestMethods.Http.Post;

            using (StreamWriter stream = new StreamWriter(request.GetRequestStream()))
            {
                stream.Write(JsonConvert.SerializeObject(body));
            }

            return encode(ref request);
        }

        static string encode(ref WebRequest request)
        {
            request.ContentType = "application/json";
            try
            {
                return toString(request.GetResponse());
            }
            catch (WebException ex)
            {
                throw new Exception(toString(ex.Response));
            }
        }

        static string toString(WebResponse response)
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                Response resp = JsonConvert.DeserializeObject<Response>(reader.ReadToEnd());
                return JsonConvert.SerializeObject(resp.Message);
            }
        }

        public static JArray ToJsonArray(List<object> items)
        {
            JArray elements = new JArray();
            foreach (var item in items)
            {
                elements.Add(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(item)));
            }
            return elements;
        }
    }

    class Response
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public dynamic Message { get; set; }
    }
}
