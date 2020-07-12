using System.Collections.Generic;
using System.Linq;

namespace Cindy_sPhoneDirectoryconsole {
    public static class PhoneBook {
        public static List<Contact> ContactList { get; set; } = new List<Contact> ();

        public static Contact FindContactByFirstNameAndLastName (string firstName, string lastName) {
            return ContactList.FirstOrDefault (x => $"{x.FirstName}{x.LastName}".ToLower () == $"{firstName}{lastName}".ToLower ());
        }
    }
}