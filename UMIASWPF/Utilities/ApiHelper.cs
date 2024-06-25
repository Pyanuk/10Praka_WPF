using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using BingingLibrary;
using System.ComponentModel;
using System.Globalization;

namespace UMIASWPF.Utilities
{
    public class ApiHelper : BindingHelper
    {
        private static string _url = "https://localhost:7245/api";

        public static T? Get<T>(string model, long id = 0)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = id == 0 ? $"{model}" : $"{model}/{id}";
                HttpResponseMessage response = client.GetAsync($"{_url}/{request}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var settings = new JsonSerializerSettings
                    {
                        Converters = { new DateOnlyConverter() }
                    };

                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result, settings);
                }
            }
            return default;
        }

        public class DateOnlyConverter : JsonConverter<DateOnly>
        {
            private readonly string _format = "dd.MM.yyyy";

            public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString(_format));
            }

            public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var value = reader.Value?.ToString();
                if (value != null)
                {
                    // Разделим строку по пробелу и возьмем только первую часть (дату)
                    var datePart = value.Split(' ')[0];
                    return DateOnly.ParseExact(datePart, _format, CultureInfo.InvariantCulture);
                }
                throw new JsonSerializationException("Invalid DateOnly format");
            }
        }

        public static bool Put(string json, string model, long id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync($"{_url}/{model}/{id}", body).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public static bool Post(string json, string model)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync($"{_url}/{model}", body).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public static bool Delete(string model, long id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.DeleteAsync($"{_url}/{model}/{id}").Result;
                return response.IsSuccessStatusCode;
            }
        }
    }
}