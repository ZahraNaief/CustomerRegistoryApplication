using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Windows;

namespace Assignmet5_ABC
{
    public partial class MainWindow : Window
    {
        CustomerManager customerManager = new CustomerManager();
        private int customerId = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateCustomerDisplay();
            this.ResizeMode = ResizeMode.NoResize;
        }
        private void UpdateCustomerDisplay()
        {

            lstCustomerInfo.Items.Clear();
            foreach (var customer in customerManager.Customers)
            {
                lstCustomerInfo.Items.Add(customer.FormatCustomerInfo());
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var contactWindow = new ContactWindow(customerManager);
                var result = contactWindow.ShowDialog();
                if (result == true) // Check if the dialog was accepted
                {
                    UpdateCustomerMainDisplay(); // Refresh the RichTextBox
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while opening the AddForm: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "EditCustomer";
            int selectedIndex = lstCustomerInfo.SelectedIndex;
            if (selectedIndex != -1)
            {
                string selectedText = lstCustomerInfo.SelectedItem.ToString();
                int customerId = int.Parse(selectedText.Substring(1, selectedText.IndexOf('-') - 1).Trim());
                Customer selectedCustomer = customerManager.GetCustomerById(customerId);
                if (selectedCustomer != null)
                {   
                    ContactWindow contactWindow = new ContactWindow(customerManager);
                    // Pass the selected customer's details to the ContactWindow for editing
                    contactWindow.ContactData = selectedCustomer.CustomerContactDetails;
                    // Populate the form fields with data from currentCustomer
                    contactWindow.txtFirstName.Text = selectedCustomer.FirstName;
                    contactWindow.txtLastName.Text = selectedCustomer.LastName;
                    contactWindow.txthomephone.Text = selectedCustomer.CustomerContactDetails.PhoneData.Home;
                    contactWindow.txtcellphone.Text = selectedCustomer.CustomerContactDetails.PhoneData.Office;
                    contactWindow.txtemailbusiness.Text = selectedCustomer.CustomerContactDetails.EmailData.Work;
                    contactWindow.txtemailprivate.Text = selectedCustomer.CustomerContactDetails.EmailData.Personal;
                    contactWindow.txtstreet.Text = selectedCustomer.CustomerContactDetails.Address.Street;
                    contactWindow.txtcity.Text = selectedCustomer.CustomerContactDetails.Address.City;
                    contactWindow.txtzipcode.Text = selectedCustomer.CustomerContactDetails.Address.ZipCode;
                    contactWindow.cmbBox_Countries.SelectedItem = selectedCustomer.CustomerContactDetails.Address.Country.ToString();
                    contactWindow.Title = "EditCustomer";
                    contactWindow.ShowDialog();
                    if (contactWindow.DialogResult.HasValue && contactWindow.DialogResult.Value)
                    {
                        // Update the existing customer with the edited details
                        //contactWindow.ContactData;                                        = selectedCustomer.CustomerContactDetails;
                        Enum.TryParse(contactWindow.cmbBox_Countries.SelectedItem.ToString(), out Countries countryEnum);
                        selectedCustomer.FirstName                                         = contactWindow.txtFirstName.Text             ;
                        selectedCustomer.LastName                                          = contactWindow.txtLastName.Text              ;
                        selectedCustomer.CustomerContactDetails.PhoneData.Home             = contactWindow.txthomephone.Text             ;
                        selectedCustomer.CustomerContactDetails.PhoneData.Office           = contactWindow.txtcellphone.Text             ;
                        selectedCustomer.CustomerContactDetails.EmailData.Work             = contactWindow.txtemailbusiness.Text         ;
                        selectedCustomer.CustomerContactDetails.EmailData.Personal         = contactWindow.txtemailprivate.Text          ;
                        selectedCustomer.CustomerContactDetails.Address.Street             = contactWindow.txtstreet.Text                ;
                        selectedCustomer.CustomerContactDetails.Address.City               = contactWindow.txtcity.Text                  ;
                        selectedCustomer.CustomerContactDetails.Address.ZipCode            = contactWindow.txtzipcode.Text               ;
                        selectedCustomer.CustomerContactDetails.Address.Country            = countryEnum;

                        // Update the customer in the customer manager
                        customerManager.ChangeCustomerAt(selectedCustomer, selectedIndex);
                        UpdateCustomerMainDisplay(); // Refresh the ListBox
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to edit.", " Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lstCustomerInfo.SelectedIndex;
            if (selectedIndex != -1)
            {
                // Show a confirmation message box
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo);
                bool success = customerManager.DeleteCustomerAt(selectedIndex);
                if (success)
                {
                    UpdateCustomerMainDisplay();
                }
                else
                {
                    MessageBox.Show("Failed to delete customer."," Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", " Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateCustomerMainDisplay()
        {
            // Assuming you have a ListView, DataGridView or some control to list customers:
            lstCustomerInfo.Items.Clear();
            foreach (var customer in customerManager.GetAllCustomers())
            {
                // Create a new ListViewItem or similar for your control
                string idCustomer = $"{customer.ID}".PadLeft(5);
                string firstNameText = customer.FirstName.PadLeft(20);
                string lastNameText = customer.LastName.PadLeft(20);
                string officePhoneText = customer.CustomerContactDetails.PhoneData.Office.PadLeft(40);
                string officeEmailText = customer.CustomerContactDetails.EmailData.Work.PadLeft(40);

                string customerInfo = $"{idCustomer} - {lastNameText}{firstNameText}{officePhoneText}{officeEmailText}";
                lstCustomerInfo.Items.Add(customerInfo);
            }

        }
        private void lstCustomerInfo_SelectedIndexChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            lstContactDetails.Items.Clear();
            if (lstCustomerInfo.SelectedItems.Count == 0)
            {
                lstContactDetails.Items.Clear();
                return;
            }
           
            // Assume that the ID is stored in the Tag property of the ListViewItem or is the first sub-item
           // int customerId = lstCustomerInfo.SelectedIndex;

            // Find the customer with the matching ID from your collection
            string selectedText = lstCustomerInfo.SelectedItem.ToString();
            int customerId = int.Parse(selectedText.Substring(1, selectedText.IndexOf('-') - 1).Trim());
            Customer selectedCustomer = customerManager.GetCustomerById(customerId);
            if (selectedCustomer != null)
            {
                // Format the customer's contact details for display
                lstContactDetails.Items.Add($"Name: {selectedCustomer.FirstName} {selectedCustomer.LastName}");
                lstContactDetails.Items.Add($"Address: {selectedCustomer.CustomerContactDetails.Address}");
                lstContactDetails.Items.Add("Emails:");
                lstContactDetails.Items.Add($"Private: {selectedCustomer.CustomerContactDetails.EmailData.Personal}");
                lstContactDetails.Items.Add($"Office: {selectedCustomer.CustomerContactDetails.EmailData.Work}");
                lstContactDetails.Items.Add("Phone Numbers:");
                lstContactDetails.Items.Add($"Private: {selectedCustomer.CustomerContactDetails.PhoneData.Home}");
                lstContactDetails.Items.Add($"Office: {selectedCustomer.CustomerContactDetails.PhoneData.Office}");
            }

        }
    }
}
