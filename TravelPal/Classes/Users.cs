using System;
using System.Collections.Generic;
using TravelPal.Managers;

namespace TravelPal.Classes
{
    public interface iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Country Country { get; set; }

    }

    public class User : iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Country Country { get; set; }

        public List<Travel> Travels = new();


        public void AddTravel(Travel travel)
        {
            Travels.Add(travel);
        }

        public void RemoveTravel(Travel travel)
        {
            Travels.Remove(travel);
        }

        // Metod för att lägga till två travels till den bestämda usern ("user", "password")
        public void AddTravels()
        {
            List<iPackingListItem> packingList = new List<iPackingListItem>()
            {
                new TravelDocument("Passport", false),
                new OtherItem("Toothbrush", 1),

            };

            List<iPackingListItem> packingListTwo = new()
            {
                new TravelDocument("Passport", true),
                new OtherItem("Laptop", 1)

            };

            Vacation travelOne = new("Stockholm", Country.Sweden, 4, DateTime.Now, DateTime.Now, true, packingList);
            Travels.Add(travelOne);
            WorkTrip travelTwo = new("London", Country.UK, 4, DateTime.Now, DateTime.Now, "Meeting Albin to discuss my project", packingListTwo);
            Travels.Add(travelTwo);

        }


    }

    public class Admin : iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Country Country { get; set; }



    }
}
