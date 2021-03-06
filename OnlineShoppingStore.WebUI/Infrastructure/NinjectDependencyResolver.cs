﻿using Moq;
using Ninject;
using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Concrete;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                    .AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
            kernel.Bind<IAuthentication>().To<FormsAuthenticationProvider>();


            Mock<IPagesRepository> mock = new Mock<IPagesRepository>();
            mock.Setup(m => m.Pages).Returns(new List<Page> {
                new Page { Header = "About", Content = "lorem ipsum" },
                new Page { Header = "Contact", Content = "Contact me" }
                          });

            kernel.Bind<IPagesRepository>().ToConstant(mock.Object);
        }
    
    }
}