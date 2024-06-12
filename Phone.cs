using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Assignmet5_ABC
{
   public class Phone
    {
        private string HomePhone;
        private string OfficePhone;

        #region CONSTRUSTOR
        // Constructor with 2 parameters
        public Phone(string homephone, string officephone)
        {
            this.HomePhone = homephone;
            this.OfficePhone = officephone;
        }

        // Default Constructor calling another constructor in this class.
        public Phone() : this(string.Empty, string.Empty)
        { }

        // Copy Constructor
        public Phone(Phone theOther)
        {
            this.HomePhone = theOther.HomePhone;
            this.OfficePhone = theOther.OfficePhone;
        }

        #endregion


        #region PROPERTIES

        public string Home
        {
            get { return HomePhone; }
            set { HomePhone = value; }
        }

        public string Office
        {
            get { return OfficePhone; }
            set { OfficePhone = value; }
        }
        #endregion

        public override string ToString()
        {
            string strOut = string.Format("{0,-25} {1, -8} ",
                HomePhone,OfficePhone );
            return strOut;
        }
    }
}
