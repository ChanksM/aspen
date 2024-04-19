using Microsoft.AspNetCore.Mvc;
using SimpleApp.Controllers;
using SimpleApp.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SimpleApp.Controllers;
using SimpleApp.Models;
namespace SimpleApp.Tests
{
    public class HomeControllerTests
    {
        class FakeDataSource : IDataSource
        {
            public FakeDataSource(Product[] data) => Products = data;
            public IEnumerable<Product> Products { get; set; }
        }
        [Fact]
        public void IndexActionModelIsComplete()
        {
            // Arrange
            Product[] testData = new Product[] {
            new Product { Name = "Watermelon", Price = 10.6M , ProductType = "Food"},
            new Product {Name = "Basketball", Price = 103M , ProductType = "Sport" },
            new Product {Name = "Transistor", Price = 8.2M , ProductType = "Electronics"}
            };
            var dataSource = new ProductDataSource(); 
            var controller = new HomeController();
            controller.dataSource = dataSource;

            // Act
            var result = controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.ViewData.Model);

            // Assert
            Assert.Equal(testData.Length, model.Count()); 
            Assert.Equal(testData.Select(p => p.Name), model.Select(p => p.Name)); 
            Assert.Equal(testData.Select(p => p.Price), model.Select(p => p.Price));
            Assert.Equal(dataSource.Products, testData, 
                Comparer.Get<Product>((p1, p2) => p1?.Name == p2?.Name
                    && p1?.Price == p2?.Price && p1?.ProductType == p2?.ProductType));
        }


    }
}