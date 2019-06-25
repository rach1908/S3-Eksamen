using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETapp
{
    public class Informant : Person
    {
        private string methodOfPayment;
        private string currency;

       

        public Informant(int id, string name, string address, string nationality, string serializedImage, string description, string methodOfPayment, string currency) 
            : base(id, name, address, nationality, serializedImage, description)
        {
            Currency = currency;
            MethodOfPayment = methodOfPayment;
        }
        public Informant(string name, string address, string nationality, string serializedImage, string description, string methodOfPayment, string currency) 
            : base(description, serializedImage, nationality, address, name)
        {
            Currency = currency;
            MethodOfPayment = methodOfPayment;
        }

        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        public string MethodOfPayment
        {
            get { return methodOfPayment; }
            set { methodOfPayment = value; }
        }
    }
}
