using Dapper;
using DapperTemplate.Entitites;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTemplate.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly SqlConnection Sqlconnection;

        public BookRepository(SqlConnection connection)
        {
            Sqlconnection = connection;
        }

        public void Add(Book book)
        {
            string AddBook = "INSERT INTO Books (Title, Author) VALUES (@Title, @Author)";
            Sqlconnection.Execute(AddBook, book);
        }

        public List<Book> GetAll()
        {
            return Sqlconnection.Query<Book>("SELECT * FROM Books").ToList();
        }

        public Book GetById(int id)
        {
            return Sqlconnection.QueryFirstOrDefault<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = id })!;
        }

        public List<Book> FindByTitle(string title)
        {
            return Sqlconnection.Query<Book>(
                "SELECT * FROM Books WHERE Title LIKE @Title",
                new { Title = $"%{title}%" }).ToList();
        }

        public List<Book> FindByAuthor(string author)
        {
            return Sqlconnection.Query<Book>(
                "SELECT * FROM Books WHERE Author LIKE @Author",
                new { Author = $"%{author}%" }).ToList();
        }

        public void Update(Book book)
        {
            string updateBooksQuery = "UPDATE Books SET Title = @Title, Author = @Author WHERE Id = @Id";
            Sqlconnection.Execute(updateBooksQuery, new { Title = book.Title, Author = book.Author, Id = book.Id });
        }

        public void Delete(int id)
        {
            Sqlconnection.Execute("DELETE FROM Books WHERE Id = @Id", new { Id = id });
        }
    }
}
