using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.AutoMapperProfiles;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.ViewModelMapping.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            AutomapperConfiguration.Cofigure(AppObjectFactory.GetContainer());

        }
        [TestMethod]
        public void RolePremissionMappingTest()
        {
            var rolepremission = new AllowedRole()
            {
                AccessPremission = AccessPremission.Manage,
                RoleName = "test"
            };
            var mapped = Mapper.Map<RolePremissionViewModel>(rolepremission);
            Assert.AreEqual(mapped.AccessPremission, AccessPremission.Manage);
        }


        [TestMethod]
        public void Shold()
        {

            var cat = new Category() { Title = "hello" };
            var vm = Mapper.Map<EntityCatLiquid>(cat);
            Assert.AreEqual(vm.Title, cat.Title);
        }

        [TestMethod]
        public void ByteArrayTest()
        {


            byte[] bytes = new byte[2] { 1, 2 };
            var bytesstring = Mapper.Map<string>(bytes);
            var mappedBytes = Mapper.Map<byte[]>(bytesstring);
            Console.Write(bytesstring);
         Assert.AreEqual(mappedBytes[0],1);
         Assert.AreEqual(mappedBytes[1],2);
        }

    }
}
