﻿using BasicContactList.Contracts;
using BasicContactListWithStreamAndCustomExtension;

namespace BasicContactList
{
    public class Menu
    {
        private readonly IContactManager contactManager;

        public Menu()
        {
            contactManager = new ContactManager();
        }

        public void PrintMenu()
        {
            Console.WriteLine("Enter 1 to Add new contact");
            Console.WriteLine("Enter 2 to delete contact");
            Console.WriteLine("Enter 3 to update contact");
            Console.WriteLine("Enter 4 to Search contact");
            Console.WriteLine("Enter 5 to Print all contacts");
            Console.WriteLine("Enter 0 to Exit");
        }
        private void HoldScreen()
        {
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        public void MyMenu()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.Clear();
                    PrintMenu();
                    int option;

                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        switch (option)
                        {
                            case 0:
                                exit = true;
                                break;
                            case 1:
                                Console.Write("Enter contact name: ");
                                var name = Console.ReadLine()!;
                                Console.Write("Enter phone number: ");
                                var phoneNumber = Console.ReadLine()!;
                                Console.Write("Enter email: ");
                                var email = Console.ReadLine()!;
                                string validEmail = Utility.ValidateEmail(email!);
                                var contactTypeInt = Utility.SelectEnum("Select contact type:\n1 Family & Friends\n2 Work Or Business: ", 1, 2);
                                var contactType = (ContactType)contactTypeInt;
                                contactManager.AddContact(name, phoneNumber, validEmail, contactType);
                                break;
                            case 2:
                                Console.Write("Enter phone number of the contact to delete: ");
                                string phone = Console.ReadLine()!;
                                contactManager.DeleteContact(phone);
                                break;
                            case 3:
                                Console.WriteLine("You can edit only your name and email");
                                Console.Write("Enter contact name: ");
                                var nameToEdit = Console.ReadLine()!;
                                Console.WriteLine("Enter phone number to search: ");
                                var phoneToEdit = Console.ReadLine()!;
                                Console.WriteLine("Enter email: ");
                                var emailToEdit = Utility.ValidateEmail(Console.ReadLine()!);
                                contactManager.UpdateContact(phoneToEdit, nameToEdit, emailToEdit);
                                break;
                            case 4:
                                Console.Write("Enter phone number of contact to search:");
                                var search = Console.ReadLine()!;
                                contactManager.GetContact(search);
                                break;
                            case 5:
                                contactManager.GetAllContacts();
                                break;
                            default:
                                Console.WriteLine("Unknown operation!");
                                break;
                        }
                        if (!exit)
                        {
                            HoldScreen();
                        }
                    }
                }


                catch (MyCustomExceptions.ContactDoesNotExistException contactDoesNotExist)
                {

                    Console.WriteLine(contactDoesNotExist.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }

        }


    }
}

