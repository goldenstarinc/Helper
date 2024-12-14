using System;
using System.Text;
using System.Security.Cryptography;

using CurrentSession = CurrentSessionDataProvider.CurrentSessionDataProvider;
using UtilitiesLibrary;
using AccountService;
using PostingService;
using NotificationService;
using MySqlConnector;
using System.Runtime.CompilerServices;
class Program
{
    static void Main(string[] args)
    {
        PasswordHasherClass hasher = new PasswordHasherClass();
        AuthenticationClass.LogIn("dude", hasher.Hash("12345"));
    }


    static void RegisterUser(string name)
    {
        PasswordHasherClass hasher = new PasswordHasherClass();

        QueryExecutor.ExecuteQuery(() =>
        {
            RegistrationClass.SignUp(name, hasher.Hash("12345"), "81111111111");
        });
    }
}