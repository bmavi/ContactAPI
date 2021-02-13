using System;
using System.Collections.Generic;
using System.Text;
using Contact.Models;

namespace Contact.Tests
{
    public class SampleData
    {
        public static IEnumerable<ContactInfo> getContactInfoData()
        {
            return new List<ContactInfo> {
                new ContactInfo()
                {
                    Id = "2865578b-32da-4a8c-8db9-76f41be167d1",
                    FirstName = "Mike",
                    LastName = "Test",
                    Email = "miketest@contact.com",
                    Phone = "1-222-444-5553",
                    Status = "Active"
                    

                },
                new ContactInfo()
                {
                    Id = "2865578b-32da-4a8c-8db9-76f41be167d2",
                    FirstName = "Bill",
                    LastName = "Test",
                    Email = "billtest@contact.com",
                    Phone = "1-433-443-6553",
                    Status = "Active"
                },
                new ContactInfo()
                {
                   Id = "2865578b-32da-4a8c-8db9-76f41be167d3",
                    FirstName = "John",
                    LastName = "Test",
                     Email = "johntest@contact.com",
                    Phone = "1-544-334-6643",
                    Status = "Active"
                }
            };
        }
    }
}
