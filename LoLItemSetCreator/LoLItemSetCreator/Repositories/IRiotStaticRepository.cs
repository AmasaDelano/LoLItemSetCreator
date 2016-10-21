using System.Collections.Generic;
using LoLItemSetCreator.DTOs;

namespace LoLItemSetCreator.Repositories
{
    public interface IRiotStaticRepository
    {
        List<Item> GetItemList();
    }
}