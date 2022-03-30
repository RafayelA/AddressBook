using AddressBook.Data.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Data.BusinessService
{
    public interface IContactService
    {
        /// <summary>
        /// Method to get contact by Id
        /// </summary>
        /// <returns>Data row</returns>
        DataRow GetContactById(int Id);

        /// <summary>
        /// Method to get all contacts
        /// </summary>
        /// <returns>Data table</returns>
        DataTable GetAllContacts();

        /// <summary>
        /// Service method to search records by multiple parameters
        /// </summary>
        /// <param name="FullName">FullName value</param>
        /// <param name="Email">Email value</param>
        /// <param name="Phone">Phone value</param>
        /// <returns>Data table</returns>
        DataTable SearchContacts(string fullName, string email, string phone);

        /// <summary>
        /// Service method to create new contact
        /// </summary>
        /// <param name="contact">address book model</param>
        /// <returns>true or false</returns>
        bool AddContact(AddressBookModel contact);

        /// <summary>
        /// Service method to update contact
        /// </summary>
        /// <param name="contact">contact</param>
        /// <returns>true / false</returns>
        bool UpdateContact(AddressBookModel contact);

        /// <summary>
        /// Method to delete a contact
        /// </summary>
        /// <param name="id">contact id</param>
        /// <returns>true / false</returns>
        bool DeleteContact(int id);
    }
}
