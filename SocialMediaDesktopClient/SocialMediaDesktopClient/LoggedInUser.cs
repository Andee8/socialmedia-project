using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SocialMediaClient
{
    public static class LoggedInUser
    {
        public static int Id { get; set; }
        public static string Name { get; set; }
        public static string Email { get; set; }

        public static bool IsLoggedIn => Id != 0; // Verifică dacă utilizatorul este logat

        public static void LogIn(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static void LogOut()
        {
            Id = 0;
            Name = null;
            Email = null;
        }
    }
}
