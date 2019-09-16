using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;

namespace IOC.Tests
{
    [TestClass]
    public class NinjectKernelTest
    {
        [TestMethod]
        public void VerfifyNinjectBindings()
        {
            //Arrange
            var exceptions = new List<string>();
            var kernel = NinjectKernel.CreateKernel();
            var bindings = GetBindings(kernel);
            //Act
            foreach (var t in bindings)
            {
                try
                {
                    kernel.Get(t);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex.Message);
                }
            }
            //Assert
            exceptions.Should().BeEmpty();
        }
        private Type[] GetBindings(IKernel kernel)
        {
            return ((Multimap<Type, IBinding>)typeof(KernelBase)
                .GetField("bindings", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(kernel)).Select(x => x.Key).ToArray();
        }
    }
}
