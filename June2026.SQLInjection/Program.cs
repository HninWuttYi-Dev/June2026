
using June2026.SQLInjection;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("=====Login=====");
Console.WriteLine("Please enter your username:");
string username = Console.ReadLine();
Console.WriteLine("Enter your password");
string password = Console.ReadLine();
LoginService loginService = new LoginService();
loginService.Login(username, password);