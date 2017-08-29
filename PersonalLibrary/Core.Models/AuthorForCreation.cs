using System;
using System.Collections.Generic;
using System.Text;

// our incoming data model for creating a author
// allows for flexibility in design, and creation validation

namespace Core.Models
{
    public class AuthorForCreation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Genre { get; set; }

        // intilize an empty list to protect against a null reference exception
        public ICollection<BookForCreation> Books { get; set; }
        = new List<BookForCreation>();
    }
}
