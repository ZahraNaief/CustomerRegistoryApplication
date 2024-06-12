using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignmet5_ABC
{
    public class CustomerManager
    {
        private List<Customer> customers;
        private int nextId = 1;

        #region CONSTRUCTOR
        public CustomerManager()
        {
            customers = new List<Customer>();
        }
        #endregion

        #region PROPERTIES
        public IReadOnlyList<Customer> Customers
        {
            get { return customers.AsReadOnly(); }
        }
        #endregion


        #region METHODS TO HANDLE LIST     

        private bool CheckIndex(int index)
        {
            return (index >= 0) && (index < customers.Count);
        }

        public void AddCustomer(Customer customer)
        {
            customer.ID = nextId++; // Assign the next unique ID and increment the ID counter
            customers.Add(customer);
        }
        public bool ChangeCustomerAt(Customer customersIn, int index)
        {
            if (CheckIndex(index) && customersIn != null)
            {
                customers[index] = customersIn;
                return true;
            }
            return false;
        }
        public bool DeleteCustomerAt(int index)
        {
            if (CheckIndex(index))
            {
                customers.RemoveAt(index);
                return true;
            }
            return false;
        }

        public List<Customer> GetAllCustomers()
        {
            return new List<Customer>(customers);
        }

        public Customer GetCustomerById(int id)
        {
            return customers.FirstOrDefault(c => c.ID == id);
        }
        public string[] GetCustomerInfoString()
        {
            List<string> customerInfo = new List<string>();
            foreach (Customer customer in customers)
            {
                customerInfo.Add(customer.FormatCustomerInfo());
            }
            return customerInfo.ToArray();
        }
        #endregion
    }
}
