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
        // För att klassen ska kunna nås i hela filen
        private iUser user;
        public TravelsWindow(iUser user)
        {
            InitializeComponent();
            lblUser.Content = user.Username;
            this.user = user;
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
            if (lstTravels.Items.Count > 0 && lstTravels.SelectedIndex != -1)
            {
                ListViewItem selectedItem = new ListViewItem();
                selectedItem = (ListViewItem)lstTravels.SelectedItem;
                Travel selectedTravel = (Travel)selectedItem.Tag;
                lstTravels.Items.Remove(selectedItem);

                User thisUser = (User)user;
                thisUser.Travels.Remove(selectedTravel);


            }
            else
            {
                MessageBox.Show("You must select an existing travel");
            }



        }
    }
}
