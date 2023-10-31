using System;
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

        // Metod som hämtar alla info om resan och stoppar in den i rutorna
        public void LoadAllInfo()
        {
            if (travel.GetType() == typeof(Vacation))
            {
                Vacation vacation = (Vacation)travel;
                txtCity.Text = vacation.Destination;
                txtCountry.Text = vacation.Country.ToString();
                txtTravelers.Text = vacation.Travelers.ToString();
                txtTraveldays.Text = vacation.GetTravelDates();
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
                    DisplayAllPackingItems(packingItem);

                }

            }
            else if (travel.GetType() == typeof(WorkTrip))
            {
                WorkTrip worktrip = (WorkTrip)travel;
                txtCity.Text = worktrip.Destination;
                txtCountry.Text = worktrip.Country.ToString();
                txtTravelers.Text = worktrip.Travelers.ToString();
                txtMeetingDetails.Text = worktrip.MeetingDetails.ToString();
                txtTraveldays.Text = worktrip.GetTravelDates();
                foreach (iPackingListItem packingItem in worktrip.PackingList)
                {
                    DisplayAllPackingItems(packingItem);

                }
            }
        }

        // Metod som loopar över alla packingitems och displayar dem
        private void DisplayAllPackingItems(iPackingListItem packingItem)
        {
            if (packingItem.GetType() == typeof(TravelDocument))
            {
                TravelDocument travelDocument = (TravelDocument)packingItem;
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = packingItem;
                listViewItem.Content = travelDocument.GetInfo();
                lstLuggage.Items.Add(listViewItem);

            }
            else if (packingItem.GetType() == typeof(OtherItem))
            {
                OtherItem otherItem = (OtherItem)packingItem;
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Tag = packingItem;
                listViewItem.Content = otherItem.GetInfo();
                lstLuggage.Items.Add(listViewItem);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new TravelsWindow(user);
            travelsWindow.Show();
            Close();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            btnEdit.IsEnabled = false;
            btnSave.IsEnabled = true;

            if (travel.GetType() == typeof(Vacation))
            {
                txtAllInclusive.IsReadOnly = false;

            }
            txtCity.IsReadOnly = false;
            txtCountry.IsReadOnly = false;
            txtTravelers.IsReadOnly = false;
            if (travel.GetType() == typeof(WorkTrip))
            {
                txtMeetingDetails.IsReadOnly = false;

            }

            txbValidCountries.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Kolla så alla fälten är korrekt ifyllda

            if (!string.IsNullOrWhiteSpace(txtCity.Text) && !string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                bool isCorrectCountry = false;
                Country newCountry = travel.Country;
                foreach (Country country in Enum.GetValues(typeof(Country)))
                {
                    if (txtCountry.Text == country.ToString())
                    {
                        isCorrectCountry = true;
                        newCountry = country;
                    }
                }
                if (!isCorrectCountry)
                {
                    MessageBox.Show("Please input a valid country, check the list!");
                }
                else
                {
                    bool isCorrectTravelers = int.TryParse(txtTravelers.Text, out int travelersCount);

                    if (!isCorrectTravelers)
                    {
                        MessageBox.Show("Please write the amount of travelers in numbers");
                    }
                    else
                    {
                        if (txtAllInclusive.IsReadOnly)
                        {
                            // Kolla så att meeting details är ifyllt
                            if (string.IsNullOrWhiteSpace(txtMeetingDetails.Text))
                            {
                                MessageBox.Show("Please fill in the details of your meeting");
                            }
                            else
                            {
                                //Spara de nya uppgifterna i klassen
                                WorkTrip workTrip = (WorkTrip)travel;
                                workTrip.Destination = txtCity.Text;
                                workTrip.Travelers = travelersCount;
                                workTrip.Country = newCountry;
                                workTrip.MeetingDetails = txtMeetingDetails.Text;

                                travel = workTrip;
                                btnSave.IsEnabled = false;
                                btnEdit.IsEnabled = true;
                                txbValidCountries.Visibility = Visibility.Hidden;
                            }


                        }
                        else if (txtMeetingDetails.IsReadOnly)
                        {
                            // Kolla så att all inclusive är korrekt ifyllt
                            if (txtAllInclusive.Text == "Yes" || txtAllInclusive.Text == "yes")
                            {
                                //Spara de nya uppgifterna i klassen
                                Vacation vacation = (Vacation)travel;
                                vacation.Destination = txtCity.Text;
                                vacation.Travelers = travelersCount;
                                vacation.Country = newCountry;
                                vacation.AllInclusive = true;

                                travel = vacation;

                            }
                            else if (txtAllInclusive.Text == "No" || txtAllInclusive.Text == "no")
                            {
                                //Spara de nya uppgifterna i klassen
                                Vacation vacation = (Vacation)travel;
                                vacation.Destination = txtCity.Text;
                                vacation.Travelers = travelersCount;
                                vacation.Country = newCountry;
                                vacation.AllInclusive = false;

                                travel = vacation;
                                btnSave.IsEnabled = false;
                                btnEdit.IsEnabled = true;
                                txbValidCountries.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                MessageBox.Show("All inclusive needs to be either yes or no!");
                            }


                        }

                    }

                }


            }

            else
            {
                MessageBox.Show("Please fill in all the unlocked textboxes.");
            }


        }
    }
}
