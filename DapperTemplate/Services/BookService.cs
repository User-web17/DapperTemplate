using DapperTemplate.Entitites;
using DapperTemplate.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace DapperTemplate.Services
{
    public class BookService
    {
        private readonly IBookRepository _repo;

        public BookService(IBookRepository repo)
        {
            _repo = repo;
        }

        public void AddBook(string title, string author)
        {
            _repo.Add(new Book { Title = title, Author = author });
        }

        public void ShowAll()
        {
            var books = _repo.GetAll();

            foreach (var book in books)
                Console.WriteLine($"{book.Id}: {book.Title} - {book.Author}");
        }

        public void GetById(int id)
        {
            var book = _repo.GetById(id);

            if (book == null)
                Console.WriteLine("Book not found");
            else
                Console.WriteLine(book.ToString());
        }

        public void FindByTitle(string title)
        {
            var books = _repo.FindByTitle(title);

            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void FindByAuthor(string author)
        {
            var books = _repo.FindByAuthor(author);

            foreach (Book book in books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public void Update(Book book)
        {
            _repo.Update(book);
            Console.WriteLine(book.ToString());
        }

        public void Delete(int id)
        {
            var book = _repo.GetById(id);

            if (book == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            Console.WriteLine(book);
            _repo.Delete(id);
        }
    }
}
