using AddressBook.Data.DataModel;
using AddressBook.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Data.DataAccess
{
    internal class ContactAccess : ConnectionAccess, IContactAccess
    {
        public DataTable GetAllContacts()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.Connection = new SqlConnection(ConnectionString);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                sqlDataAdapter.SelectCommand.CommandText = Scripts.sqlGetAllContacts;

                // Fill the datatable from adapter
                sqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }
        
        public DataRow GetContactById(int id)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = new SqlConnection(ConnectionString);
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Scripts.sqlGetContactById;

                // Add the parameter to the parameter collection
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                dataRow = dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;

                return dataRow;
            }
        }

        public DataTable SearchContacts(string fullName, string email, string phone)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.Connection = new SqlConnection(ConnectionString);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                sqlDataAdapter.SelectCommand.CommandText = Scripts.sqlSearchContacts;

                // Add the input parameters to the parameter collection
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@FullName", fullName);

                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Email", email);

                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Phone", phone);

                // Fill the table from adapter
                sqlDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public bool AddContact(AddressBookModel contact)
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                // Set the command object properties
                sqlCommand.Connection = new SqlConnection(ConnectionString);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = Scripts.sqlInsertContact;

                // Add the input parameters to the parameter collection
                sqlCommand.Parameters.AddWithValue("@FullName", contact.FullName);
                sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                sqlCommand.Parameters.AddWithValue("@Phone", contact.Phone);
                sqlCommand.Parameters.AddWithValue("@Address", contact.Address);

                // Open the connection, execute the query and close the connection
                sqlCommand.Connection.Open();
                var rowsAffected = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool UpdateContact(AddressBookModel contact)
        {
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                // Set the command object properties
                sqlCommand.Connection = new SqlConnection(ConnectionString);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = Scripts.sqlUpdateContact;

                // Add the input parameters to the parameter collection
                sqlCommand.Parameters.AddWithValue("@FullName", contact.FullName);
                sqlCommand.Parameters.AddWithValue("@Email", contact.Email);
                sqlCommand.Parameters.AddWithValue("@Phone", contact.Phone);
                sqlCommand.Parameters.AddWithValue("@Address", contact.Address);
                sqlCommand.Parameters.AddWithValue("@Id", contact.Id);

                // Open the connection, execute the query and close the connection
                sqlCommand.Connection.Open();
                var rowsAffected = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        public bool DeleteContact(int id)
        {

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                // Set the command object properties
                sqlCommand.Connection = new SqlConnection(ConnectionString);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = Scripts.sqlDeleteContact;

                // Add the input parameter to the parameter collection
                sqlCommand.Parameters.AddWithValue("@Id", id);

                // Open the connection, execute the query and close the connection
                sqlCommand.Connection.Open();
                var rowsAffected = sqlCommand.ExecuteNonQuery();
                sqlCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
    }
}
