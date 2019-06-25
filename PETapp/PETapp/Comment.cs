using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PETapp
{
    public class Comment
    {
        private int id;
        private Report parentReport;
        private Person author;
        private string text;

        public Comment(string text, Person author, Report parentReport)
        {
            Text = text;
            Author = author;
            ParentReport = parentReport;
        }

        public Comment(string text, Person author, Report parentReport, int id)
        {
            Text = text;
            Author = author;
            ParentReport = parentReport;
            Id = id;
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Person Author
        {
            get { return author; }
            set { author = value; }
        }

        public Report ParentReport
        {
            get { return parentReport; }
            set { parentReport = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
