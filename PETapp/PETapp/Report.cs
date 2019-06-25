using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETapp
{
    public class Report
    {
        private int id;
        private Person author;
        private Person subject;
        private string text;

        public Report(string text, Person subject, Person author)
        {
            Text = text;
            Subject = subject;
            Author = author;
        }

        public Report(string text, Person subject, Person author, int id)
        {
            Text = text;
            Subject = subject;
            Author = author;
            Id = id;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Person Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        public Person Author
        {
            get { return author; }
            set { author = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
