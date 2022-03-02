using System.Collections.Generic;
using StoreModel;
using Moq;
using StoreDL;
using BL;
using Xunit;

namespace StoreTest;

public class ModelTest
{
    [Fact]
    public void Should_LineItemValidData()
    {
        //Arrange
        LineItem testlineitem = new LineItem();
        int testnumber = 12;

        //Act
        testlineitem.Quantity = testnumber;

        //Assert
        Assert.NotNull(testlineitem.Quantity);
        Assert.Equal(testnumber, testlineitem.Quantity);
    }

    //=====================================================================================================================================

    [Theory]
    [InlineData(-1)]
    [InlineData(-10)]
    [InlineData(-178)]
    [InlineData(-600000)]
    [InlineData(-2)]
    public void Should_InvalidLineItemDataTest(int p_invalidquantity)
    {
        //Arrange
        LineItem testlineitem = new LineItem();


        //Act & Assert
        Assert.Throws<System.Exception>(
            () => testlineitem.Quantity = p_invalidquantity
        );
    }

    //=====================================================================================================================================

    [Fact]
    public void Should_ProductValidData()
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

    //=====================================================================================================================================

    [Fact]
    public void Should_ValidCustomerData()
    {
        //Arrange
        //Customer testName = new Customer();

        int _testID = 77;
        string _testNameVar = "Winnie Pooh Bear";
        string _testAdress = "6700 Owl Woods";
        string _testPhone = "123-123-1234";
        string _username = "WinnieThePooh1";


        //Act
        Customer testName = new Customer()
        {
            ID = _testID,
            Name = _testNameVar,
            Address = _testAdress,
            Phone = _testPhone,
            Username = _username
        };

        //Assert
        Assert.NotNull(testName.Name);
        Assert.Equal(_testID, testName.ID);
        Assert.Equal(_testNameVar, testName.Name);
        Assert.Equal(_testAdress, testName.Address);
        Assert.Equal(_testPhone, testName.Phone);
        Assert.Equal(_username, testName.Username);

    }
    //=====================================================================================================================================

    [Fact]
    public void Should_InventoryValidData()
    {

        //Arrange
        Inventory _testInventory = new Inventory();
        int _storeID = 11;
        int _productID = 82;
        int _quantity = 15;


        //Act
        _testInventory.StoreID = _storeID;
        _testInventory.ProductID = _productID;
        _testInventory.Quantity = _quantity;

        //Assert
        Assert.Equal(_quantity, _testInventory.Quantity);
        Assert.Equal(_storeID, _testInventory.StoreID);
        Assert.Equal(_quantity, _testInventory.Quantity);
    }

    //=====================================================================================================================================

    [Theory]
    [InlineData(123.00)]
    [InlineData(99111.000)]
    [InlineData(1127773.66)]
    public void Should_CartDetailsValidData(decimal p_price)
    {
        CartDetails _testCart = new CartDetails();

        _testCart.Price = p_price;

        Assert.Equal(p_price, _testCart.Price);
    }

}