using System;

namespace TravelPal.Managers
{
    public class TravelManager
    {
    }

    public class Travel
    {
        public string Destination { get; set; }
        public Location Country { get; set; }

        public int Travelers { get; set; }

        // TODO Adda list för packinglist

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }

        public Travel(string destination, Location country, int travelers, DateTime startDate, DateTime endDate, int travelDays)
        {
            Destination = destination;
            Country = country;
            Travelers = travelers;
            StartDate = startDate;
            EndDate = endDate;
            TravelDays = travelDays;
        }
    }
}
