using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Book
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Genres { get; set; }
        public int Year { get; set; }
        public int TimesCheckedOut { get; private set; }
        public bool IsAvailable { get; private set; } = true;

        public Book(string title, List<string> authors, List<string> genres, int year)
        {
            Title = title;
            Authors = authors;
            Genres = genres;
            Year = year;
        }

        public void Checkout()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                TimesCheckedOut++;
            }
            else
            {
                Console.WriteLine("Book is not available.");
            }
        }

        public void Return()
        {
            IsAvailable = true;
        }
    }
}
