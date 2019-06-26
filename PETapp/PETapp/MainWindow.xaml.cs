using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Person currentUser;
        private DBHandler db = new DBHandler();
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            List<Person> persons = db.AllPersons();
        }

        private void BtnShowAgents_Click(object sender, RoutedEventArgs e)
        {
            dtgSelected.ItemsSource = db.AllSubPerson("AGENT");
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

        private void BtnSetUser_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(selected))
            {
                MessageBox.Show("You must select a person to set as the current user", "Error");
            }
            else if (selected == "OBSERVANT")
            {
                MessageBox.Show("You cannot set an Observant as current user", "Error");
            }
            else
            {
                if (dtgSelected.SelectedItem == null)
                {
                    MessageBox.Show("You must select a person to set as the current user", "Error");
                }
                else
                {
                    currentUser = dtgSelected.SelectedItem as Person;
                    lblCurrentUser.Content = selected + " " + currentUser.Name;
                }
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            //Null check hvis der ikke er nogen valgt bruger, || stopper den anden statement fra at blive tjekket hvis den første er sand
            if (currentUser == null || currentUser.GetType() != typeof(Agent))
            {
                MessageBox.Show("You do not have permission to perform this task.", "Error");
            }
            else
            {
                if (selected == "OBSERVANT")
                {
                    AddObservant dialog = new AddObservant();
                    dialog.ShowDialog();
                    //Updater itemssource efter der kommer en ny entry til den.
                    dtgSelected.ItemsSource = db.AllSubPerson("OBSERVANT");
                }
                else if (selected == "AGENT")
                {
                    //implement AddAgent dialog here
                }
                else if (selected == "INFORMANT")
                {
                    AddInformant dialog = new AddInformant();
                    dialog.ShowDialog();
                    dtgSelected.ItemsSource = db.AllSubPerson("INFORMANT");
                }
                else
                {
                    //Unreachable?
                    MessageBox.Show("You must select a type of person to add", "Error");
                }
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null || currentUser.GetType() != typeof(Agent))
            {
                MessageBox.Show("You do not have permission to perform this task.", "Error");
            }
            else
            {
                if (selected == "OBSERVANT" && dtgSelected.SelectedItem != null)
                {
                    EditObservant dialog = new EditObservant(dtgSelected.SelectedItem as Observant);
                    if (dialog.ShowDialog() == true)
                    {
                        db.UpdatePerson(dialog.observant as Observant);
                        MessageBox.Show("Successfully updated");
                        dtgSelected.ItemsSource = db.AllSubPerson("OBSERVANT");
                    }

                }
                else if (selected == "AGENT" && dtgSelected.SelectedItem != null)
                {
                    //implement EditAgent dialog here
                }
                else if (selected == "INFORMANT" && dtgSelected.SelectedItem != null)
                {
                    EditInformant dialog = new EditInformant(dtgSelected.SelectedItem as Informant);
                    if (dialog.ShowDialog() == true)
                    {
                        db.UpdatePerson(dialog.informant as Informant);
                        MessageBox.Show("Successfully updated");
                        dtgSelected.ItemsSource = db.AllSubPerson("INFORMANT");
                    }
                }
                else
                {
                    //Unreachable?
                    MessageBox.Show("You must select a type of person to edit", "Error");
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null || currentUser.GetType() != typeof(Agent))
            {
                MessageBox.Show("You do not have permission to perform this task.", "Error");
            }
            else
            {
                if (selected == "OBSERVANT" && dtgSelected.SelectedItem != null)
                {
                    try
                    {
                        db.DeletePerson(dtgSelected.SelectedItem as Observant);
                        dtgSelected.ItemsSource = db.AllSubPerson("OBSERVANT");
                        MessageBox.Show("Person succesfully deleted");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Operation failed: " + ex.Message);
                    }

                }
                else if (selected == "AGENT" && dtgSelected.SelectedItem != null)
                {
                    //implement Delete Agent here
                }
                else if (selected == "INFORMANT" && dtgSelected.SelectedItem != null)
                {
                    try
                    {
                        db.DeletePerson(dtgSelected.SelectedItem as Informant);
                        dtgSelected.ItemsSource = db.AllSubPerson("INFORMANT");
                        MessageBox.Show("Person Successfully deleted");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Operation failed: " + ex.Message);
                    }
                }
                else
                {
                    //Unreachable?
                    MessageBox.Show("You must select a type of person to delete", "Error");
                }
            }
        }

        private void BtnReadReports_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("You must select a user before accessing reports");
            }
            else
            {
                if (dtgSelected.SelectedItem != null && dtgSelected.SelectedItem.GetType() == typeof(Observant))
                {
                    Reports dialog = new Reports(dtgSelected.SelectedItem as Person, currentUser);
                    dialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Only Observants are valid subjects of reports");
                }
            }
        }
    }
}
