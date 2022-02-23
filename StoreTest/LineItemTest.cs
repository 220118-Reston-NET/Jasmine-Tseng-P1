using StoreModel;
using StoreDL;
using BL;

using Xunit;
using System.Collections.Generic;
using Moq;

namespace StoreTest;

public class ModelTest
{
    [Fact] // <-- data annotation in c# will tell the complier that this specific method is a unit test
    public void QuantityLimitTest1()
    {
        //Arrange
        LineItem testlineitem = new LineItem();
        int testnumber = 12;

        //Act
        testlineitem.Quantity = testnumber;

        //Assert
        Assert.NotNull(testlineitem.Quantity); //checks that the property is not null meaning we did set data in this property
        Assert.Equal(testnumber, testlineitem.Quantity); //checks if the property does indeed hold the same value as what we set it as
    }

        /// <summary>
        /// Checks validation for PP property with incorrect data
        /// Should throw an exception
        /// </summary>
        [Theory] //Changes the unit test to be parameterized and run multiple data and ensure they all passes
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-178)]
        [InlineData(-600000)]
        [InlineData(-2)]
        public void InvalidDataTest(int p_invalidquantity)
        {
            //Arrange
            LineItem testlineitem = new LineItem();


            //Act & Assert
            Assert.Throws<System.Exception>(
                () => testlineitem.Quantity = p_invalidquantity
            );
        
        }

    
}