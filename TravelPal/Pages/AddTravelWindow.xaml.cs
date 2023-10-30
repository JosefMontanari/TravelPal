using System;
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
                        required = " Required";
                    }
                    else
                    {
                        isRequired = false;
                        required = " Not required";
                    }

                    TravelDocument travelDocument = new(name, isRequired);

                    ListViewItem listViewItem = new();
                    listViewItem.Tag = travelDocument;

                    listViewItem.Content = travelDocument.Name + required;
                    lstLuggage.Items.Add(listViewItem);

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

                        listViewItem.Content = otherItem.Name + " " + quantity;
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
                // TODO lägg till resa
            }
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
