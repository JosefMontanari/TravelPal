using TravelPal.Managers;

namespace TravelPal.Classes
{
    public interface iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Location Country { get; set; }

    }

    public class User : iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Location Country { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class Admin : iUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Location Country { get; set; }

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
