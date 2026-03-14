using DapperTemplate.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTemplate.Repositories
{
    public interface IBookRepository
    {
        void Add(Book book);
        List<Book> GetAll();
        Book GetById(int id);
        List<Book> FindByTitle(string title);
        List<Book> FindByAuthor(string author);
        void Update(Book book);
        void Delete(int id);
    }
}
