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
    /// Interaction logic for EditObservant.xaml
    /// </summary>
    public partial class EditObservant : Window
    {
        public Observant observant;
        private DBHandler db;
        private string imgString;
        public EditObservant(Observant o)
        {
            InitializeComponent();
            db = new DBHandler();
            observant = o;
            imgString = o.SerializedImage;
            lblName.Content = observant.Name;
            lblAddress.Content = observant.Address;
            lblNationality.Content = observant.Nationality;
            lblDescription.Content = observant.Description;
            tbxName.Text = observant.Name;
            tbxAddress.Text = observant.Address;
            tbxNationality.Text = observant.Nationality;
            tbxDescription.Text = observant.Description;
            if (o.SerializedImage.ToUpper() != "PLACEHOLDER")
            {
                imgPicture.Source = db.StringToImage(o.SerializedImage);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
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
            else
            {
                if (tbxNationality.Text.Count() != 3)
                {
                    MessageBox.Show("Nationality must follow the standards of ISO-3166, Alpha-3");
                }
                else
                {
                    observant.Name = tbxName.Text;
                    observant.Address = tbxAddress.Text;
                    observant.Nationality = tbxNationality.Text;
                    observant.Description = tbxDescription.Text;
                    observant.SerializedImage = imgString;
                    DialogResult = true;
                }
            }
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
                imgString = db.ImageToString(fileDialog.FileName);
            }
        }
    }
}
