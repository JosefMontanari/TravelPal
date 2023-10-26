using System.Windows;
using TravelPal.Classes;
using TravelPal.Managers;
using TravelPal.Pages;

namespace TravelPal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (iUser user in UserManager.Users)
            {
                if (user.GetType() == typeof(User))
                {
                    User thisUser = (User)user;
                    if (user.Username == "user")
                    {
                        thisUser.AddTravels();
                    }

                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password.ToString();
            iUser user = UserManager.SignInUser(username, password);

            if (user != null)
            {
                TravelsWindow travelsWindow = new TravelsWindow(user);
                travelsWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Login unsuccessfull, please try again");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }
    }
}
