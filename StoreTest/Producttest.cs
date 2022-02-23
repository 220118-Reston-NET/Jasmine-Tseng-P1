using System.Collections.Generic;
using Moq;
using StoreDL;
using BL;
using StoreModel;
using Xunit;

namespace StoreTest
{

    public class ProductTest
    {
        [Fact]

        public void ShouldProduct()
        {
            //Arrange
            Product _prod = new Product();
            int _itemID = 50;
            decimal _itemPrice = 5.70m;

            //Act
            _prod.ItemID = _itemID;
            _prod.Price = _itemPrice;

            //Assert
            Assert.Equal(_itemID, _prod.ItemID);
            Assert.Equal(_itemPrice, _prod.Price);
            
        }

    }
}