using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignmet5_ABC
{
   public class Contact
    {
        //Instance variables
        private string firstName;
        private string lastName;
        private Address address;
        private Email email;
        private Phone phone;

        #region CONSTRUCTOR
        public Contact()
        {
           address = new Address();
            email = new Email();
            phone = new Phone();
        }

        //Constructor with 3 parameters
        public Contact(string firstName, string lastName, Address addr, Email email, Phone phone)
        {
            this.firstName = firstName;
            this.lastName = lastName;

            // Check and assign address
            if (addr != null)
                address = addr;
            else
                address = new Address();

            // Check and assign email
            if (email != null)
                this.email = email;
            else
                email = new Email();

            // Check and assign phone
            if (phone != null)
                this.phone = phone;
            else
                this.phone = new Phone();
        }

        //Copy Constructor 
        //useful when we want to create an object of Participant and initialize it

        public Contact(Contact theOther)
        {
            this.firstName = theOther.firstName;
            this.lastName = theOther.lastName;
            this.address = new Address(theOther.address);
            this.email = new Email(theOther.email);
            this.phone = new Phone(theOther.phone);
        } 
        #endregion

        #region PROPERTIES
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public Email EmailData
        {
            get { return email; }
            set { email = value; }
        }

        public Phone PhoneData
        {
            get { return phone; }
            set { phone = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        #endregion
     
    }
}
