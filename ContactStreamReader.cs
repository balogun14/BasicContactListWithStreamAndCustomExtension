using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicContactListWithStreamAndCustomExtension
{
    public class ContactStreamReader
    {
        public static List<string> ContactReader()
        {
            string directoryName = ".my-contacts";
            string file = "my-contacts.txt";
            string fullpath = directoryName + "/" + file;
            var fileContent = new List<string>();
            try
            {
                var streamReader = new StreamReader(fullpath);

                using (streamReader)
                {
                    string line = streamReader.ReadLine()!;
                    while (line != null)
                    {
                        fileContent.Add(line);
                        line = streamReader.ReadLine()!;
                    }
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

            return fileContent;
        }
    }
}