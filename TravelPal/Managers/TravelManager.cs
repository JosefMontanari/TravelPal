﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using TravelPal.Classes;

namespace TravelPal.Managers
{
    public static class TravelManager
    {

        public static void GetAllTravels(ListView listView)
        {

            foreach (iUser user in UserManager.Users)
            {
                if (user.GetType() == typeof(User))
                {
                    User userAsUser = (User)user;
                    foreach (Travel travel in userAsUser.Travels)
                    {
                        listView.Items.Add(travel.Destination + " " + user.Username);

                    }
                }
            }
        }
    }

    public class Travel
    {
        public string Destination { get; set; }
        public Country Country { get; set; }

        public int Travelers { get; set; }

        public List<iPackingListItem> PackingList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }

        public Travel(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, List<iPackingListItem> packingList)
        {
            Destination = destination;
            Country = country;
            Travelers = travelers;
            StartDate = startDate;
            EndDate = endDate;
            PackingList = packingList;
            TravelDays = CalculateTravelDays(startDate, endDate);
        }

        public virtual string GetInfo()
        {
            return $"Destination: {Destination}, {Country}. Travelers: {Travelers}. Duration: {TravelDays} days.";
        }

        // Metod för att på ett snyggt sätt displaya start och slut samt hur lång resan är
        public string GetTravelDates()
        {
            string startDateString = StartDate.Year.ToString() + "-" + StartDate.Month.ToString() + "-" + StartDate.Day.ToString();
            string endDateString = EndDate.Year.ToString() + "-" + EndDate.Month.ToString() + "-" + EndDate.Day.ToString();
            string travelDates = startDateString + " to " + endDateString + $"( {CalculateTravelDays(StartDate, EndDate)} Days)";


            return travelDates;
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

    public class WorkTrip : Travel
    {
        public string MeetingDetails { get; set; }

        public WorkTrip(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, string meetingDetails, List<iPackingListItem> packingList) : base(destination, country, travelers, startDate, endDate, packingList)
        {
            MeetingDetails = meetingDetails;
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" Meeting details: {MeetingDetails}";
        }
    }

    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        public Vacation(string destination, Country country, int travelers, DateTime startDate, DateTime endDate, bool allInclusive, List<iPackingListItem> packingList) : base(destination, country, travelers, startDate, endDate, packingList)
        {
            AllInclusive = allInclusive;
        }

        public override string GetInfo()
        {
            if (AllInclusive)
            {
                return base.GetInfo() + $"All inclusive";

            }
            else { return base.GetInfo(); }
        }
    }
}
