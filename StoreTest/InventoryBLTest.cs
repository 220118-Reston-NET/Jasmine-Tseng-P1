// using System.Collections.Generic;
// using StoreModel;
// using Moq;
// using StoreDL;
// using BL;
// using Xunit;

// namespace Test
// {
//     public class InventoryBLTest
//     {
//         [Fact]

//         public void Should_Get_All_Inventory()
//         {
//             //arrange
//             Inventory _inventory = new Inventory()
//             {
//                 StoreID = 1,
//                 ProductID = 1,
//                 Quantity = 110
//             };

//             List<Inventory> expectedListOfInventory = new List<Inventory>();
//             expectedListOfInventory.Add(_inventory);

//             Mock<IRepository> mockRepo = new Mock<IRepository>();
//             mockRepo.Setup(repo => repo.GetAllInventory()).Returns(expectedListOfInventory);
//             IStoreBL _storeBL = new StoreBL(mockRepo.Object);

//             // act
//             List<Inventory> acualListOfInventory = _storeBL.GetAllInventory();

//             // assert
//             Assert.Same(expectedListOfInventory, acualListOfInventory);
//         }
//     }
// }