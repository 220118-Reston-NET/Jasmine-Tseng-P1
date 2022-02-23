using StoreModel;
using Xunit;

namespace StoreTest;

public class AddCustomerTest
{
    [Fact] 
    public void AddCustomer()
    {
        //Arrange
        Customer testName = new Customer();
        string testNameVar = "Pooh Bear";

        //Act
        testName.Name = testNameVar;

        //Assert
        Assert.NotNull(testName.Name);
        Assert.Equal(testNameVar, testName.Name);

    }
} 