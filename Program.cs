using System;
using System.Linq;

namespace Cindy_sPhoneDirectoryconsole {
    class Program {
        static void Main (string[] args) {
            Menu ();
        }

        private static void Menu () {
            Console.WriteLine ($"1. Create a contact\n2. Read all contacts in phonebook\n3. Update an existing contact\n4. Delete a contact\n0. Exit");
            int userResponse = UserOptionResponse (new int[] { 0, 1, 2, 3, 4 });

            switch (userResponse) {
                case 1:
                    CreateContact ();
                    break;
                case 2:
                    ReadAllContacts ();
                    break;
                case 3:
                    UpdateContact ();
                    break;
                case 4:
                    DeleteContact ();
                    break;
                case 0:
                    Environment.Exit (0);
                    break;
                default:
                    break;
            }
        }

        private static void CreateContact () {
            Console.WriteLine ("Please enter the contacts first name.");
            string contactFirstName = NonEmptyValue ();
            Console.WriteLine ("Please enter the contacts last name.");
            string contactLastName = NonEmptyValue ();

            Console.WriteLine ("Please enter the contacts phone number.");
            string contactPhoneNumber = NonEmptyValue ();

            // Check if the first name and last name the user entered already belongs to another contact in phone book
            bool contactExist = PhoneBook.FindContactByFirstNameAndLastName (contactFirstName, contactLastName) != null;

            // if the first name and last name entered by the user belongs to another contact in phone book,
            // the user is forced to enter a different first name and last name
            while (contactExist) {
                Console.WriteLine ($"Contact with First Name:'{contactFirstName}' and Last Name: '{contactLastName}' already exist. Enter a different name");
                Console.WriteLine ("First Name");
                contactFirstName = NonEmptyValue ();
                Console.WriteLine ("Last Name");
                contactLastName = NonEmptyValue ();
                contactExist = PhoneBook.FindContactByFirstNameAndLastName (contactFirstName, contactLastName) != null;
            }

            Contact contact = new Contact ();
            contact.FirstName = contactFirstName;
            contact.LastName = contactLastName;
            contact.PhoneNumber = contactPhoneNumber;

            PhoneBook.ContactList.Add (contact);
            Console.WriteLine ();
            Console.WriteLine ("New contact added");
            DisplayContact (contact);

            Console.WriteLine ();
            Console.WriteLine ($"1. Create another contact\n0. Back");
            int userResponse = UserOptionResponse (new int[] { 0, 1 });

            switch (userResponse) {
                case 1:
                    CreateContact ();
                    break;
                case 0:
                    Menu ();
                    break;
                default:
                    break;
            }
        }

        private static void ReadAllContacts () {
            Console.WriteLine ();
            if (PhoneBook.ContactList.Count == 0) {
                Console.WriteLine ("There are no contacts in phone book");
            } else {
                foreach (var contact in PhoneBook.ContactList) {
                    DisplayContact (contact);
                }
            }

            Console.WriteLine ();
            Console.WriteLine ($"0. Back");
            int userResponse = UserOptionResponse (new int[] { 0 });

            switch (userResponse) {
                case 0:
                    Menu ();
                    break;
                default:
                    break;
            }
        }

        private static void UpdateContact () {
            Console.WriteLine ();
            Console.WriteLine ("Please enter the first name and last name of the contact to update.");
            Console.WriteLine ("First Name");
            string contactFirstName = NonEmptyValue ();
            Console.WriteLine ("Last Name");
            string contactLastName = NonEmptyValue ();

            // Get user contact from phone book using the combination his/her first name and last name
            Contact contact = PhoneBook.FindContactByFirstNameAndLastName (contactFirstName, contactLastName);

            // if the contact is in phone book, update it otherwise, tell the user the contact does not exist
            if (contact == null) {
                Console.WriteLine ($"Contact with First Name:'{contact.FirstName}' and Last Name: '{contact.LastName}' does not exist.");
            } else {
                Console.WriteLine ("Please enter the contacts first name.");
                string firstName = NonEmptyValue ();
                Console.WriteLine ("Please enter the contacts last name.");
                string lastName = NonEmptyValue ();

                Console.WriteLine ("Please enter the contacts phone number.");
                string phoneNumber = Console.ReadLine ();

                // check if the new first name and last name the user entered already belongs to another contact in phone book
                bool contactExist = PhoneBook.FindContactByFirstNameAndLastName (firstName, lastName) != null;

                // if the new first name and last name entered by the user belongs to another contact in phone book,
                // the user is forced to enter a different first name and last name
                while (contactExist) {
                    Console.WriteLine ($"Contact with First Name:'{firstName}' and Last Name: '{lastName}' already exist. Enter a different name");
                    Console.WriteLine ("First Name");
                    firstName = NonEmptyValue ();
                    Console.WriteLine ("Last Name");
                    lastName = NonEmptyValue ();
                    contactExist = PhoneBook.FindContactByFirstNameAndLastName (firstName, lastName) != null;
                }

                contact.FirstName = firstName;
                contact.LastName = lastName;
                if (!string.IsNullOrEmpty (phoneNumber) && !string.IsNullOrWhiteSpace (phoneNumber)) {
                    contact.PhoneNumber = phoneNumber;
                }

                Console.WriteLine ();
                Console.WriteLine ($"Contact with First Name:'{contactFirstName}' and Last Name: '{contactLastName}' updated");
                DisplayContact (contact);
            }

            Console.WriteLine ();
            Console.WriteLine ($"1. Update another contact phone number\n0. Back");
            int userResponse = UserOptionResponse (new int[] { 0, 1 });

            switch (userResponse) {
                case 1:
                    UpdateContact ();
                    break;
                case 0:
                    Menu ();
                    break;
                default:
                    break;
            }
        }

