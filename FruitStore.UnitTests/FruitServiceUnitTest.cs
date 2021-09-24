using FruitStore.Database;
using FruitStore.Models;
using FruitStore.Services.Classes;
using FruitStore.Services.Interfaces;
using FruitStore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FruitStore.UnitTests
{
    [TestClass]
    public class FruitServiceUnitTest
    {
        IFruitService _fruitService;

        public FruitServiceUnitTest()
        {

        }

        [TestMethod]
        public async Task AddingFruitItemReturnsSuccess()
        {
            //Arrange
            using var serviceProvider = BuildServiceProvider();
            using var dbContext = serviceProvider.GetService<FruitDbContext>();
            FruitItemDTO fruitItem = new FruitItemDTO
            {
                FruitId = 1,
                FruitName = "Apple",
                Quantity = 10,
                Price = 12
            };
            _fruitService = new FruitService(dbContext);
            //Act
            //Add a fruit item
            var addedItem = await _fruitService.AddFruitItem(fruitItem);
            //Assert
            //Fruit item added successfully
            Assert.IsNotNull(addedItem);
        }

        [TestMethod]
        public async Task AddingFruitItemAndThenRetreivingReturnsSuccess()
        {
            //Arrange
            using var serviceProvider = BuildServiceProvider();
            using var dbContext = serviceProvider.GetService<FruitDbContext>();
            FruitItemDTO FruitItemToAdd = new FruitItemDTO
            {
                FruitId = 1,
                FruitName = "Apple",
                Quantity = 10,
                Price = 12
            };
            _fruitService = new FruitService(dbContext);
            //Act
            //Add a fruit item and then retreive
            await _fruitService.AddFruitItem(FruitItemToAdd);
            var fruitItems = await _fruitService.GetFruitItems();
            //Assert
            //Fruit item retreived successfully
            Assert.IsNotNull(fruitItems);
        }
        [TestMethod]
        
        public async Task AddingFruitItemAndThenDeletingReturnsSuccess()
        {
            //Arrange
            using var serviceProvider = BuildServiceProvider();
            using var dbContext = serviceProvider.GetService<FruitDbContext>();
            FruitItemDTO fruitItemToAdd = new FruitItemDTO
            {
                FruitId = 1,
                FruitName = "Apple",
                Quantity = 10,
                Price = 12
            };
            _fruitService = new FruitService(dbContext);
            //Act
            //Add a fruit item and then delete
            var addedItem = await _fruitService.AddFruitItem(fruitItemToAdd);
            var deletedItem = await _fruitService.DeleteFruitItem(addedItem.FruitId);
            //Assert
            //Fruit item deleted successfully
            Assert.IsNotNull(deletedItem);
        }
        [TestMethod]
        
        public async Task DeletingFruitItemOnEmptyCollectionReturnsNull()
        {
            //Arrange
            using var serviceProvider = BuildServiceProvider();
            using var dbContext = serviceProvider.GetService<FruitDbContext>();
            FruitItemDTO fruitItemToDelete = new FruitItemDTO
            {
                FruitId = 1,
                FruitName = "Apple",
                Quantity = 10,
                Price = 12
            };
            _fruitService = new FruitService(dbContext);
            //Act
            //Delete without adding anything(Empty Collection)
            var deletedItem = await _fruitService.DeleteFruitItem(fruitItemToDelete.FruitId);
            //Assert
            //Nothing gets deleted
            Assert.IsNull(deletedItem);
        }
        
        [TestMethod]
        public async Task AddingFruitItemAndThenDeletingWithInvalidIdReturnsNull()
        {
            //Arrange
            using var serviceProvider = BuildServiceProvider();
            using var dbContext = serviceProvider.GetService<FruitDbContext>();
            FruitItemDTO fruitItemToAdd = new FruitItemDTO
            {
                FruitId = 1,
                FruitName = "Apple",
                Quantity = 10,
                Price = 12
            };
            FruitItemDTO fruitItemToDelete = new FruitItemDTO
            {
                FruitId = 2,
                FruitName = "Orange",
                Quantity = 11,
                Price = 13
            };
            _fruitService = new FruitService(dbContext);
            //Act
            //Add Apple and Delete Orange :)
            var addedFruitItem = await _fruitService.AddFruitItem(fruitItemToAdd);
            var deletedItem = await _fruitService.DeleteFruitItem(fruitItemToDelete.FruitId);
            //Assert
            //Deleting a non existent id returns null
            Assert.IsNull(deletedItem);
        }
        
        [TestMethod]
        public async Task RetreivingFruitItemsOnEmptyCollectionReturnsEmpty()
        {
            //Arrange
            using var serviceProvider = BuildServiceProvider();
            using var dbContext = serviceProvider.GetService<FruitDbContext>();
            _fruitService = new FruitService(dbContext);
            //Act
            //Retreive without adding anything
            var fruitItems = await _fruitService.GetFruitItems();
            //Assert
            //Returns an empty collection
            Assert.IsTrue(condition: fruitItems.IsNullOrEmpty());
        }

        private ServiceProvider BuildServiceProvider()
        {
            //Unit testing an in memory database is not easy. Hence using the following way to run each unit test independent of each other.
            var services = new ServiceCollection();
            services.ConfigureDatabase(Guid.NewGuid().ToString());

            return services.BuildServiceProvider();
        }
    }
}
