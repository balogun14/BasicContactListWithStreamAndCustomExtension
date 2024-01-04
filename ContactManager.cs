using System.Text.Json;
using BasicContactListWithStreamAndCustomExtension;
using ConsoleTables;
using Humanizer;

namespace BasicContactList
{
    internal sealed class ContactManager : IContactManager
    {

        private static List<string> fileContent = ContactStreamReader.ContactReader();
        private static HashSet<Contact> contacts = []; //used to avoid duplication
        private static List<Contact> hashsetList = [];
        public ContactManager()
        {
            UpdateContactHashset();
        }
        private static void UpdateContactHashset()
        {
            contacts.Clear();
            hashsetList.Clear(); //this serves as a gc for the list to remove duplicates and clear ds
            foreach (var item in fileContent)
            {
                if (item != null)
                {
                    var result = Utility.Deserialize(item);
                    contacts.Add(result);
                }
            }

            hashsetList = [.. contacts];
        }
        public void AddContact(string name, string phoneNumber, string? email, ContactType contactType)
        {
            int id = contacts.Count > 0 ? contacts.Count + 1 : 1;
            var isContactExist = IsContactExist(phoneNumber);

            if (isContactExist)
            {
                Console.WriteLine("Contact already exist!");
                return;
            }

            var contact = new Contact
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                ContactType = contactType,
                CreatedAt = DateTime.Now
            };
            // var options = new JsonSerializerOptions{WriteIndented=true};
            string jsonString = JsonSerializer.Serialize<Contact>(contact);
            ContactStreamWriter.ContactWrite(jsonString);
            Console.WriteLine("Contact added successfully.");
            UpdateContactHashset();
        }

        public void DeleteContact(string phoneNumber)
        {
            UpdateContactHashset();
            var contact = FindContact(phoneNumber);
            if (contact is null)
            {
                Console.WriteLine("Unable to delete contact as it does not exist!");
                return;
            }
            int lineToDelete = hashsetList.IndexOf(contact);
            if (lineToDelete >= 0 && lineToDelete < fileContent.Count)
            {
                fileContent.RemoveAt(lineToDelete);
                File.WriteAllLines(".my-contacts/my-contacts.txt", fileContent);
            }
            UpdateContactHashset();
        }

        public Contact? FindContact(string phoneNumber)
        {
            return hashsetList.Find(c => c.PhoneNumber == phoneNumber);
        }

        public void GetContact(string phoneNumber)
        {
            var contact = FindContact(phoneNumber);

            if (contact is null)
            {
                Console.WriteLine($"Contact with {phoneNumber} not found");
            }
            else
            {
                Print(contact);
            }
        }

        public void GetAllContacts()
        {
            // UpdateContactHashset();
            int contactCount = hashsetList.Count;

            Console.WriteLine("You have " + "contact".ToQuantity(contactCount));

            if (contactCount == 0)
            {
                Console.WriteLine("There is no contact added yet.");
                return;
            }

            var table = new ConsoleTable("Id", "Name", "Phone Number", "Email", "Contact Type", "Date Created");

            foreach (var contact in hashsetList)
            {
                table.AddRow(contact.Id, contact.Name, contact.PhoneNumber, contact.Email, ((ContactType)contact.ContactType).Humanize(), contact.CreatedAt.Humanize());
            }

            table.Write(Format.Alternative);
        }

        public void UpdateContact(string phoneNumber, string name, string email)
        {
            var contact = FindContact(phoneNumber) ?? throw new MyCustomExceptions.ContactDoesNotExistException("The contact you searched does not exit", name);
            contact.Name = name;
            contact.Email = email;
            contact.ModifiedAt = DateTime.Now;
            int lineToUpdate = hashsetList.IndexOf(contact);
            if (lineToUpdate >= 0 && lineToUpdate < fileContent.Count)
            {
                string jsonString = JsonSerializer.Serialize<Contact>(contact);
                fileContent.RemoveAt(lineToUpdate);
                fileContent.Insert(lineToUpdate, jsonString);
            }
            File.WriteAllLines(".my-contacts/my-contacts.txt", fileContent);
            UpdateContactHashset();
        }
        private void Print(Contact contact)
        {
            Console.WriteLine($"Name: {contact!.Name}\nPhone Number: {contact!.PhoneNumber}\nEmail: {contact!.Email}");
        }

        private bool IsContactExist(string phoneNumber)
        {
            return contacts.Any(c => c.PhoneNumber == phoneNumber);
        }
    }




}
