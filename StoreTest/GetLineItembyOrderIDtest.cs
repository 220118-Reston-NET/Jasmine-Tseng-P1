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
        [InlineData(3, 5, 9)]
        [InlineData(7, 1, 1)]
        [InlineData(88, 3, 4)]
        public void Should_GetAllLineItemsByOrderID(int _orderID, int _productID, int _quantity)
        {

            LineItem TestLineItem = new LineItem()
            {
                OrderID = _orderID,
                ProductID = _productID,
                Quantity = _quantity
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

        [Fact]
        public void should_GetAllCustomers()
        {
            int _id = 18;
            string _name = "TestName";
            string _address = "123 Test Address";
            string _phone = "123 - 788 - 1999";
            string _username = "TestUsername123";

            Customer _testcustomer = new Customer()
            {
                ID = _id,
                Name = _name,
                Address = _address,
                Phone = _phone,
                Username = _username
            };

            List<Customer> expectedListOfCustomer = new List<Customer>();
            expectedListOfCustomer.Add(_testcustomer);

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(expectedListOfCustomer);
            IStoreBL _storeBL = new StoreBL(mockRepo.Object);

            // act
            List<Customer> acualListOfLineItems = _storeBL.GetAllCustomers();

            // assert
            Assert.Same(expectedListOfCustomer, acualListOfLineItems);


        }
    }
}