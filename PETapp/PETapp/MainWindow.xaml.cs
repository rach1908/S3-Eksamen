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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PETapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selected = "";
        private DBHandler db = new DBHandler();
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            List<Person> persons = db.AllPersons();
        }

        private void BtnShowAgents_Click(object sender, RoutedEventArgs e)
        {
            List<Person> li = db.AllSubPerson("AGENT");
            dtgSelected.ItemsSource = li;
            selected = "AGENT";
        }

        private void BtnShowInformants_Click(object sender, RoutedEventArgs e)
        {
            dtgSelected.ItemsSource = db.AllSubPerson("INFORMANT");
            selected = "INFORMANT";
        }

        private void BtnShowObservants_Click(object sender, RoutedEventArgs e)
        {
            dtgSelected.ItemsSource = db.AllSubPerson("OBSERVANT");
            selected = "OBSERVANT";
        }
    }
}
