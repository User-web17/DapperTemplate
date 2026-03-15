using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DapperTemplate.Entitites
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }

        public Author Author { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id} | {Title} | {AuthorId}";
        }
    }
}
