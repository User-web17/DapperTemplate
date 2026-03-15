using Dapper;
using DapperTemplate.Entitites;
using DapperTemplate.Repositories;
using DapperTemplate.Services;
using DapperTemplate.UI;
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

            var visitors = GetAllVisitors(connection);

            foreach (var visitor in visitors)
            {
                Console.WriteLine(visitor);
            }
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
    }
}
