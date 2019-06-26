using Microsoft.Win32;
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
    /// Interaction logic for AddInformant.xaml
    /// </summary>
    public partial class AddInformant : Window
    {
        private DBHandler db;
        private string imgstring;
        public AddInformant()
        {
            InitializeComponent();
            db = new DBHandler();
            imgstring = "Placeholder";
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.FileName = "Picture";
            fileDialog.DefaultExt = ".png";
            fileDialog.Filter = "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|" +
                "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff";
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(fileDialog.FileName);
                image.EndInit();
                imgPicture.Source = image;
                imgstring = db.ImageToString(fileDialog.FileName);
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbxName.Text))
            {
                MessageBox.Show("You must enter a name");
            }
            else if (String.IsNullOrEmpty(tbxAddress.Text))
            {
                MessageBox.Show("You must enter an Address");
            }
            else if (String.IsNullOrEmpty(tbxNationality.Text))
            {
                MessageBox.Show("You must enter a Nationality (NAN for unknown)");
            }
            else if (String.IsNullOrEmpty(tbxMoP.Text))
            {
                MessageBox.Show("You must enter a Method of Payment");
            }
            else if (String.IsNullOrEmpty(tbxCurrency.Text))
            {
                MessageBox.Show("You must enter a Currency");
            }
            else
            {
                if (tbxNationality.Text.Count() != 3)
                {
                    MessageBox.Show("Nationality must follow the standards of ISO-3166, Alpha-3");
                }
                else
                {
                    Informant i = new Informant(tbxName.Text, tbxAddress.Text, tbxNationality.Text, imgstring, tbxDescription.Text, tbxMoP.Text, tbxCurrency.Text);
                    db.NewPerson(i);
                    this.Close();
                }
            }
        }
    }
}
