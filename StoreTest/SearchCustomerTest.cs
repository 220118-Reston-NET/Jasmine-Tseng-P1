// using StoreModel;
// using StoreDL;
// using BL;
// using Xunit;
// using System.Collections.Generic;
// using Moq;

// namespace StoreTest;

// public class SearchCustomerTest
// {
//     [Fact]
//     public object Should_SearchCustomers()
//     {
//         //Arrange
//         string testNameVar = "Pooh Bear";
//         int testID = 8;

//         Customer TestCust = new Customer()
//         {
//             Name = testNameVar,
//             ID = testID
//         };

//         List<Customer> testList = new List<Customer>();

//         testList.Add(TestCust);
//         return testList;

//         Mock<IRepository> mockRepo = new Mock<IRepository>();
//         mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(testList);

//         IStoreBL _storeBL = new StoreBL(mockRepo.Object);
//         //Act
//         List<Customer> realList = _storeBL.GetAllCustomers();

//         //Assert
//         Assert.Same(testList, realList);
//         Assert.Equal(testNameVar, realList[0].Name);
//         Assert.Equal(testID, realList[0].ID);


//     }
// }