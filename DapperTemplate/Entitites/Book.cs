using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTemplate.Entitites
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }

        public override string ToString()
        {
            return $"{Title} | {AuthorId}";
        }
    }
}
