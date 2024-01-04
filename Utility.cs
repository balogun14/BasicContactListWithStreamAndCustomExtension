using System.Text.Json;
using System.Text.RegularExpressions;

namespace BasicContactList
{
    public static class Utility
    {
        public static int SelectEnum(string label, int start, int end)
        {
            int outValue;

            do
            {
                Console.Write(label);
            }
            while (!(int.TryParse(Console.ReadLine(), out outValue) && IsRangeValid(outValue, start, end)));

            return outValue;
        }

        public static bool IsRangeValid(int value, int start, int end)
        {
            return value >= start && value <= end;
        }
        public static Contact Deserialize(string data)
        {
            Contact contact = JsonSerializer.Deserialize<Contact>(data)!;
            return contact;
        }
        public static bool Validator(string value)
        {
            string pattern = @"[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-z0-9]+\.[a-zA-Z]+";
            var match = Regex.IsMatch(value, pattern);
            return match;
        }
        public static string ValidateEmail(string email)
        {

            bool isEmail = Validator(email!);
            if (isEmail != true)
            {
                do
                {
                    Console.Write("Enter a valid email: ");
                    email = Console.ReadLine()!;
                    isEmail = Validator(email!);
                } while (isEmail != true);
            }
            return email;
        }
    }
}