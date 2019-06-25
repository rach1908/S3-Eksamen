using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETapp
{
    public class Observant : Person
    {
        public Observant(int id, string name, string address, string nationality, string serializedImage, string description) 
            : base(id, name, address, nationality, serializedImage, description)
        {
        }
        public Observant(string name, string address, string nationality, string serializedImage, string description) 
            : base(description, serializedImage, nationality, address, name)
        {
        }
    }
}
