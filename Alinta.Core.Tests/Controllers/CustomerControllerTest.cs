using System;
using System.Collections.Generic;
using Alinta.Api.Controllers;
using Alinta.Core.Domain;
using Alinta.Core.Entities;
using Alinta.Core.Repository;
using Alinta.Core.Service;
using Alinta.Core.Service.Abstract;
using Alinta.Core.UnitofWork;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Alinta.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void Update_GivenCustomer_Returns_Accepted()
        {
            var customer = new CustomerViewModel() { Id = 1, FirstName = "Mohsin", LastName = "Jamadar", DateOfBirth = new DateTime(2010, 02, 10) };



            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.Update(customer)).Returns(true);

            var result = new CustomerController(mockService.Object).Update(1, customer);



            Assert.Equal(202, ((ObjectResult)result).StatusCode);

        }
        [Fact]
        public void Update_GivenWrongId_ReturnsBadRequest()
        {
            var customer = new CustomerViewModel() { Id = 1, FirstName = "Mohsin", LastName = "Jamadar", DateOfBirth = new DateTime(2010, 02, 10) };



            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.Update(customer)).Returns(true);

            var result = new CustomerController(mockService.Object).Update(2, customer);



            Assert.Equal(400, ((StatusCodeResult)result).StatusCode);

        }
       

        [Fact]
        public void GetById_GivenWrongId_ReturnsNotFoundStatus()
        {

            var customer = new CustomerViewModel() { Id = 1, FirstName = "Mohsin", LastName = "Jamadar", DateOfBirth = new DateTime(2010, 02, 10) };
            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.GetOne(1)).Returns(customer);

            var result = new CustomerController(mockService.Object).GetById(3);

            Assert.Equal(404, ((StatusCodeResult)result).StatusCode);

        }

        [Fact]
        public void GetOneByName_GivenMatchingName_ReturnsStatusOK()
        {

            IEnumerable<CustomerViewModel> customers = new List<CustomerViewModel>() {
             new CustomerViewModel { Id = 1, FirstName = "John", LastName = "Blake", DateOfBirth = new DateTime(2010, 02, 10) },
             new CustomerViewModel{ Id = 2, FirstName = "John", LastName = "Smith", DateOfBirth = new DateTime(2000, 08, 03) }
             };
            var mockService = new Mock<ICustomerService<CustomerViewModel,Customer>>();


            mockService.Setup(s => s.GetByName("john")).Returns(customers);

            var result = new CustomerController(mockService.Object).GetByName("john");

            Assert.Equal(200, ((ObjectResult)result).StatusCode);

        }

      

        [Fact]
        public void GetById_GivenExactId_ReturnsStatusOK()
        {

            var customer = new CustomerViewModel() {Id=1, FirstName = "Mohsin", LastName = "Jamadar", DateOfBirth = new DateTime(2010, 02, 10) };
            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.GetOne(1)).Returns(customer);

            var result = new CustomerController(mockService.Object).GetById(1);
            var customerVM = (CustomerViewModel)((ObjectResult)result).Value;
            Assert.Equal(200, ((ObjectResult)result).StatusCode);

        }

       


        [Fact]
        public void Create_GivenCustomer_ReturnsCreated()
        {
            var customer = new CustomerViewModel() { Id = 1, FirstName = "Mohsin", LastName = "Jamadar", DateOfBirth = new DateTime(2010, 02, 10) };

            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.Add(customer)).Returns(1);

            var result = new CustomerController(mockService.Object).Create(customer);

            var createdId = ((ObjectResult)result).Value;

            Assert.Equal(1, createdId);

        }

        [Fact]
        public void GetAll_Returns_ReturnsStatusOK()
        {
            IEnumerable<CustomerViewModel> customers = new List<CustomerViewModel>() {
             new CustomerViewModel { Id = 1, FirstName = "Mohsin", LastName = "Jamadar", DateOfBirth = new DateTime(2010, 02, 10) },
             new CustomerViewModel{ Id = 2, FirstName = "John", LastName = "Smith", DateOfBirth = new DateTime(2000, 08, 03) }
             };


            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.GetAll()).Returns(customers);

            var result = new CustomerController(mockService.Object).GetAll();

            Assert.Equal(200, ((ObjectResult)result).StatusCode);

        }


        

        [Fact]
        public void Delete_Returns_Accepted()
        {

            var mockService = new Mock<ICustomerService<CustomerViewModel, Customer>>();


            mockService.Setup(s => s.Remove(1)).Returns(true);

            var result = new CustomerController(mockService.Object).Delete(1);



            Assert.Equal(204, ((StatusCodeResult)result).StatusCode);

        }



    }
}
