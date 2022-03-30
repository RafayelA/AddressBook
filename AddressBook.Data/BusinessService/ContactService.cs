using AddressBook.Data.DataAccess;
using AddressBook.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Data.BusinessService
{
    public class ContactService : IContactService
    {
        /// <summary>
        /// interface of ContactAccess
        /// </summary>
        private IContactAccess contactAccess;

        /// <summary>
        /// Initializes a new instance of the ContactService class
        /// </summary>
        public ContactService()
        {
            this.contactAccess = new ContactAccess();
        }
        
        public DataRow GetContactById(int id)
        {
            return this.contactAccess.GetContactById(id);
        }

        public DataTable GetAllContacts()
        {
            return this.contactAccess.GetAllContacts();
        }

        public DataTable SearchContacts(string fullName, string email, string phone)
        {
            return this.contactAccess.SearchContacts(fullName, email, phone);
        }

        public bool AddContact(AddressBookModel contact)
        {
            return this.contactAccess.AddContact(contact);
        }

        public bool UpdateContact(AddressBookModel contact)
        {
            return this.contactAccess.UpdateContact(contact);
        }

        public bool DeleteContact(int id)
        {
            return this.contactAccess.DeleteContact(id);
        }
    }
}
