using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book) => books.Add(book);

        public void RemoveBook(Book book) => books.Remove(book);

        public List<Book> GetAvailableBooks() => books.Where(b => b.IsAvailable).ToList();

        public List<Book> GetCheckedOutBooks() => books.Where(b => !b.IsAvailable).ToList();

        public List<Book> GetBooksByGenre(string genre) => books.Where(b => b.Genres.Contains(genre)).ToList();

        public List<Book> GetBooksByAuthor(string author) => books.Where(b => b.Authors.Contains(author)).ToList();

        public List<Book> GetBooksByYear(int year) => books.Where(b => b.Year == year).ToList();

        public Book GetBookInfo(string title) => books.FirstOrDefault(b => b.Title == title);
    }
}