        private static void DeleteContact () {
            Console.WriteLine ();
            Console.WriteLine ("Please enter the first name and last name of the contact to delete.");
            Console.WriteLine ("First Name");
            string contactFirstName = NonEmptyValue ();
            Console.WriteLine ("Last Name");
            string contactLastName = NonEmptyValue ();

            // Get user contact from phone book using the combination his/her first name and last name
            Contact contact = PhoneBook.FindContactByFirstNameAndLastName (contactFirstName, contactLastName);

            // if the contact is in phone book, delete it otherwise, tell the user the contact does not exist
            if (contact == null) {
                Console.WriteLine ($"Contact with First Name:'{contact.FirstName}' and Last Name: '{contact.LastName}' does not exist.");
            } else {
                PhoneBook.ContactList.Remove (contact);
            }

            Console.WriteLine ();
            Console.WriteLine ($"Contact with First Name:'{contactFirstName}' and Last Name: '{contactLastName}' deleted");
            Console.WriteLine ($"1. Delete another contact phone number\n0. Back");
            int userResponse = UserOptionResponse (new int[] { 0, 1 });

            switch (userResponse) {
                case 1:
                    DeleteContact ();
                    break;
                case 0:
                    Menu ();
                    break;
                default:
                    break;
            }
        }

        #region Helper Methods

        /// <summary>
        /// Force user to respond with valid option
        /// </summary>
        /// <param name="options">the options to check user response against</param>
        /// <returns>user response</returns>
        private static int UserOptionResponse (int[] options) {
            int response = -1;
            string userInput = Console.ReadLine ();
            if (int.TryParse (userInput, out response)) {
                if (!options.Contains (response)) {
                    Console.WriteLine ("Wrong option. Choose a valid option.");
                    response = UserOptionResponse (options);
                }
            } else {
                Console.WriteLine ("Invalid input. Enter number only");
                response = UserOptionResponse (options);
            }

            return response;
        }

        /// <summary>
        /// Force user to respond with valid option
        /// </summary>
        /// <param name="options">the options to check user response against</param>
        /// <returns>user response</returns>
        private static string UserOptionResponse (string[] options) {
            string userInput = Console.ReadLine ();
            if (!options.Contains (userInput)) {
                Console.WriteLine ("Wrong option. Choose a valid option.");
                userInput = UserOptionResponse (options);
            }

            return userInput;
        }

        /// <summary>
        /// Force user to type something
        /// </summary>
        private static string NonEmptyValue () {
            string userInput = Console.ReadLine ();
            if (string.IsNullOrEmpty (userInput) || string.IsNullOrWhiteSpace (userInput)) {
                Console.WriteLine ("value must not be empty");
                userInput = NonEmptyValue ();
            }

            return userInput;
        }

        /// <summary>
        /// Display contact details in a nice format
        /// </summary>
        /// <param name="contact"></param>
        private static void DisplayContact (Contact contact) {
            Console.WriteLine ("...............................");
            Console.WriteLine ($"First Name: {contact.FirstName}");
            Console.WriteLine ($"Last Name: {contact.LastName}");
            Console.WriteLine ($"Phone Number: {contact.PhoneNumber}");
        }

        #endregion
    }
}