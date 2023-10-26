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
                    listViewItem.Content = travel.Destination;
                    lstTravels.Items.Add(travel);
                }

            }

        }
    }
}
