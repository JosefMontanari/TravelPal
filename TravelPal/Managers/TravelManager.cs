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

        public Travel(string destination, Location country, int travelers, DateTime startDate, DateTime endDate)
        {
            Destination = destination;
            Country = country;
            Travelers = travelers;
            StartDate = startDate;
            EndDate = endDate;
            TravelDays = CalculateTravelDays(startDate, endDate);
        }

        public virtual string GetInfo()
        {
            return $"Destination: {Destination}, {Country}. ";
        }

        private int CalculateTravelDays(DateTime startDate, DateTime endDate)
        {
            var dateAndTime = startDate;
            var dateOnly = dateAndTime.Date;

            var dateAndTimeTwo = endDate;
            var dateOnlyTwo = dateAndTimeTwo.Date;

            var days = dateOnlyTwo - dateOnly;
            var daysInDays = days.Days;

            return daysInDays;
        }
    }
}
