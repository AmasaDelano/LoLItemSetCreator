using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using LoLItemSetCreator.DTOs;
using Newtonsoft.Json;

namespace LoLItemSetCreator.Repositories
{
    public interface IRiotStaticRepository
    {
        List<Item> GetItemList();
    }

    public class RiotStaticRepository : IRiotStaticRepository
    {
        private const string BaseUrl = "https://global.api.pvp.net/api/lol/static-data/na/v1.2/";
        private const string ApiKey = "RGAPI-2e6a53c3-e226-4f83-9008-265dd2fb85aa";

        public List<Item> GetItemList()
        {
            string jsonResponse = MakeRiotStaticGetRequest("item?itemListData=all");

            if (jsonResponse == null)
            {
                return new List<Item>();
            }

            // Define the structure of the anonymous type
            var jsonItemsList = new
            {
                data = new Dictionary<string, JsonItem>
                {
                    {
                        string.Empty,
                        new JsonItem
                        {
                            Id = 0,
                            Name = ""
                        }
                    }
                }
            };

            jsonItemsList = JsonConvert.DeserializeAnonymousType(jsonResponse, jsonItemsList);

            var items = jsonItemsList.data
                .ToList()
                .Select(d => new Item
                {
                    Id = d.Value.Id,
                    Name = d.Value.Name
                })
                .ToList();

            return items;
        }

        private class JsonItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Returns the contents of the response to the get request.
        /// If the request fails, returns null.
        /// </summary>
        private static string MakeRiotStaticGetRequest(string relativeUrl)
        {
            string targetAddress = BaseUrl + relativeUrl + "&api_key=" + ApiKey;

            var client = new WebClient();

            var stream = client.OpenRead(targetAddress);

            if (stream == null)
            {
                return null;
            }

            var reader = new StreamReader(stream);

            string response = reader.ReadToEnd();

            reader.Close();
            stream.Close();

            return response;
        }
    }
}