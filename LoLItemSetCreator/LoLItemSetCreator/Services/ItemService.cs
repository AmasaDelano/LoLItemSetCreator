using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoLItemSetCreator.DTOs;
using LoLItemSetCreator.Repositories;

namespace LoLItemSetCreator.Services
{
    public class ItemService
    {
        private readonly IRiotStaticRepository _riotStaticRepository;
        private const string BasePictureUrl = "http://ddragon.leagueoflegends.com/cdn/6.21.1/img/item/{0}.png";

        public ItemService(IRiotStaticRepository riotStaticRepository)
        {
            _riotStaticRepository = riotStaticRepository;
        }

        public List<Item> GetItems()
        {
            var items = _riotStaticRepository.GetItemList();

            if (items == null)
            {
                return new List<Item>();
            }

            items = items.Where(e => e.Name != null).OrderBy(e => e.Name).ToList();

            return items;
        }

        public Stream GetPictureStream(int itemId)
        {
            string imageUrl = string.Format(BasePictureUrl, itemId);

            byte[] imageData;

            using (var webClient = new System.Net.WebClient())
            {
                imageData = webClient.DownloadData(imageUrl);
            }

            return new MemoryStream(imageData);
        }
    }
}