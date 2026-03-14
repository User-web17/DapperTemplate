using DapperTemplate.Entitites;
using DapperTemplate.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTemplate.UI
{
    public class BookMenu
    {
        private readonly BookService _service;

        public BookMenu(BookService service)
        {
            _service = service;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n===== Library Menu =====");
                Console.WriteLine("1.Add Book");
                Console.WriteLine("2.Show All Books");
                Console.WriteLine("3.Find Book by Id");
                Console.WriteLine("4.Find Books by Title");
                Console.WriteLine("5.Find Books by Author");
                Console.WriteLine("6.Update Book");
                Console.WriteLine("7.Delete Book");
                Console.WriteLine("0.Exit");

                Console.Write("Choose option: ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;

                    case "2":
                        _service.ShowAll();
                        break;

                    case "3":
                        GetById();
                        break;

                    case "4":
                        FindByTitle();
                        break;

                    case "5":
                        FindByAuthor();
                        break;

                    case "6":
                        UpdateBook();
                        break;

                    case "7":
                        DeleteBook();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void AddBook()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter author: ");
            string author = Console.ReadLine()!;

            _service.AddBook(title, author);
        }

        private void GetById()
        {
            Console.Write("Enter book Id: ");
            int id = int.Parse(Console.ReadLine()!);

            _service.GetById(id);
        }

        private void FindByTitle()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine()!;

            _service.FindByTitle(title);
        }

        private void FindByAuthor()
        {
            Console.Write("Enter author: ");
            string author = Console.ReadLine()!;

            _service.FindByAuthor(author);
        }

        private void UpdateBook()
        {
            Console.Write("Enter Id: ");
            int id = int.Parse(Console.ReadLine()!);

            Console.Write("Enter new title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter new author: ");
            string author = Console.ReadLine()!;

            Book book = new Book
            {
                Id = id,
                Title = title,
                Author = author
            };

            _service.Update(book);
        }

        private void DeleteBook()
        {
            Console.Write("Enter Id: ");
            int id = int.Parse(Console.ReadLine()!);

            _service.Delete(id);
        }
    }
}
