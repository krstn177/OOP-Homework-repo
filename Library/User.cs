using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class User
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public List<Book> CheckedOutBooks { get; private set; } = new List<Book>();
        public List<Book> ReturnedBooks { get; private set; } = new List<Book>();

        public int RetentionPeriod => Type switch
        {
            "Regular" => 30,
            "Special" => 35,
            "Gold" => 40,
            _ => 30
        };

        public User(string firstName, string middleName, string lastName, string type)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Type = type;
        }

        public void CheckoutBook(Book book)
        {
            if (book.IsAvailable)
            {
                book.Checkout();
                CheckedOutBooks.Add(book);
            }
        }

        public void ReturnBook(Book book)
        {
            if (CheckedOutBooks.Contains(book))
            {
                book.Return();
                CheckedOutBooks.Remove(book);
                ReturnedBooks.Add(book);
            }
        }
    }
}
