namespace AddressBookApp
{
    // UC-1
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}\n" +
                   $"Address: {Address}, {City}, {State} - {Zip}\n" +
                   $"Phone Number: {PhoneNumber}\n" +
                   $"Email: {Email}\n";
        }
    }

    public class AddressBook
    {
        private List<Contact> contacts = new List<Contact>();

        public Contact CreateContact(string firstName, string lastName, string address, string city, string state, string zip, string phoneNumber, string email)
        {
            return new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                City = city,
                State = state,
                Zip = zip,
                PhoneNumber = phoneNumber,
                Email = email
            };
        }

        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }

        public void DisplayContacts()
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine(contact);
            }
        }

        public void EditContactByName(string firstName, string lastName)
        {
            Contact contactToEdit = FindContactByName(firstName, lastName);
            if (contactToEdit != null)
            {
                Console.WriteLine("Enter new details:");

                Console.Write("Enter Address: ");
                contactToEdit.Address = Console.ReadLine();

                Console.Write("Enter City: ");
                contactToEdit.City = Console.ReadLine();

                Console.Write("Enter State: ");
                contactToEdit.State = Console.ReadLine();

                Console.Write("Enter Zip: ");
                contactToEdit.Zip = Console.ReadLine();

                Console.Write("Enter Phone Number: ");
                contactToEdit.PhoneNumber = Console.ReadLine();

                Console.Write("Enter Email: ");
                contactToEdit.Email = Console.ReadLine();

                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void DeleteContactByName(string firstName, string lastName)
        {
            // Convert input to lowercase for case-insensitive comparison
            firstName = firstName.ToLower();
            lastName = lastName.ToLower();

            // Convert contact names to lowercase for comparison
            foreach (var contact in contacts)
            {
                contact.FirstName = contact.FirstName.ToLower();
                contact.LastName = contact.LastName.ToLower();
            }

            Contact contactToDelete = FindContactByName(firstName, lastName);
            if (contactToDelete != null)
            {
                contacts.Remove(contactToDelete);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        private Contact FindContactByName(string firstName, string lastName)
        {
            return contacts.Find(c => c.FirstName == firstName && c.LastName == lastName);
        }
    }

    public class AddressBookMain
    {
        public static void Main(string[] args)
        {
            AddressBookMain addressBook = new AddressBookMain();
            addressBook.DisplayWelcomeMessage();

            AddressBook addressBookInstance = new AddressBook();

            char choice;
            do
            {
                Console.WriteLine("\nAdding New Contact:");
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();

                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();

                Console.Write("Enter Address: ");
                string address = Console.ReadLine();

                Console.Write("Enter City: ");
                string city = Console.ReadLine();

                Console.Write("Enter State: ");
                string state = Console.ReadLine();

                Console.Write("Enter Zip: ");
                string zip = Console.ReadLine();

                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();

                Console.Write("Enter Email: ");
                string email = Console.ReadLine();

                Contact newContact = addressBookInstance.CreateContact(firstName, lastName, address, city, state, zip, phoneNumber, email);
                addressBookInstance.AddContact(newContact);

                Console.Write("Do you want to add another contact? (Y/N): ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (Char.ToUpper(choice) != 'Y')
                {
                    Console.Write("Do you want to edit any contact? (Y/N): ");
                    choice = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (Char.ToUpper(choice) == 'Y')
                    {
                        Console.Write("Enter First Name of Contact to Edit: ");
                        string editFirstName = Console.ReadLine();

                        Console.Write("Enter Last Name of Contact to Edit: ");
                        string editLastName = Console.ReadLine();

                        addressBookInstance.EditContactByName(editFirstName, editLastName);
                    }

                    Console.Write("Do you want to delete any contact? (Y/N): ");
                    choice = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    if (Char.ToUpper(choice) == 'Y')
                    {
                        Console.Write("Enter First Name of Contact to Delete: ");
                        string deleteFirstName = Console.ReadLine();

                        Console.Write("Enter Last Name of Contact to Delete: ");
                        string deleteLastName = Console.ReadLine();

                        addressBookInstance.DeleteContactByName(deleteFirstName, deleteLastName);
                    }
                }
            } while (Char.ToUpper(choice) == 'Y');

            // Display all contacts
            Console.WriteLine("\nAll Contacts:");
            addressBookInstance.DisplayContacts();
        }

        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Address Book Program....!");
        }
    }
}
