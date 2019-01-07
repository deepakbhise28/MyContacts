using MyContact.Entity;
using System.Collections.Generic;
using System.Linq;


namespace MyContact.DAL
{
    public class ContactRepository : IRepository<Contact, long>
    {
        ContactContext context = null;
        public ContactRepository()
        {
            context = new ContactContext();
        }
        public bool Delete(string userId, long key)
        {
           var record = context.Contacts.FirstOrDefault(x => x.UserId == userId && x.Id == key);
            if(record == null)
            {
                return false;
            }
            else
            {
                context.Contacts.Remove(record);
                int savedRec = context.SaveChanges();
                return true;
            }
        }

        public IEnumerable<Contact> Get(string userId)
        {
            return context.Contacts.Where(x => x.UserId == userId);
        }

        public Contact Get(string userId, long key)
        {
          return context.Contacts.FirstOrDefault(x => x.UserId == userId && x.Id == key);
        }

        public bool Insert(string userId, Contact entity)
        {
            context.Contacts.Add(entity);
            context.SaveChanges();
            return true;
        }

        public bool Update(string userId, Contact entity)
        {
            var record = context.Contacts.FirstOrDefault(x => x.UserId == userId && x.Id == entity.Id);
            if (record == null)
            {
                return false;
            }
            else
            {
                record.FirstName = entity.FirstName;
                record.LastName = entity.LastName;
                record.PhoneNumber = entity.PhoneNumber;
                record.Status = entity.Status;
                record.Email = entity.Email;
                //context.SaveChanges()
                int savedRec = context.SaveChanges();
                return true;
            }          
        }
        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }
    }
}
