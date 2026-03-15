using Dapper;
using DapperTemplate.Entitites;
using Microsoft.Data.SqlClient;

namespace DapperTemplate.Program
{
    public class Program
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=DapperLibrary;
            Integrated Security=True;
            TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //Visitor visitor = new Visitor()
            //{
            //    FullName = "Anton Pivko",
            //    PhoneNumber = "0506789990",
            //    BirthDate = new DateTime(1999, 10, 10),
            //    passport = new Passport()
            //    {
            //        PassportNumber = "111111111"
            //    }
            //};

            //AddVisitorWithPassport(connection, visitor);

            //var visitors = GetAllVisitors(connection);

            //foreach (var visitor in visitors)
            //{
            //    Console.WriteLine(visitor);
            //}

            //Author author = new Author()
            //{
            //    FullName = "Lina Kostenko",
            //    BirthDate = new DateTime(1930, 3, 19),
            //    Books = new List<Book>()
            //    {
            //        new Book{Title = "Ping-Pong"},
            //        new Book{Title = "Out There Are Planets Far Beyond Our View"},
            //        new Book{Title = "How Bitter Is the Wine"}
            //    }
            //};

            //AddAuthorWithBooks(connection, author);

            //var authors = GetAllAuthors(connection);

            //foreach (var author in authors)
            //{
            //    Console.WriteLine(author.ToString());
            //}


        }

        public static void AddVisitorWithPassport(SqlConnection conn, Visitor visitor)
        {
            string insertVisitor = @"
                    INSERT INTO Visitors (FullName, PhoneNumber, BirthDate)
                    OUTPUT Inserted.Id
                    VALUES (@FullName, @PhoneNumber, @BirthDate);
                ";

            int visitorId = conn.ExecuteScalar<int>(insertVisitor, visitor);

            Console.WriteLine($"[LOG] Visitor inserted. Id: {visitorId}.");

            string insertPassport = @"
                    INSERT INTO Passports (PassportNumber, VisitorId)
                    OUTPUT Inserted.Id
                    VALUES (@PassportNumber, @VisitorId)
                ";

            visitor.passport.VisitorId = visitorId;

            int passportId = conn.ExecuteScalar<int>(insertPassport, visitor.passport);

            Console.WriteLine($"[LOG] Passport inserted. Id: {passportId}.");
        }

        public static List<Visitor> GetAllVisitors(SqlConnection conn)
        {
            string query = @"
                SELECT V.Id, V.FullName, V.PhoneNumber, V.BirthDate, P.Id, P.PassportNumber, P.VisitorId
                FROM Visitors V
                JOIN Passports P ON V.Id = P.VisitorId;
            ";

            // І тип параметр - І таблиця у JOIN
            // II тип параметр - II таблиця у JOIN
            // III тип параметр - тип даних результату
            var result = conn.Query<Visitor, Passport, Visitor>(query,
                (v, p) =>
                {
                    v.passport = p;
                    return v;
                },
                splitOn: "Id"
                );

            return result.ToList();
        }

        public static List<Author> GetAllAuthors(SqlConnection conn)
        {
            string query = @"
                    SELECT A.Id, A.FullName, A.BirthDate, B.Id, B.Title, B.AuthorId
                    FROM Authors A 
                    LEFT JOIN Books B ON A.Id = B.AuthorId 
                ";

            var authorsMap = new Dictionary<int, Author>();

            var result = conn.Query<Author, Book, Author>(query,
                (a, b) =>
                {
                    if (!authorsMap.TryGetValue(a.Id, out Author author))
                    {
                        author = a;
                        author.Books = new List<Book>();
                        authorsMap.Add(a.Id, author);
                    }

                    if (b != null)
                    {
                        author.Books.Add(b);
                    }

                    return author;
                },
                splitOn: "Id"
                ).Distinct();

            return result.ToList();
        }

        public static void AddAuthorWithBooks(SqlConnection conn, Author author)
        {
            var transaction = conn.BeginTransaction();

            string insertAuthor = @"
                    INSERT INTO Authors (FullName, BirthDate)
                    OUTPUT Inserted.Id
                    VALUES (@FullName, @BirthDate)
                ";

            int authorId = conn.ExecuteScalar<int>(insertAuthor, author, transaction);

            for (int i = 0; i < author.Books.Count; i++)
            {
                author.Books[i].AuthorId = authorId;
            }

            string insertBooks = @"
                    INSERT INTO Books (Title, AuthorId)
                    VALUES (@Title, @AuthorId)
                ";

            conn.Execute(insertBooks, author.Books, transaction);

            transaction.Commit();
        }
    }
}
