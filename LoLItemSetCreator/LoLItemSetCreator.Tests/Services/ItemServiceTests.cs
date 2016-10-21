using System.Collections.Generic;
using System.Linq;
using LoLItemSetCreator.DTOs;
using LoLItemSetCreator.Repositories;
using LoLItemSetCreator.Services;
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
        public void GetItems_Should_Return_An_Empty_List_When_GetItemList_Returns_Null()
        {
            _riotStaticRepositoryMock.Setup(e => e.GetItemList()).Returns((List<Item>) null);

            var items = _itemService.GetItems();

            Assert.IsEmpty(items);
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
}