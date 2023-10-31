using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TravelPal.Classes;
using TravelPal.Managers;

namespace TravelPal.Pages
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {
        private User user;

        public AddTravelWindow(User user)
        {
            this.user = user;
            InitializeComponent();

            LoadAllBoxes();
        }

        private void LoadAllBoxes()
        {
            for (int i = 0; i < 10; i++)
            {
                cbQuantity.Items.Add(i + 1);
            }

            cbPurpose.Items.Add("Vacation");
            cbPurpose.Items.Add("Worktrip");

            foreach (Country countries in Enum.GetValues(typeof(Country)))
            {
                cbCountries.Items.Add(countries);
            }

            for (int i = 0; i < 10; i++)
            {
                cbTravelers.Items.Add(i + 1);
            }
        }

        private void btnAddPacking_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLuggage.Text))
            {
                if (chkTravelDocument.IsChecked == true)
                {
                    //Lägg till ett travel document

                    string name = txtLuggage.Text;
                    bool isRequired;
                    string required;
                    if (chkRequired.IsChecked == true)
                    {
                        isRequired = true;
                    }
                    else
                    {
                        isRequired = false;
                    }

                    AddTravelDocument(name, isRequired);

                    ClearLuggageFields();
                }

                else
                {
                    if (cbQuantity.SelectedIndex != -1)
                    {
                        // Lägg till ett other item
                        string name = txtLuggage.Text;
                        int quantity = int.Parse(cbQuantity.SelectedItem.ToString());

                        OtherItem otherItem = new(name, quantity);

                        ListViewItem listViewItem = new();
                        listViewItem.Tag = otherItem;

                        listViewItem.Content = otherItem.GetInfo();
                        lstLuggage.Items.Add(listViewItem);

                        ClearLuggageFields();



                    }
                    else
                    {
                        MessageBox.Show("You have to select a quantity!");
                    }
                }

            }
            else
            {
                MessageBox.Show("You have to give the packing item a name!");
            }

        }

        private void AddTravelDocument(string name, bool isRequired)
        {
            TravelDocument travelDocument = new(name, isRequired);

            ListViewItem listViewItem = new();
            listViewItem.Tag = travelDocument;
            listViewItem.Content = travelDocument.GetInfo();


            lstLuggage.Items.Add(listViewItem);
        }

        private void ClearLuggageFields()
        {
            txtLuggage.Clear();
            chkRequired.IsChecked = false;
            chkTravelDocument.IsChecked = false;
            cbQuantity.SelectedIndex = -1;
        }

        private void chkTravelDocument_Checked(object sender, RoutedEventArgs e)
        {
            if (chkTravelDocument.IsChecked == true)
            {
                cbQuantity.IsEnabled = false;
                cbQuantity.SelectedIndex = -1;
                chkRequired.IsEnabled = true;
            }

        }

        private void chkTravelDocument_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkTravelDocument.IsChecked == false)
            {
                cbQuantity.IsEnabled = true;
                chkRequired.IsEnabled = false;
                chkRequired.IsChecked = false;
            }
        }

        private void cbPurpose_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbPurpose.SelectedIndex != -1)
            {
                if (cbPurpose.SelectedItem.ToString() == "Vacation")
                {
                    chkAllInclusive.IsEnabled = true;
                    txtMeetingDetails.IsEnabled = false;
                }
                else if (cbPurpose.SelectedItem.ToString() == "Worktrip")
                {
                    chkAllInclusive.IsEnabled = false;
                    txtMeetingDetails.IsEnabled = true;
                }

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            TravelsWindow travelsWindow = new(user);
            travelsWindow.Show();
            Close();
        }

        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCity.Text) || cbCountries.SelectedIndex == -1
                || cbTravelers.SelectedIndex == -1 || cbPurpose.SelectedIndex == -1
                || dtpStart.SelectedDate == null || dtpEnd.SelectedDate == null)
            {
                MessageBox.Show("You need to fill all fields!");
            }
            else
            {

                if (cbPurpose.SelectedIndex == 0)
                {
                    //Vacation
                    List<iPackingListItem> packingList = new();
                    foreach (ListViewItem item in lstLuggage.Items)
                    {

                        iPackingListItem packingItem = (iPackingListItem)item.Tag;
                        packingList.Add(packingItem);

                    }

                    Vacation vacation = new(txtCity.Text, (Country)cbCountries.SelectedItem, (int)cbTravelers.SelectedItem,
                        (DateTime)dtpStart.SelectedDate, (DateTime)dtpEnd.SelectedDate, (bool)chkAllInclusive.IsChecked, packingList);

                    user.Travels.Add(vacation);
                }
                else if (cbPurpose.SelectedIndex == 1)
                {
                    //Worktrip
                    List<iPackingListItem> packingList = new();
                    foreach (ListViewItem item in lstLuggage.Items)
                    {

                        iPackingListItem packingItem = (iPackingListItem)item.Tag;
                        packingList.Add(packingItem);

                    }
                    WorkTrip workTrip = new(txtCity.Text, (Country)cbCountries.SelectedItem, (int)cbTravelers.SelectedItem,
                        (DateTime)dtpStart.SelectedDate, (DateTime)dtpEnd.SelectedDate, txtMeetingDetails.Text, packingList);

                    user.Travels.Add(workTrip);

                }
                ClearAllFields();
            }
        }

        private void ClearAllFields()
        {
            ClearLuggageFields();
            txtCity.Text = string.Empty;
            txtMeetingDetails.Text = string.Empty;
            cbCountries.SelectedIndex = -1;
            cbTravelers.SelectedIndex = -1;
            cbPurpose.SelectedIndex = -1;
            dtpEnd.SelectedDate = null;
            dtpStart.SelectedDate = null;

        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool userIsEuropean = false;
            bool countryIsEuropean = false;
            // Kolla om det finns ett travel document "passport" i listan redan, i så fall ersätter vi detta
            for (int i = 0; i < lstLuggage.Items.Count; i++)
            {
                ListViewItem listViewItem = (ListViewItem)lstLuggage.Items[i];

                if (listViewItem.ToString().Contains("Passport") || listViewItem.ToString().Contains("passport"))
                {
                    lstLuggage.Items.Remove(listViewItem);
                }

            }
            if (cbCountries.SelectedIndex != -1)
            {

                // Kolla om användaren är från europa
                foreach (Country countries in Enum.GetValues(typeof(EuropeanCountry)))
                {
                    if (user.Country == countries)
                    {
                        userIsEuropean = true;
                    }
                }

                // Kolla om destinationen är i europa
                foreach (Country countries in Enum.GetValues(typeof(EuropeanCountry)))
                {
                    if ((Country)cbCountries.SelectedItem == countries)
                    {
                        countryIsEuropean = true;
                    }
                }


                // Om användaren är från samma land behövs inte ett passport oavset vad
                if (user.Country == (Country)cbCountries.SelectedItem)
                {
                    AddTravelDocument("Passport", false);

                }
                else if (!userIsEuropean)
                {
                    AddTravelDocument("Passport", true);
                }

                else if (!countryIsEuropean)
                {
                    AddTravelDocument("Passport", true);

                }

                else
                {
                    AddTravelDocument("Passport", false);

                }
            }
        }
    }
}
