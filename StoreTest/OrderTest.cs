using System.Collections.Generic;
using StoreModel;
using Moq;
using StoreDL;
using BL;
using Xunit;

namespace Test
{
    public class BLTest
    {
        [Fact]

        public void ReplenishInventoryTest()
        {
          
            //arrange
            int _storeID = 4;
            int _productID = 9;
            int _quantity = 10;
    


            Inventory _inventory = new Inventory()
            {
                StoreID = _storeID,
                ProductID = _productID,
                Quantity = _quantity
            };

            List<Inventory> expectedListOfInventory = new List<Inventory>();
            expectedListOfInventory.Add(_inventory);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllInventory()).Returns(expectedListOfInventory);

            IStoreBL _storeBL = new StoreBL(mockRepo.Object);

            // act
            List<Inventory> acualListOfInventory = _storeBL.GetAllInventory();

            // assert
            Assert.Same(expectedListOfInventory, acualListOfInventory);
            Assert.Equal(_storeID, acualListOfInventory[0].StoreID);
            Assert.Equal(_productID, acualListOfInventory[0].ProductID);
            Assert.Equal(_quantity, acualListOfInventory[0].Quantity);
   
        }
    }
}