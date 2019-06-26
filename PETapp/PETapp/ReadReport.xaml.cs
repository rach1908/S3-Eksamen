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
    /// Interaction logic for ReadReport.xaml
    /// </summary>
    public partial class ReadReport : Window
    {
        private Report report;
        public ReadReport(Report r)
        {
            InitializeComponent();
            report = r;
            tbkText.Text = report.Text;
            lblAuthor.Content = "Author: " + report.Author.Name;
            lblSubject.Content = "Subject: " + report.Subject.Name;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnComment_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
