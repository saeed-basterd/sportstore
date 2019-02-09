﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Repository;
using Xunit;
using Moq.Protected;
using SportStore.Models.ViewModels;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products)
                .Returns((new Product[]
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"},
                    new Product {ProductID = 4, Name = "P4"},
                    new Product {ProductID = 5, Name = "P5"},
                }).AsQueryable());

            ProductController controller =
                new ProductController(mock.Object);

            controller.PageSize = 3;

            // Act
            var result = controller
                    .List(null, 2)
                    .ViewData.Model
                as ProductsListViewModel;

            // Assert
            Product[] products = result.Products.ToArray();

            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock =
                new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
            }).AsQueryable());

            var controller = new ProductController(mock.Object) {PageSize = 3};

            ProductsListViewModel result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Product()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat3"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"},
            }).AsQueryable());
            //Arrange -- Create a controller and make the page size 3 items
            var controller = new ProductController(mock.Object) {PageSize = 3};

            // Action
            Product[] result =
                (controller
                    .List("Cat2", 1)
                    .ViewData
                    .Model as ProductsListViewModel)?
                .Products
                .ToArray();


            // Assert

            Assert.Equal(2, result.Length);

            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");

            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}