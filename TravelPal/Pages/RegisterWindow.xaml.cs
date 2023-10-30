using System;
using System.Windows;
using TravelPal.Classes;
using TravelPal.Managers;

namespace TravelPal.Pages
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

            foreach (Country countries in Enum.GetValues(typeof(Country)))
            {
                cbCountries.Items.Add(countries);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow("yes");
            mainWindow.Show();
            Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            if (txtPassword.Password == txtPasswordConfirm.Password)
            {

                User user = new();
                user.Username = txtUsername.Text;
                user.Password = txtPasswordConfirm.Password.ToString();
                if (cbCountries.SelectedIndex != -1)
                {
                    user.Country = (Country)cbCountries.SelectedItem;
                    if (UserManager.AddUser(user))
                    {
                        MessageBox.Show("You have now been registred! Welcome!");
                        txtPassword.Clear();
                        txtPasswordConfirm.Clear();
                        txtUsername.Clear();
                    }


                }


                else
                {
                    MessageBox.Show("You need to select a country of origin!");
                }



            }
            else
            {
                MessageBox.Show("Passwords do not match, please try again!");
            }


        }
    }
}
