using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HTTP_Parser
{
    public class HttpParser
    {
        List<Photo> photos;

        public async void GetPhotos(string url) //Вызов этого метода идет из MainActivity.cs
        {
            photos = await GetPhotoList(url);
        }

        public async Task<List<Photo>> GetPhotoList(string url)
        {
            var result = new List<Photo>();
            var httpClient = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(url) //передаем наш url из MainActivity, чтобы обратиться к API ВК
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //добавляем headers

            var resp = await httpClient.GetAsync(url).ConfigureAwait(false); //Отправляем запрос

            if (resp.IsSuccessStatusCode) //Если http-ответ успешен
            {
                var content = resp.Content;
                string task = await content.ReadAsStringAsync().ConfigureAwait(false); //получаем JSON

                var test = JsonConvert.DeserializeObject<Root>(task); //десериализация json-файла
                return test.Response.Items;
            }
            return new List<Photo>();
        }

        public class Root
        {
            public Response Response { get; set; }
        }

        public class Response
        {
            public int Count { get; set; }
            public List<Photo> Items { get; set; }
        }

        public class Photo // Класс для хранения данных из Json (три вида фотки - разные по размеру, долгота и широта)
        {
            public string Photo_75 { get; set; }
            public string Photo_130 { get; set; }
            public string Photo_604 { get; set; }
            public double Lat { get; set; }
            public double Long { get; set; }
        }
    }
}
