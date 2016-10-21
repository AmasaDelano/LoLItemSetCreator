using System.Collections.Generic;
using System.Linq;
using Castle.Components.DictionaryAdapter;
using Moq;
using NUnit.Framework;

namespace LoLItemSetCreator.Tests.Services
{
    [TestFixture]
    public class ItemServiceTests
    {
        private ItemService _itemService;
        private Mock<IRiotStaticRepository> _riotStaticRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _riotStaticRepositoryMock = new Mock<IRiotStaticRepository>();

            _itemService = new ItemService(_riotStaticRepositoryMock.Object);
        }

        [Test]
        public void GetItems_Should_Call_GetItemList_On_RiotStaticRepository()
        {
            var items = _itemService.GetItems();

            _riotStaticRepositoryMock.Verify(e => e.GetItemList(), Times.Once);
        }

        [Test]
        public void GetItems_Should_Order_Items_By_Cost_By_Default()
        {
            var itemList = new List<Item>
            {
                new Item
                {
                    Cost = 2500
                },
                new Item
                {
                    Cost = 75
                },
                new Item
                {
                    Cost = 300
                },
                new Item
                {
                    Cost = 900
                }
            };
            _riotStaticRepositoryMock.Setup(e => e.GetItemList()).Returns(itemList);

            var items = _itemService.GetItems();

            var orderedItems = items.OrderBy(e => e.Cost);

            Assert.IsTrue(items.SequenceEqual(orderedItems));
        }
    }

    public interface IRiotStaticRepository
    {
        List<Item> GetItemList();
    }

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

            items = items.OrderBy(e => e.Cost).ToList();

            return items;
        }
    }

    public class Item
    {
        public int Cost { get; set; }
    }
}