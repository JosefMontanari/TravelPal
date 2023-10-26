using System.Windows;
using System.Windows.Controls;
using TravelPal.Classes;
using TravelPal.Managers;

namespace TravelPal.Pages
{
    /// <summary>
    /// Interaction logic for TravelDetails.xaml
    /// </summary>
    public partial class TravelDetails : Window
    {
        // För att klassen ska kunna nås i hela filen
        private Travel travel;
        private iUser user;
        public TravelDetails(Travel travel, iUser user)
        {
            this.user = user;
            this.travel = travel;
            InitializeComponent();
            LoadAllInfo();
        }

        public void LoadAllInfo()
        {
            if (travel.GetType() == typeof(Vacation))
            {
                Vacation vacation = (Vacation)travel;
                txtCity.Text = vacation.Destination;
                txtCountry.Text = vacation.Country.ToString();
                txtTravelers.Text = vacation.Travelers.ToString();
                if (vacation.AllInclusive)
                {
                    txtAllInclusive.Text = "Yes";
                }
                else
                {
                    txtAllInclusive.Text = "No";
                }

                foreach (iPackingListItem packingItem in vacation.PackingList)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Tag = packingItem;
                    lstLuggage.Items.Add(listViewItem);
                }

            }
            else if (travel.GetType() == typeof(WorkTrip))
            {
                WorkTrip worktrip = (WorkTrip)travel;
                txtCity.Text = worktrip.Destination;
                txtCountry.Text = worktrip.Country.ToString();
                txtTravelers.Text = worktrip.Travelers.ToString();
                txtMeetingDetails.Text = worktrip.MeetingDetails.ToString();

                foreach (iPackingListItem packingItem in worktrip.PackingList)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Tag = packingItem;
                    lstLuggage.Items.Add(listViewItem);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new TravelsWindow(user);
            travelsWindow.Show();
            Close();

        }
    }
}
