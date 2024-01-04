using System.Text.Json;
using BasicContactList;

namespace BasicContactListWithStreamAndCustomExtension
{
    public class ContactStreamWriter
    {
        static readonly string directoryName = ".my-contacts";
        static readonly string file = "my-contacts.txt";
        static readonly string fullpath = directoryName + "/" + file;
        public static void RewriteContact(List<Contact> data)
        {
            File.Delete(fullpath);
            StreamWriter streamWriter = new StreamWriter(fullpath, true);
            using (streamWriter)
            {
                foreach (var item in data)
                {
                    streamWriter.WriteLine(item);
                    
                }
            }
        }
        public static void ContactWrite(string data)
        {

            try
            {
                // DirectoryInfo directory = Directory.CreateDirectory(directoryName);
                StreamWriter streamWriter = new StreamWriter(fullpath, true);

                using (streamWriter)
                {
                    streamWriter.WriteLine(data);

                }
            }
            catch (DirectoryNotFoundException directoryNot)
            {

                Console.WriteLine(directoryNot.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}