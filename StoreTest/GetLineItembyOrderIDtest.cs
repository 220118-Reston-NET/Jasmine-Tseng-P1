using System.Collections.Generic;
using StoreModel;
using Moq;
using StoreDL;
using BL;
using Xunit;

namespace Test
{
    public class PlaceOrderTest
    {
        [Theory]
        [InlineData(3)]
        [InlineData(7)]
        [InlineData(88)]
        public void Should_GetAllLineItemsByOrderID(int _orderID)
        {

            LineItem TestLineItem = new LineItem()
            {
                OrderID = _orderID
            };

            List<LineItem> expectedListOfLineItems = new List<LineItem>();
            expectedListOfLineItems.Add(TestLineItem);

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.GetAllLineItemsByOrderID(_orderID)).Returns(expectedListOfLineItems);
            IStoreBL _storeBL = new StoreBL(mockRepo.Object);

            // act
            List<LineItem> acualListOfLineItems = _storeBL.GetAllLineItemsByOrderID(_orderID);

            // assert
            Assert.Same(expectedListOfLineItems, acualListOfLineItems);
        }
    }
}