using Newtonsoft.Json;

namespace AddressBookApp
{
    public class Person
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Person other = (Person)obj;
            return First_name.Equals(other.First_name, StringComparison.OrdinalIgnoreCase) &&
                   Last_name.Equals(other.Last_name, StringComparison.OrdinalIgnoreCase) &&
                   Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First_name, Last_name, Number, Email, Address);
        }
    }

    public class AddressBook
    {
        public List<Person> Contacts { get; set; }

        public AddressBook()
        {
            Contacts = new List<Person>();
        }

        public bool AddContact(Person person)
        {
            if (ContactExists(person))
            {
                Console.WriteLine("Contact with the same email or phone number already exists. Please enter unique email and phone.");
                return false;
            }
            Contacts.Add(person);
            SaveToFile("address_book_data.json"); // Save changes to file
            return true;
        }

        private bool ContactExists(Person person)
        {
            return Contacts.Any(c => c.Email.Equals(person.Email, StringComparison.OrdinalIgnoreCase) ||
                                      c.Number.Equals(person.Number, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayContacts()
        {
            if (Contacts.Count > 0)
            {
                foreach (var person in Contacts)
                {
                    Console.WriteLine(person);
                }
            }
            else
            {
                Console.WriteLine("Your Contact list is empty");
            }
        }

        public void EditContactByName()
        {
            Console.WriteLine("\nEditing Contact:");
            Console.Write("Enter First Name of Contact to Edit: ");
            string editFirstName = Console.ReadLine();

            Console.Write("Enter Last Name of Contact to Edit: ");
            string editLastName = Console.ReadLine();

            Person personToEdit = FindContactByName(editFirstName, editLastName);
            if (personToEdit != null)
            {
                Console.WriteLine("Select what you want to update:");
                Console.WriteLine("1. First Name");
                Console.WriteLine("2. Last Name");
                Console.WriteLine("3. Address");
                Console.WriteLine("4. Phone Number");
                Console.WriteLine("5. Email");

                Console.Write("Enter your choice: ");
                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        Console.Write("Enter new First Name: ");
                        string newFirstName = Console.ReadLine();
                        personToEdit.First_name = newFirstName;
                        break;
                    case '2':
                        Console.Write("Enter new Last Name: ");
                        string newLastName = Console.ReadLine();
                        personToEdit.Last_name = newLastName;
                        break;
                    case '3':
                        Console.Write("Enter new Address: ");
                        string newAddress = Console.ReadLine();
                        personToEdit.Address = newAddress;
                        break;
                    case '4':
                        do
                        {
                            Console.Write("Enter new Phone Number: ");
                            string newPhoneNumber = Console.ReadLine();

                            if (!IsValidPhoneNumber(newPhoneNumber))
                            {
                                Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
                            }
                            else if (Contacts.Any(c => c.Number.Equals(newPhoneNumber, StringComparison.OrdinalIgnoreCase) && c != personToEdit))
                            {
                                Console.WriteLine("A contact with this phone number already exists. Please enter a unique phone number.");
                            }
                            else
                            {
                                personToEdit.Number = newPhoneNumber;
                            }
                        } while (!IsValidPhoneNumber(personToEdit.Number));
                        break;
                    case '5':
                        Console.Write("Enter new Email: ");
                        string newEmail = Console.ReadLine();
                        if (!IsValidEmail(newEmail))
                        {
                            Console.WriteLine("Invalid email format. Please enter a valid email address.");
                        }
                        else if (Contacts.Any(c => c.Email.Equals(newEmail, StringComparison.OrdinalIgnoreCase) && c != personToEdit))
                        {
                            Console.WriteLine("A contact with this email already exists. Please enter a new email.");
                        }
                        else
                        {
                            personToEdit.Email = newEmail;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No updates performed.");
                        return;
                }

                SaveToFile("address_book_data.json");

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

            Person personToDelete = FindContactByName(deleteFirstName, deleteLastName);
            if (personToDelete != null)
            {
                Contacts.Remove(personToDelete);
                Console.WriteLine("Contact deleted successfully.");
                SaveToFile("address_book_data.json"); // Save changes to file
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        public void SaveToFile(string fileName)
        {
            string json = JsonConvert.SerializeObject(Contacts);
            File.WriteAllText(fileName, json);
        }

        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                Contacts = JsonConvert.DeserializeObject<List<Person>>(json) ?? new List<Person>();
            }
        }

        private Person FindContactByName(string firstName, string lastName)
        {
            return Contacts.Find(c => c.First_name.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                      c.Last_name.Equals(lastName, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null) return false;
            string pattern = @"^\d{10}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }

        private static bool IsValidEmail(string email)
        {
            if (email == null) return false;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
    }

    public class AddressBookMain
    {
        private static List<(string name, AddressBook book)> addressBooks = new List<(string name, AddressBook book)>();
        private const string dataFile = "address_book_data.json";

        public static void Main(string[] args)
        {
            LoadAddressBookData();
            AddressBookMain addressBook = new AddressBookMain();
            addressBook.DisplayWelcomeMessage();

            char choice;
            do
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Add new Address Book");
                Console.WriteLine("2 - Select Address Book");
                Console.WriteLine("3 - Delete Address Book");
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
                    case '3':
                        DeleteAddressBook();
                        break;
                    case '0':
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            } while (choice != '0');

            SaveAddressBookData();
        }

        private static void LoadAddressBookData()
        {
            if (File.Exists(dataFile))
            {
                string json = File.ReadAllText(dataFile);
                var addressBookData = JsonConvert.DeserializeObject<List<(string name, List<Person> contacts)>>(json);
                if (addressBookData != null)
                {
                    foreach (var (name, contacts) in addressBookData)
                    {
                        var addressBook = new AddressBook { Contacts = contacts };
                        addressBooks.Add((name, addressBook));
                    }
                }
            }
        }

        private static void SaveAddressBookData()
        {
            var dataToSave = new List<(string name, List<Person> contacts)>();
            foreach (var (name, book) in addressBooks)
            {
                dataToSave.Add((name, book.Contacts));
            }
            string json = JsonConvert.SerializeObject(dataToSave);
            File.WriteAllText(dataFile, json);
        }

        private static void AddAddressBook()
        {
            Console.Write("Enter Name for the new Address Book: ");
            string name = Console.ReadLine();
            var newAddressBook = new AddressBook();
            addressBooks.Add((name, newAddressBook));
            Console.WriteLine($"Address Book '{name}' added successfully.");
        }

        private static void SelectAddressBook()
        {
            Console.WriteLine("\nSelect Address Book:");
            int count = 1;
            foreach (var (name, _) in addressBooks)
            {
                Console.WriteLine($"{count}. {name}");
                count++;
            }

            Console.Write("Enter the number for the Address Book to select: ");
            int index;
            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= addressBooks.Count)
            {
                string selectedBook = addressBooks[index - 1].name;
                Console.WriteLine($"\nSelected Address Book: {selectedBook}");

                AddressBook selectedAddressBook = addressBooks[index - 1].book;
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

        private static void DeleteAddressBook()
        {
            Console.WriteLine("\nDelete Address Book:");
            int count = 1;
            foreach (var (name, _) in addressBooks)
            {
                Console.WriteLine($"{count}. {name}");
                count++;
            }

            Console.Write("Enter the number for the Address Book to delete: ");
            int index;
            if (int.TryParse(Console.ReadLine(), out index) && index > 0 && index <= addressBooks.Count)
            {
                string deletedBookName = addressBooks[index - 1].name;
                addressBooks.RemoveAt(index - 1);
                Console.WriteLine($"\nAddress Book '{deletedBookName}' deleted successfully.");
                SaveAddressBookData(); // Save changes to file
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid number.");
            }
        }

        private static Person GetContactDetails()
        {
            Console.WriteLine("\nAdding New Contact:");

            string firstName;
            do
            {
                Console.Write("Enter First Name: ");
                firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName))
                {
                    Console.WriteLine("First Name cannot be empty. Please enter a valid First Name.");
                }
            } while (string.IsNullOrWhiteSpace(firstName));

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            string phoneNumber;
            do
            {
                Console.Write("Enter Phone Number: ");
                phoneNumber = Console.ReadLine();

                if (!IsValidPhoneNumber(phoneNumber))
                {
                    Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
                    phoneNumber = null; // Reset phoneNumber to null to repeat the loop
                }
            } while (string.IsNullOrWhiteSpace(phoneNumber));

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            if (!IsValidEmail(email))
            {
                Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return new Person();
            }

            return new Person
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

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null) return false;
            string pattern = @"^\d{10}$";
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }

        private static bool IsValidEmail(string email)
        {
            if (email == null) return false;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }
    }
}
