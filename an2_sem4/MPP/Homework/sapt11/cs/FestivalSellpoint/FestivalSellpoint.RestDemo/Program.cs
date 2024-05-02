using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace FestivalSellpoint.RestDemo
{
    class MainClass
    {        
        static readonly HttpClient Client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

        private static string URL_Base = "http://localhost:8080/festival/spectacole";

        public static void Main(string[] args) => RunAsync().Wait();        

        static async Task<T> Get<T>(string uri)
        {            
            HttpResponseMessage response = await Client.GetAsync(URL_Base + uri);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            return default(T);
        }

        static async Task<T> Post<T>(string uri, T obj)
        {            
            HttpResponseMessage response = await Client.PostAsJsonAsync(URL_Base + uri, obj);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            return default(T);
        }

        static async Task<T> Put<T>(string uri, T obj)
        {
            HttpResponseMessage response = await Client.PutAsJsonAsync(URL_Base + uri, obj);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            return default(T);
        }

        static async Task<bool> Delete<T>(string uri)
        {
            HttpResponseMessage response = await Client.DeleteAsync(URL_Base + uri);
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        static async Task<Spectacol> GetById(int id) => await Get<Spectacol>($"/{id}");
        static async Task<Spectacol[]> GetAll() => await Get<Spectacol[]>("");
        static async Task<Spectacol> Create(Spectacol s) => await Post("", s);
        static async Task<Spectacol> Update(Spectacol s) => await Put($"/{s.Id}", s);
        static async Task<bool> Delete(Spectacol s) => await Delete<Spectacol>($"/{s.Id}");

        static void WriteLine(object msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg?.ToString() ?? "null");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static async Task RunAsync()
        {
            Client.BaseAddress = new Uri(URL_Base);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            WriteLine("\nGET ALL", ConsoleColor.Cyan);
            (await GetAll()).ToList().ForEach(_ => WriteLine(_, ConsoleColor.Yellow));


            WriteLine("\nGET BY ID", ConsoleColor.Cyan);
            var s1 = await GetById(2);
            WriteLine(s1, ConsoleColor.Yellow);

            WriteLine("\nCREATE", ConsoleColor.Cyan);
            var s = new Spectacol("Magician17", DateTime.Now, "Cluj", 5, 6);
            s = await Create(s);
            WriteLine(s, ConsoleColor.Yellow);

            WriteLine("\nUPDATE", ConsoleColor.Cyan);
            s.NrLocuriDisponibile = 20;
            s = await Update(s);
            WriteLine(s, ConsoleColor.Yellow);


            WriteLine("\nDELETE", ConsoleColor.Cyan);
            await Delete(s);

            Console.ReadLine();
        }       
    }    
}
