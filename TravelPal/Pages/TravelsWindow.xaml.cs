using System.Windows;
using System.Windows.Controls;
using TravelPal.Classes;
using TravelPal.Managers;

namespace TravelPal.Pages
{
    /// <summary>
    /// Interaction logic for TravelsWindow.xaml
    /// </summary>
    public partial class TravelsWindow : Window
    {
        public TravelsWindow(iUser user)
        {
            InitializeComponent();
            lblUser.Content = user.Username;

            if (user.GetType() == typeof(User))
            {
                User userAsUser = (User)user;
                foreach (Travel travel in userAsUser.Travels)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Tag = travel;
                    listViewItem.Content = travel.Destination.ToString();
                    lstTravels.Items.Add(listViewItem);
                }

            }

        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = new ListViewItem();
            selectedItem = (ListViewItem)lstTravels.SelectedItem;



        }
    }
}
