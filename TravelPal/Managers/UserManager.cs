using System.Collections.Generic;
using System.Windows;
using TravelPal.Classes;

namespace TravelPal.Managers
{
    public static class UserManager
    {
        public static List<iUser> Users = new()
        {
            new User {Username = "user", Password = "password", Country = Country.Sweden},
            new Admin{Username = "admin",Password = "password", Country = Country.Sweden}
        };

        public static iUser SignedInUser { get; set; }


        public static bool AddUser(User user)
        {
            if (ValidateUsername(user.Username))
            {
                if (ValidatePassword(user.Password))
                {
                    Users.Add(user);
                    return true;

                }
                else
                {
                    return false;
                }
            }
            else
            {

                return false;
            }
        }

        private static bool ValidateUsername(string username)
        {
            bool isValid = true;
            if (!string.IsNullOrWhiteSpace(username) && username.Length > 3)
            {

                foreach (iUser user in Users)
                {
                    if (user.Username == username)
                    {
                        MessageBox.Show("Sorry, that username is already taken!");
                        isValid = false;
                        return isValid;
                    }
                }
            }
            else
            {
                MessageBox.Show("Username needs to be at least three characters!");
                isValid = false;
            }

            return isValid;

        }

        private static bool ValidatePassword(string password)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(password) || password.Length < 3)
            {
                MessageBox.Show("Password needs to be at least three characters!");
                isValid = false;
            }
            return isValid;

        }

        public static void UpdateUsername(iUser user, string newUsername)
        {
            if (ValidateUsername(newUsername))
            {
                user.Username = newUsername;
            }

        }

        public static void RemoveUser(iUser user)
        {
            MessageBox.Show($"{user.Username} successfully removed!");
            Users.Remove(user);
        }

        public static iUser SignInUser(string username, string password)
        {
            bool isValid = false;
            foreach (iUser user in Users)
            {
                if (user.Username == username && user.Password == password)
                {
                    isValid = true;
                    SignedInUser = user;
                }

            }
            if (isValid)
            {
                return SignedInUser;

            }
            else
            {
                return null;
            }
        }
    }
}
