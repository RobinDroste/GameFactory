﻿using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace GameFactory.Model
{
    internal class PlayerAuth
    {
        SQLPlayerService PlayerService = new();
        string p_loginName;
        string p_password;
        string p_name;
        char p_icon;
        string p_colour;

        internal bool PlayerSignup()
        {
            Console.Clear();
            do
            {
                do
                {
                    Console.WriteLine("Please enter the name you want to signup with:");
                    p_loginName = Console.ReadLine();
                } while (!ValidateLoginName(p_loginName));

            } while (!PlayerService.CheckLoginName(p_loginName));

            do
            {
                Console.WriteLine("Please enter your password:");
                p_password = Console.ReadLine();
            } while (!ValidatePassword(p_password));

            string p_passwordSave = HashPassword(p_password);
            if (PlayerService.SignUpPlayer(p_loginName, p_passwordSave))
            {
                Console.WriteLine("You have successfully signed up! Hit any key to continue");

                Console.ReadLine();
                SavePlayerVariables();

                Console.Clear();
                return false;
            }
            else
            {
                Console.WriteLine("An error occurred while signing you up.");
                Console.ReadLine();
                return false;
            }



        }
        internal int PlayerSignIn()
        {

            bool loggedIn = false;
            Console.Clear();
            Console.WriteLine("Please enter your login name:");
            p_loginName = Console.ReadLine();
            Console.WriteLine("Please enter your password:");
            p_password = Console.ReadLine();
            string p_passwordSave = HashPassword(p_password);
            do
            {
                int p_ident = PlayerService.LoginPlayer(p_loginName, p_passwordSave);
                if (p_ident != 0)
                {
                    Console.WriteLine("You have successfully logged in! Hit any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                    return p_ident;
                }
                else
                {
                    Console.WriteLine("An error occurred while you logged in.");
                    Console.WriteLine("You want to try (a)gain or sign(U)p?");
                    string loginAgainOrSignUp = Console.ReadKey().KeyChar.ToString();

                    if (loginAgainOrSignUp == "a")
                    {
                        return 0;
                    }
                    else if (loginAgainOrSignUp == "u")
                    {
                        PlayerSignup();
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("You have entered an invalid input. Please try again.");
                        return 0;
                    }
                }
            } while (!loggedIn);

        }

        #region Utility Methods
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public bool ValidateLoginName(string loginName)
        {
            if (loginName.Length < 3 || loginName.Length > 16)
            {
                Console.WriteLine("Your login name must be between 3 and 16 characters long.");
                return false;
            }

            Regex alphanumericRegex = new Regex("^[a-zA-Z0-9]+$");
            if (!alphanumericRegex.IsMatch(loginName))
            {
                Console.WriteLine("Your login name must only contain alphanumeric characters.");
                return false;
            }

            return true;
        }
        public static bool ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 16)
            {
                Console.WriteLine("Your password must be between 8 and 16 characters long.");
                return false;
            }
            return true;
        }
        internal bool SavePlayerVariables()
        {
            Console.Clear();
            if (p_loginName == null)
            {
                p_loginName = "Guest";
            }
            p_name = Game.InitializePlayerName();
            p_icon = Game.InitializePlayerIcon();
            p_colour = Game.InitializePlayerColor();

            PlayerService.SavePlayerVariables(p_loginName, p_name, p_icon, p_colour);
            Console.Clear();
            return true;
        }

        #endregion
    }

}
