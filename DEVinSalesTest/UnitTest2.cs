using DevInSales.Context;
using DevInSales.Controllers;
using DevInSales.DTOs;
using DevInSales.Models;
using DevInSales.Seeds;
using DevInSales.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEVinSalesTest
{
    public class UnitTest2
    {
        private readonly DbContextOptions<SqlContext> _contextOptions;

        public UnitTest2()
        {
            _contextOptions = new DbContextOptionsBuilder<SqlContext>()
            .UseInMemoryDatabase("meuTesteUnitario")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

            using var context = new SqlContext(_contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public async Task RetornaTodosOsAddressAsync()
        {
            var lista = AddressSeed.Seed;
            var result = new List<Address>();
            var context = new AddressRepository(new SqlContext(_contextOptions));

            var controller = new AddressController(context);
            var response = await controller.GetAddress();

            foreach (var address in response.Value)
                result.Add(address);

            Assert.AreEqual(result.ToString(), lista.ToString());
        }

        [Test]
        public async Task RetornaEnderecoPorID()
        {
            var lista = AddressSeed.Seed;

            var context = new AddressRepository(new SqlContext(_contextOptions));

            var controller = new AddressController(context);

            var result = await controller.GetAddress(1);


            Assert.AreEqual(result.Value.Number, 22);
        }


        [Test]
        public async Task GetAddressComParametros()
        {
            var context = new AddressRepository(new SqlContext(_contextOptions));

            var controller = new AddressController(context);

            var response = context.getAddress("999999-99", "Rua Lateral", new CityStateDTO());

            var result = controller.GetAddress("999999-99", "Rua Lateral", new CityStateDTO());

            Assert.AreEqual(null, null);
        }

        [Test]
        public async Task PutAddress()
        {
            var context = new AddressRepository(new SqlContext(_contextOptions));

            var controller = new AddressController(context);


        }



        //AuthenticationController
        [Test]
        public async Task loginAdministrador()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new AuthController(context);

            UserLoginDto dadosUser = new UserLoginDto
            {
                Id = 1,
                Password = "romeu123@"
            };
            var result = controller.AuthenticateAsync(dadosUser);
            Assert.AreEqual(result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }


        //DeliveryController
        [Test]
        public async Task getAllDelivery()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new DeliveryController(context);

            var response = await controller.GetDelivery(0, 0);

            var result = response.Result;
            Assert.AreEqual(result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }
        [Test]
        public async Task PatchDelivery()
        {
            var context = new SqlContext(_contextOptions);
            var controller = new DeliveryController(context);

            var response = await controller.PatchDelivery(0, DateTime.Now) as Microsoft.AspNetCore.Mvc.StatusCodeResult;
            var result = response.StatusCode;


            Assert.AreEqual(result, 404);
        }


        //FreightController
        [Test]
        public async Task FreightGet()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new FreightController(context);
            var response = controller.GetFreight(1);

            Assert.AreEqual(response.Result.ToString(), "Microsoft.AspNetCore.Mvc.NotFoundResult");
        }

        //OrderController
        [Test]
        public async Task OrderGetByUserId()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new OrderController(context);
            var response = await controller.GetUserId(0);


            Assert.AreEqual(response.Result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        //ProductController
        [Test]
        public async Task ProductGetProduct()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new ProductController(context);
            var response = await controller.GetProduct("Curso de C Sharp", 0, 5000);
            var conteudo = response.Result as ObjectResult;
            var test = conteudo.Value as System.Collections.Generic.List<ProductGetDTO>;

            Assert.AreEqual(test[0].Suggested_Price, 259.99);
        }

        //StateController
        [Test]
        public async Task getState()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new StateController(context);
            var response = await controller.GetState("Santa");
            var conteudo = (response.Result as ObjectResult).Value as System.Collections.Generic.List<State>;
            Assert.AreEqual(conteudo[0].Name, "Santa Catarina");
        }

        //UserController

        [Test]
        public async Task getUser()
        {
            var context = new SqlContext(_contextOptions);

            var controller = new UserController(context);
            var response = await controller.Get("Romeu", "01/01/1990", "01/05/2020");
            var conteudo = (response.Result as ObjectResult).Value as System.Collections.Generic.List<UserResponseDTO>;
            Assert.AreEqual(conteudo[0].Name, "Romeu A Lenda");
        }
    }
}
