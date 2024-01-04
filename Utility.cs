using System.Text.Json;

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
        public static Contact Deserialize(string data){
            Contact contact = JsonSerializer.Deserialize<Contact>(data)!;
            return contact;
        }
    }
}