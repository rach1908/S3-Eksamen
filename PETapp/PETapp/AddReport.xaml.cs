using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PETapp
{
    /// <summary>
    /// Interaction logic for AddReport.xaml
    /// </summary>
    public partial class AddReport : Window
    {
        private Person sub;
        private Person auth;
        private DBHandler db;
        public AddReport(Person subject, Person author)
        {
            InitializeComponent();
            sub = subject;
            auth = author;
            db = new DBHandler();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbxText.Text))
            {
                MessageBox.Show("You must enter text to submit a report", "Error");
            }
            else
            { 
                db.NewReport(new Report(tbxText.Text, sub, auth));
                this.Close();
            }
        }
    }
}
