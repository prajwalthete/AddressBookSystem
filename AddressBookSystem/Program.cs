namespace AddressBookApp
{
    public class AddressBookMain
    {
        public static void Main(string[] args)
        {
            AddressBookMain addressBook = new AddressBookMain();
            addressBook.DisplayWelcomeMessage();
        }

        public void DisplayWelcomeMessage()
        {
            Console.WriteLine("Welcome to Address Book Program.....!");

        }
    }
}