using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETapp
{
    public abstract class Person
    {
        private int id;
        private string name;
        private string address;
        private string nationality;
        private string serializedImage;
        private string description;
        //Contstructor til data fra databasen
        public Person(int id, string name, string address, string nationality, string serializedImage, string description)
        {
            Id = id;
            Name = name;
            Address = address;
            Nationality = nationality;
            SerializedImage = serializedImage;
            Description = description;
        }
        //Contructor til data IKKE fra databasen
        public Person(string description, string serializedImage, string nationality, string address, string name)
        {
            Description = description;
            SerializedImage = serializedImage;
            Nationality = nationality;
            Address = address;
            Name = name;
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string SerializedImage
        {
            get { return serializedImage; }
            set { serializedImage = value; }
        }

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
