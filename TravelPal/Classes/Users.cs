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

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public void AddTravel(Travel travel)
        {
            Travels.Add(travel);
        }

        public void RemoveTravel(Travel travel)
        {
            Travels.Remove(travel);
        }


    }

    public class Admin : iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Country Country { get; set; }

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
