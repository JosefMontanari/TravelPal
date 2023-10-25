using System;
using System.Windows;
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
    }
}
