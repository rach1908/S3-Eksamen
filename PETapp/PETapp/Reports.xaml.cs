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
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        private Person sub;
        private Person auth;
        private DBHandler db;
        public Reports(Person subject, Person author)
        {
            InitializeComponent();
            sub = subject;
            auth = author;
            db = new DBHandler();
            lblIntro.Content = "Reports about " + sub.Name;
            try
            {
                dtgSubject.ItemsSource = db.GetReportsBySubject(sub.Id);
            }
            catch (Exception)
            {
                //Empty catch, because the query returned no results
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            AddReport dialog = new AddReport(sub, auth);
            dialog.ShowDialog();
            try
            {
                dtgSubject.ItemsSource = db.GetReportsBySubject(sub.Id);
            }
            catch (Exception)
            {
                //try catch in case a new report was not created, and the query still returns 0;
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dtgSubject.SelectedItem != null)
            {
                EditReport dialog = new EditReport(dtgSubject.SelectedItem as Report);
                if (dialog.ShowDialog() == true)
                {
                    db.UpdateReport(dialog.report);
                    MessageBox.Show("Report has been succesfully updated");
                    dtgSubject.ItemsSource = db.GetReportsBySubject(sub.Id);
                }
            }
            else
            {
                MessageBox.Show("You must select a report to edit", "Error");
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (auth.GetType() != typeof(Agent))
            {
                MessageBox.Show("You do not have permission to delete reports", "Error");
            }
            else
            {
                if (dtgSubject.SelectedItem == null)
                {
                    MessageBox.Show("You must select and item to be deleted", "Error");
                }
                else
                {
                    db.DeleteReport(dtgSubject.SelectedItem as Report);
                }
                try
                {
                    dtgSubject.ItemsSource = db.GetReportsBySubject(sub.Id);
                }
                catch (Exception)
                {
                    dtgSubject.ItemsSource = null;
                }
            }
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            if (dtgSubject.SelectedItem == null)
            {
                MessageBox.Show("You must select a report to read");
            }
            else
            {
                ReadReport dialog = new ReadReport(dtgSubject.SelectedItem as Report);
                dialog.ShowDialog();
            }
        }
    }
}
