using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Assignmet5_ABC
{
    public class Customer
    {
        private static int nextId = 1;
        private string firstname = string.Empty;
        private string lastname = string.Empty;


        private Contact contact;

        #region CONSTRUCTOR
        public Customer(int id, string firstname, string lastname, Contact contact)
        {
            ID = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.contact = contact;
        }

        #endregion

        #region PROPERTIES
        public Contact CustomerContactDetails
        {
            get { return contact; }
            set { contact = value; }
        }
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public int ID { get; set; }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        #endregion
        public static int GenerateUniqueId()
        {
            return nextId++;
        }
        public string FormatCustomerInfo()
        {

            string formattedLastName = lastname.PadRight(20);
            string formattedFirstName = firstname.PadRight(30);
            string formattedOfficePhone = contact.PhoneData.Office.PadRight(40);
            string formattedEmail = contact.EmailData.Work.PadRight(40);

            string strOut = $"{formattedLastName}{formattedFirstName}{formattedOfficePhone}{formattedEmail}";
            return strOut;
        }
    }
}
