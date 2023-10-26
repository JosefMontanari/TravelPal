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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {

            if (txtPassword.Password == txtPasswordConfirm.Password)
            {

                UserManager userManager = new UserManager();
                User user = new();
                user.Username = txtUsername.Text;
                user.Password = txtPasswordConfirm.Password.ToString();
                try
                {
                    user.Country = (Country)cbCountries.SelectedItem;
                    if (userManager.AddUser(user))
                    {
                        MessageBox.Show("You have now been registred! Welcome!");
                    }

                    else
                    {
                        MessageBox.Show("Passwords do not match, please try again!");
                    }
                }


                catch (NullReferenceException ex)
                {
                    MessageBox.Show("You need to select a country of origin!");
                }



            }
        }
    }
}
