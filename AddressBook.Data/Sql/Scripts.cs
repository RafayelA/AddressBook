using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Data.Sql
{
    public static class Scripts
    {
        /// <summary>
        /// Sql to get a contact details by Id
        /// </summary>
        public static readonly string sqlGetContactById = "Select" +
            " Id, FullName, Email, Phone, Address" +
            " From Contact Where Id = @Id";

        /// <summary>
        /// Sql to get all contacts
        /// </summary>
        public static readonly string sqlGetAllContacts = "Select" +
            " Id, FullName, Email, Phone, Address" +
            " From Contact";

        /// <summary>
        /// sql to insert a contact details
        /// </summary>
        public static readonly string sqlInsertContact =
            "Insert Into" +
            " Contact(FullName, Email, Phone, Address)" +
            " Values(@FullName, @Email, @Phone, @Address)";

        /// <summary>
        /// sql to search for contacts
        /// </summary>
        public static readonly string sqlSearchContacts = "Select" +
            " Id, FullName, Email, Phone, Address" +
            " From Contact Where (FullName Like @FullName + '%') AND (Email Like @Email + '%') AND (Phone Like @Phone + '%')";

        /// <summary>
        /// sql to update customer details
        /// </summary>
        public static readonly string sqlUpdateContact = "Update Contact " +
            " Set [FullName] = @FullName, [Email] = @Email, [Phone] = @Phone, [Address] = @Address Where ([Id] = @Id)";

        /// <summary>
        /// sql to delete a customer record
        /// </summary>
        public static readonly string sqlDeleteContact = "Delete From Contact Where (Id = @Id)";
    }
}
