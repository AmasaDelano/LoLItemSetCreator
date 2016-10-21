using System.Collections.Generic;
using System.Linq;
using LoLItemSetCreator.DTOs;
using LoLItemSetCreator.Repositories;

namespace LoLItemSetCreator.Services
{
    public class ItemService
    {
        private readonly IRiotStaticRepository _riotStaticRepository;

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

            items = items.OrderBy(e => e.Cost).ToList();

            return items;
        }
    }
}