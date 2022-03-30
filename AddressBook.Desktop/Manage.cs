using AddressBook.Data.BusinessService;
using AddressBook.Data.DataModel;
using AddressBook.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddressBook.Desktop
{
    public partial class Manage : Form
    {
        private IContactService contactService;

        private string errorMessage;

        private int contactId;
        public Manage()
        {
            InitializeComponent();
            contactService = new ContactService();
            Reset();
        }

        private void Reset()
        {
            DataTable data = contactService.GetAllContacts();
            this.LoadDataGridView(data);

            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            this.ActiveControl = txtFullName;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void LoadDataGridView(DataTable data)
        {           
            dgvAddressBook.DataSource = data;
            dgvAddressBook.DataMember = data.TableName;
        }

        private void AddErrorMessage(string error)
        {
            if (errorMessage == string.Empty)
            {
                errorMessage = Resources.Error_Message_Header + "\n\n";
            }

            errorMessage += error + "\n";
        }

        private void ShowErrorMessage(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                Resources.System_Error_Message_Title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private bool ValidateRegistration()
        {
            errorMessage = string.Empty;

            if (txtFullName.Text.Trim() == string.Empty)
            {
                AddErrorMessage(Resources.Contact_Adding_Fullname_Required_Text);
            }

            if (txtEmail.Text.Trim() == string.Empty)
            {
                AddErrorMessage(Resources.Contact_Adding_Email_Required_Text);
            }

            if (txtPhone.Text.Trim() == string.Empty)
            {
                AddErrorMessage(Resources.Contact_Adding_Phone_Required_Text);
            }

            return errorMessage != string.Empty ? false : true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the validation passes
                if (ValidateRegistration())
                {
                    // Assign the values to the model
                    AddressBookModel model = new AddressBookModel()
                    {
                        Id = 0,
                        FullName = txtFullName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),                        
                        Phone = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                    };

                    if (!txtEmail.Text.Trim().Contains("@"))
                    {
                        MessageBox.Show(
                        Resources.Email_Error_Message,
                        Resources.Email_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }                        

                    // Call the service method and assign the return status to variable
                    var success = contactService.AddContact(model);

                    // if status of success variable is true then display a information else display the error message
                    if (success)
                    {
                        // display the message box
                        MessageBox.Show(
                            Resources.Contact_Adding_Successful_Message,
                            Resources.Contact_Adding_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Reset the screen
                        DataTable data = contactService.GetAllContacts();
                        LoadDataGridView(data);
                        Reset();
                    }
                    else
                    {
                        // display the error messge
                        MessageBox.Show(
                            Resources.Contact_Adding_Error_Message,
                            Resources.Contact_Adding_Error_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Display the validation failed message
                    MessageBox.Show(
                        errorMessage,
                        Resources.Contact_Adding_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRegistration())
                {
                    AddressBookModel model = new AddressBookModel()
                    {
                        Id = contactId,
                        FullName = txtFullName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                    };

                    var success = contactService.UpdateContact(model);

                    if (success)
                    {
                        DataTable data = contactService.GetAllContacts();
                        LoadDataGridView(data);
                        Reset();

                        MessageBox.Show(
                            Resources.Update_Successful_Message,
                            Resources.Update_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                        errorMessage,
                        Resources.Contact_Updating_Error_Message_Title,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(
                            Resources.Delete_Check_Message, 
                            Resources.Delete_Check_Message_Title, 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var success = contactService.DeleteContact(contactId);

                    if (success)
                    {
                        DataTable data = contactService.GetAllContacts();
                        LoadDataGridView(data);
                        Reset();

                        MessageBox.Show(
                            Resources.Delete_Successful_Message,
                            Resources.Delete_Successful_Message_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = contactService.SearchContacts(txtFullName.Text, txtEmail.Text, txtPhone.Text);
                LoadDataGridView(data);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private void dgvAddressBook_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            try
            {
                if (dgv.SelectedRows.Count > 0)
                {
                    contactId = (int)dgv.SelectedRows[0].Cells[0].Value;

                    DataRow dataRow = contactService.GetContactById(contactId);

                    txtFullName.Text = dataRow["FullName"].ToString();

                    txtEmail.Text = dataRow["Email"].ToString();

                    txtPhone.Text = dataRow["Phone"].ToString();

                    txtAddress.Text = dataRow["Address"].ToString();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex);
            }
        }

        private void Manage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
