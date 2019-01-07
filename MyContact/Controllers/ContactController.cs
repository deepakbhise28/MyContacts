using MyContact.DAL;
using MyContact.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System;

namespace MyContact.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private const string Get_ViewName = "GetContacts";
        private const string Add_ViewName = "AddContact";
        private const string Edit_ViewName = "EditContact";
       
        private IRepository<Contact, Guid> _repository;
        public ContactController()
        {
            _repository = new ContactRepository();
        }

        public ContactController(IRepository<Contact, Guid> repository)
        {
            _repository = repository;
            
        }

        /// <summary>
        /// Get Contacts List
        /// </summary>
        /// <returns>list of Contacts</returns>
        public ActionResult GetContacts()
        {   
            var contacts = _repository.Get(User.Identity.GetUserId());            
            return View(contacts);
        }

        /// <summary>
        /// Show Add view
        /// </summary>
        /// <returns>Return view to add new contact</returns>       
        public ActionResult AddContact()
        {
            return View();
        }

        /// <summary>
        /// Save newly added contact
        /// </summary>
        /// <param name="contactViewModel">Contact details</param>
        /// <returns>return to contact list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveContact(Contact contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(Add_ViewName, contactViewModel);
            }
            contactViewModel.UserId = User.Identity.GetUserId();
            _repository.Insert(contactViewModel.UserId, contactViewModel);
            // var contacts = _repository.Get(contactViewModel.UserId);           
            // return View(Get_ViewName, contacts);
           return RedirectToAction(Get_ViewName);
        }

        /// <summary>
        /// GET: /Contact/Edit
        /// </summary>
        /// <param name="id">contact card id</param>
        /// <returns>show detail to edit</returns>
        public ActionResult EditContact(Guid id)
        {  
            var contact = _repository.Get(User.Identity.GetUserId(), id);
            return View(contact);
        }

        /// <summary>
        /// POST: Contact/Update
        /// </summary>
        /// <param name="contactViewModel">contact details</param>
        /// <returns>return to contact list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateContact(Contact contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(Edit_ViewName, contactViewModel);
            }        
            contactViewModel.UserId = User.Identity.GetUserId();
            _repository.Update(contactViewModel.UserId, contactViewModel);
            //var contacts = _repository.Get(contactViewModel.UserId);
            //return View(Get_ViewName, contacts); 
            return RedirectToAction(Get_ViewName);
        }

        /// <summary>
        /// POST: contact/Delete
        /// </summary>
        /// <param name="id">contact id</param>
        /// <returns>return to contact list</returns>       
     
        public ActionResult DeleteContact(Guid id)
        {  
            _repository.Delete(User.Identity.GetUserId(), id);
            // var contacts = _repository.Get(User.Identity.GetUserId());
            //return View(Get_ViewName, contacts);
            return RedirectToAction(Get_ViewName);
        }

        [NonAction]
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_repository != null)
                {
                    _repository.Dispose();
                    _repository = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}