using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignmet5_ABC
{
    /// <summary>
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        private Contact contact;
        private Countries country;
        private CustomerManager customerManager;

        public ContactWindow()
        {
            InitializeComponent();
            InitializeGUI();
            contact = new Contact();
            country = new Countries();
            this.ResizeMode = ResizeMode.NoResize;
        }
        private void InitializeGUI()
        {
            InitializeComboBox();
            //Clear All Input Controls
            ClearInputControls();
        }
        private void ClearInputControls()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txthomephone.Text = string.Empty;
            txtcellphone.Text = string.Empty;
            txtemailbusiness.Text = string.Empty;
            txtemailprivate.Text = string.Empty;
            txtstreet.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtzipcode.Text = string.Empty;
        }
        #region PROPERTIES
        public Contact ContactData
        {
            get { return contact; }
            set { contact = value; }
        }
        #endregion
        #region CONSTRUCTORS
        // Constructor with CustomerManager parameter
        public ContactWindow(CustomerManager customerManager)
        {
            InitializeComponent();
            InitializeGUI();
            this.customerManager = customerManager; // Assign customerManager
            contact = new Contact();
            country = new Countries();
            this.ResizeMode = ResizeMode.NoResize;
        }
        #endregion

        #region EVENT HANDLERS
        //Click event handler for thee Ok button

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = CheckData();
            if (string.IsNullOrEmpty(errorMessage))
            {
                if (this.Title == "EditCustomer")
                {
                    ReadInput();
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    ReadInput(); // Call method to read input
                                 // Add the Customer object to the CustomerManager collection
                    int customerId = Customer.GenerateUniqueId();
                    Customer newCustomer = new Customer(customerId, contact.FirstName, contact.LastName, contact);
                    customerManager.AddCustomer(newCustomer);
                    // Set DialogResult to true to indicate successful addition
                    this.DialogResult = true;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Click event handler for the Cancel button
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Show a confirmation message box
            MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo);

            // If user selects "Yes", close the form without saving
            if (result == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
            // If user selects "No", do nothing and continue inputting
        }
        #endregion
        #region OTHER METHODS
        // Populate Combo Box with countries 
        private void InitializeComboBox()
        {
            foreach (string category in Enum.GetNames(typeof(Countries)))
            {
                cmbBox_Countries.Items.Add(category);
                // Set the default country as the selected item
                cmbBox_Countries.SelectedItem = Countries.DefaultCountry.ToString();
            }
        }

        // Read Input 
        private void ReadInput()
        {
            ReadNames();
            ReadPhones();
            ReadAddress();
            ReadEmails();
        }
        private void ReadEmails()
        {
            Email email = new Email(txtemailbusiness.Text, txtemailprivate.Text);
            contact.EmailData = email;
        }
        private void ReadPhones()
        {
            Phone phone = new Phone(txthomephone.Text, txtcellphone.Text);
            contact.PhoneData = phone;
            
        }
        private void ReadNames()
        {
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            
        }
        private void ReadAddress()
        {
            string selectedCountry = cmbBox_Countries.SelectedItem.ToString();
            Enum.TryParse(selectedCountry, out Countries countryEnum);
            Address address = new Address(txtstreet.Text, txtcity.Text, txtzipcode.Text, countryEnum);
            contact.Address = address;
        }
        #endregion
        #region VALIDATION
        private string CheckData()
        {
            // Check that no fields are empty and that emails and phone numbers have the correct format
            string errorMessage = string.Empty;
            errorMessage += IsValidName(txtFirstName.Text, txtLastName.Text) ? "" : "Name  is not entered.\n";  
            errorMessage += IsValidPhoneNumber(txthomephone.Text) ? "" : "Home phone number is not valid.\n";
            errorMessage += IsValidPhoneNumber(txtcellphone.Text) ? "" : "Cell phone number is not valid.\n";
            errorMessage += IsValidEmail(txtemailbusiness.Text) ? "" : "Business email is not valid.\n";
            errorMessage += IsValidEmail(txtemailprivate.Text) ? "" : "Private email is not valid.\n";
            errorMessage += cmbBox_Countries.SelectedIndex != -1 ? "" : "Country is not selected.\n";
            errorMessage += IsValidAddress(txtcity.Text, cmbBox_Countries.SelectedItem.ToString()) ? "" : "City or country is not valid.\n";
            

            return errorMessage;
        }
        public bool IsValidName(string firstName,string lastname)
        {
            bool namesOK = (!string.IsNullOrEmpty(txtFirstName.Text)) && (!string.IsNullOrEmpty(txtLastName.Text));
            return namesOK;
        }
        // Validation for city name and country, which should not be empty
        public bool IsValidAddress(string city, string selectedCountry)
        {
            bool cityOk = !string.IsNullOrEmpty(city);
            bool countryOk = selectedCountry != Countries.DefaultCountry.ToString();
            return cityOk && countryOk ;
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression for basic email validation
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string number)
        {
            // Simple pattern check - you may need a more complex regex for international formats
            return Regex.IsMatch(number, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
        }
        #endregion
    }
}
