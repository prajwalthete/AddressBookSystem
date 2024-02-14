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
    }

    public class AddressBookMain
    {
        public static void Main(string[] args)
        {
            AddressBookMain addressBook = new AddressBookMain();
            addressBook.DisplayWelcomeMessage();
        }

        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Address Book Program....!");
        }
    }
}
