namespace AddressBookApp
{
    // UC-1
    public class Persons
    {
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"| Name: {First_name} {Last_name} | Mobile: {Number} | Email: {Email} | Address: {Address} |";
        }
    }

    public class AddressBook
    {
        private List<Persons> contacts = new List<Persons>();

        public void AddContact(Persons person)
        {
            contacts.Add(person);
        }

        public void DisplayContacts()
        {
            if (contacts.Count > 0)
            {
                foreach (var person in contacts)
                {
                    Console.WriteLine(person);
                }

            }
            else { Console.WriteLine("Your Contact list is empty"); }

        }

        public void EditContactByName()
        {
            Console.WriteLine("\nEditing Contact:");
            Console.Write("Enter First Name of Contact to Edit: ");
            string editFirstName = Console.ReadLine();

            Console.Write("Enter Last Name of Contact to Edit: ");
            string editLastName = Console.ReadLine();

            Persons personToEdit = FindContactByName(editFirstName, editLastName);
            if (personToEdit != null)
            {
                Console.WriteLine("Enter new details to Update:");

                Console.Write("Enter first Name :");
                personToEdit.First_name = Console.ReadLine();

                Console.Write("Enter last Name :");
                personToEdit.Last_name = Console.ReadLine();

                Console.Write("Enter Address: ");
                personToEdit.Address = Console.ReadLine();

                Console.Write("Enter Phone Number: ");
                personToEdit.Number = Console.ReadLine();

                Console.Write("Enter Email: ");
                personToEdit.Email = Console.ReadLine();

                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void DeleteContactByName()
        {
            Console.WriteLine("\nDeleting Contact:");
            Console.Write("Enter First Name of Contact to Delete: ");
            string deleteFirstName = Console.ReadLine();

            Console.Write("Enter Last Name of Contact to Delete: ");
            string deleteLastName = Console.ReadLine();

            Persons personToDelete = FindContactByName(deleteFirstName, deleteLastName);
            if (personToDelete != null)
            {
                contacts.Remove(personToDelete);
                Console.WriteLine("Contact deleted successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        private Persons FindContactByName(string firstName, string lastName)
        {
            return contacts.Find(c => c.First_name.Equals(firstName, StringComparison.OrdinalIgnoreCase) && c.Last_name.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class AddressBookMain
    {
        private static Dictionary<string, AddressBook> addressBooks = new Dictionary<string, AddressBook>();

        public static void Main(string[] args)
        {
            AddressBookMain addressBook = new AddressBookMain();
            addressBook.DisplayWelcomeMessage();

            char choice;
            do
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Add new Address Book");
                Console.WriteLine("2 - Select Address Book");
                Console.Write("Enter your choice: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        AddAddressBook();
                        break;
                    case '2':
                        SelectAddressBook();
                        break;
                    case '0':
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            } while (choice != '0');
        }

        private static void AddAddressBook()
        {
            Console.Write("Enter Name for the new Address Book: ");
            string name = Console.ReadLine();
            addressBooks.Add(name, new AddressBook());
            Console.WriteLine($"Address Book '{name}' added successfully.");
        }

        private static void SelectAddressBook()
        {
            Console.WriteLine("\nSelect Address Book:");
            int count = 1;
            foreach (var name in addressBooks.Keys)
            {
                Console.WriteLine($"{count}. {name}");
                count++;
            }

            Console.Write("Enter the number for the Address Book to select: ");
            int index;
            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= addressBooks.Count)
            {
                string selectedBook = addressBooks.Keys.ElementAt(index - 1);
                Console.WriteLine($"\nSelected Address Book: {selectedBook}");

                AddressBook selectedAddressBook = addressBooks[selectedBook];
                char choice;
                do
                {
                    Console.WriteLine("\nChoose an option:");
                    Console.WriteLine("0 - Back");
                    Console.WriteLine("1 - Get all contacts");
                    Console.WriteLine("2 - Add contacts");
                    Console.WriteLine("3 - Edit contacts");
                    Console.WriteLine("4 - Delete contact");
                    Console.Write("Enter your choice: ");
                    choice = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    switch (choice)
                    {
                        case '1':
                            Console.WriteLine("\nAll Contacts List:");
                            selectedAddressBook.DisplayContacts();
                            break;
                        case '2':
                            selectedAddressBook.AddContact(GetContactDetails());
                            break;
                        case '3':
                            selectedAddressBook.EditContactByName();
                            break;
                        case '4':
                            selectedAddressBook.DeleteContactByName();
                            break;
                        case '0':
                            Console.WriteLine("Going back to main menu...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                } while (choice != '0');
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number.");
            }
        }

        public static Persons GetContactDetails()
        {
            Console.WriteLine("\nAdding New Contact:");
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            return new Persons
            {
                First_name = firstName,
                Last_name = lastName,
                Address = address,
                Number = phoneNumber,
                Email = email
            };
        }

        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Address Book Program....!");
        }
    }
}
