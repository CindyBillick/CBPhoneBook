// using System;
// using System.Collections.Generic;
// using System.Text;

// namespace Cindy_sPhoneDirectoryconsole
// {
//     public static class User
//     {
//         public static string Response { get; set; }
//         public static void ReadAllContacts()
//         {
//             //Reading all contacts
//             foreach (var c in PhoneBook.ContactList)
//             {
//                 Console.WriteLine($"Firstname:{c.FirstName}| Lastname:{c.LastName}|+" +
//                 $" Phonenumber:{c.PhoneNumber}");
//             }
//         }
//         public static Contact CreateContact()
//         {
//             Contact c = new Contact();
            
//             Console.WriteLine("Please enter the contacts first name.");
//             c.FirstName = Console.ReadLine();
//             Console.WriteLine("Please enter the contacts last name.");
//             c.LastName = Console.ReadLine();
//             Console.WriteLine("Please enter the contacts phone number.");
//             c.PhoneNumber = Console.ReadLine();

//             return c;
//         }
//     }
// }