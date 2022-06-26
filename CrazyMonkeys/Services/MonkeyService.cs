using CrazyMonkeys.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CrazyMonkeys.Services
{
    public class MonkeyService
    {
        private readonly string urlRequest = "https://www.montemagno.com/monkeys.json";

        public List<Monkey> monkeyList = new List<Monkey>();

        public HttpClient client;

        public MonkeyService()
        {
            client = new HttpClient();
        }

        public async Task<List<Monkey>> GetMonkeys()
        {
            if (monkeyList?.Count > 0)
                return monkeyList;

            var response = await client.GetAsync(urlRequest);

            if (response.IsSuccessStatusCode)
            {
                monkeyList = await response.Content.ReadFromJsonAsync<List<Monkey>>();
            }

            return monkeyList;
        }

        public async Task<List<Monkey>> GetMonkeysOffline()
        {
            if (monkeyList?.Count > 0)
                return monkeyList;

            using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var data = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize<List<Monkey>>(data);

            return monkeyList;
        }
    }
}
