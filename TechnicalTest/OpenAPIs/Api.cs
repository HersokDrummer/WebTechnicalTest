using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using TechnicalTest.Models;
using Newtonsoft.Json;
using System.Text;

namespace TechnicalTest.OpenAPIs
{
    /// <summary>
    /// 
    /// </summary>
    public class Api
    {
        static HttpClient client = new HttpClient();

        /// <summary>
        /// 
        /// </summary>
        public Api()
        {
            //Initialize api client
            if (client.BaseAddress == null)
                client.BaseAddress = new Uri("https://localhost:44345/api/person");
                //client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings.Get("WS_URL"));            

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static async Task<Uri> CreatePersonAsync(Person person) {
            var payload = JsonConvert.SerializeObject(person);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://localhost:44345/api/person",content);

            response.EnsureSuccessStatusCode();

            return response.RequestMessage.RequestUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<List<Person>> GetPersonAsync(string path)
        {
            var jsonString = "";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode) {
                jsonString = response.Content.ReadAsStringAsync().Result;
            }
            List<Person> LstPersons = JsonConvert.DeserializeObject<List<Person>>(jsonString);

            return LstPersons;
        }


        public static async Task<Uri> EditPersonAsync(Person person)
        {
            var payload = JsonConvert.SerializeObject(person);
            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync("https://localhost:44345/api/person", content);

            response.EnsureSuccessStatusCode();

            return response.RequestMessage.RequestUri;
        }

        public static async Task<Uri> DeletePersonAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:44345/api/person/" + id);

            response.EnsureSuccessStatusCode();

            return response.RequestMessage.RequestUri;
        }

    }
}
