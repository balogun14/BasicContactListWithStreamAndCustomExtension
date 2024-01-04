using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicContactListWithStreamAndCustomExtension
{
    public class MyCustomExceptions
    {
        public class ContactDoesNotExistException : Exception
        {
            public string ContactName { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public ContactDoesNotExistException()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            {

            }
            public ContactDoesNotExistException(string message, string name) : base(message)
            {
                ContactName = name;
            }
        }
    }
}