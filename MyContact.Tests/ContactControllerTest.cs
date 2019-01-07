using System;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyContact.Controllers;
using MyContact.Models;
using MyContact.DAL;
using MyContact.Entity;
using System.Security.Principal;
using System.Web.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
//using MyContact.Models;

namespace MyContact.Tests
{
    [TestClass]
    public class ContactControllerTest
    {
        private ContactController Get()
        {
            var identity = new GenericIdentity("TestUser");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "TestUser"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "TestUser"));

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole("Administrator")).Returns(true);
            principal.SetupGet(x => x.Identity).Returns(identity);
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            ContactController contactController = new ContactController(new ContactRepository())
            {

                ControllerContext = controllerContext.Object
            };

            return contactController;
        }

        Guid id = new Guid();
        [TestMethod]
        public void AddContact()
        {
            var result = Get().AddContact();
            Assert.IsNotNull(result);
        }

        
        [TestMethod]
        public void SaveContact()
        {
            var result = Get().SaveContact(new Contact { Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "9004357416", Status = true });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetContacts()
        {
           
            var result = Get().GetContacts() as ViewResult;
            var cnts = result.Model as List<Contact>;
            if (cnts != null && cnts.Count > 0)
            {
                id = cnts.First().Id;
            }
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void EditContact()
        {
            var result = Get().EditContact(id);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void UpdateContact()
        {
            var result = Get().UpdateContact(new Contact { Email = "deepak@gmail.com", FirstName = "deepak", LastName = "bhise", PhoneNumber = "88888888888", Status = true });
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteContact()
        {
            var result = Get().DeleteContact(id);
            Assert.IsNotNull(result);
        }

    }
}
