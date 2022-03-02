using System.Collections.Generic;
using System;
using System.Globalization;
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

            List<Customer> expectedListOfCustomers = new List<Customer>();
            expectedListOfCustomers.Add(_testcustomer);

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(expectedListOfCustomers);
            IStoreBL _storeBL = new StoreBL(mockRepo.Object);

            // act
            List<Customer> actualListOfCustomers = _storeBL.GetAllCustomers();

            // assert
            Assert.Same(expectedListOfCustomers, actualListOfCustomers);

        }

        // [Theory]
        // [InlineData("HelloKitty123", "Meow456")]
        // [InlineData("Spiderman23", "Spider")]
        // [InlineData("Test123", "password23344")]
        // public void Should_login(string _username, string _password)
        // {
        //     //arrange
        //     User _testuser = new User()
        //     {
        //      Username = _username,
        //      Password = _password
        //     };

        //     Mock<IStoreBL> mockBL = new Mock<IStoreBL>();
        //     mockBL.Setup(bl => bl.Login(_testuser));
        //     ControllerBase _authencontroller = new AuthenticationController(mockBL.Object);

        //     actual = _storeBL.Login();

        // [Fact]
        // public void Should_PlaceOrder(Order _order)
        // {

        //     int _orderid = 45;
        //     int _customerid = 30;
        //     int _storeID = 15;
        //     decimal _totalprice = 35.00m;
        //     DateTime _datecreated = DateTime.UtcNow;
        //     List<LineItem> _cart = new List<LineItem>();


        //     Order _testorder = new Order()
        //     {
        //         OrderID = _orderid,
        //         CustomerID = _customerid,
        //         StoreID = _storeID,
        //         TotalPrice = _totalprice,
        //         DateCreated = _datecreated,
        //         Cart = _cart
        //     };

        //     Mock<IRepository> mockRepo = new Mock<IRepository>();
        //     mockRepo.Setup(repo => repo.PlaceOrder(_testorder));
        //     IStoreBL _storeBL = new StoreBL(mockRepo.Object);
        //     Order expectedOrder = _testorder;

        //     // act
        //     Order actualOrder = _storeBL.PlaceOrder();

        //     // assert
        //     Assert.Same(expectedListOfCustomer, actualOrder);


        // }

        [Fact]
        public void Should_GetAllStores()
        {
            int _storeID = 20;
            string _storefrontname = "Test Store";
            string _storefrontaddress = "344 Store Address";
            string _username = "testusernameofstore123";

            StoreFront _testStoreFront = new StoreFront()
            {
                StoreID = _storeID,
                StoreFrontName = _storefrontname,
                StoreFrontAddress = _storefrontaddress,
                Username = _username
            };

            List<StoreFront> expectedStoreFront = new List<StoreFront>();
            expectedStoreFront.Add(_testStoreFront);

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.GetAllStores()).Returns(expectedStoreFront);
            IStoreBL _storeBL = new StoreBL(mockRepo.Object);

            //assert

            List<StoreFront> actualListOfStoreFront = _storeBL.GetAllStores();

            // assert
            Assert.Same(expectedStoreFront, actualListOfStoreFront);
        }


        [Fact]
        public void Should_GetAllProducts()
        {
            int _itemID = 99;
            string _name = "Shampoo";
            decimal _price = 5.99m;
            int _quantity = 7;

            Product _testproduct = new Product()
            {
                ItemID = _itemID,
                Name = _name,
                Price = _price,
                Quantity = _quantity
            };

            List<Product> expectedListOfProducts = new List<Product>();
            expectedListOfProducts.Add(_testproduct);

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.GetAllProducts()).Returns(expectedListOfProducts);
            IStoreBL _storeBL = new StoreBL(mockRepo.Object);

            //assert

            List<Product> actualListOfProducts = _storeBL.GetAllProducts();

            // assert
            Assert.Same(expectedListOfProducts, actualListOfProducts);
        }


    }
}
