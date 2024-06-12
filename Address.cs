using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Assignmet5_ABC
{
   public class Address
    {
        // instance variables
        //Input fields
        private string street;
        private string city;
        private string zipCode;
        private Countries country;


        #region CONSTRUCTORS
        //Constructors with chain call to each other
        //constructor with 2 parameters calls constructor with 3 parameters
        //And constructor with 3 parameters call constructor with 4 parameters

        public Address(string street, string zipCode, string city, Countries country)
        {
            this.street = street;
            this.city = city;
            this.zipCode = zipCode;
            this.country = country;
        }

        //Constructor - call the next constructor for reuse
        public Address(string street, string zipCode, string city) : this(street, zipCode, city, Countries.Sverige)
        {
        }

        //Default Constructor calling another constructor in this calss.
        public Address() : this(string.Empty, string.Empty, string.Empty)
        { }

        //Copy Constructor
        public Address(Address anotherAddress)
        {
            this.street = anotherAddress.street;
            this.city = anotherAddress.city;
            this.zipCode = anotherAddress.zipCode;
            this.country = anotherAddress.country;
        }

        #endregion

        #region PROPERTIES  
        public string Street
        {
            get
            {
                return this.street;
            }
            set
            {
                this.street = value;
            }
        }
        public string City
        {
            get
            {
                return this.city; 
            }
            set
            {
               city = value;
            }
        }
        public string ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                this.zipCode = value;
            }
        }
        public Countries Country
        {
            get
            {
                return this.country;
            }
            set
            {
                this.country = value;
            }
        }
        #endregion

        //Methods

        //this method returns country name without underscore char
        public string GetCountryString()
        {
            string strCountry = country.ToString();
            strCountry.Replace("_", "");
            return strCountry;
        }

        //validation for city nam,e which should not be empty
        public bool Validate()
        {
            bool cityOk = !string.IsNullOrEmpty(this.city);
            bool countryOk = this.country != default(Countries);
            return cityOk && countryOk;
        }

        // Formating Address into multiple lines
        public string GetAddressLabel()
        {
            string strOut = street + Environment.NewLine;
            strOut += zipCode + "" + city;
            return strOut;
        }

        public override string ToString()
        {
            string strOut = string.Format("{0,-10} {1, -8} {2, -10} {3}",
                street,  city, zipCode, GetCountryString());
            return strOut;
        }
    }
}


