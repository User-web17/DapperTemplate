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
            Initial Catalog=DapperDemo;
            Integrated Security=True;
            TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            var connection = new SqlConnection(connectionString);

            IBookRepository repo = new BookRepository(connection);
            BookService service = new BookService(repo);

            BookMenu menu = new BookMenu(service);
            menu.Start();
        }

        public static void CreateBooksTable(SqlConnection connection)
        {
            string sql = @"
                IF NOT EXISTS (
                    SELECT * FROM sys.tables WHERE name = 'Books'
                )
                BEGIN
                    CREATE TABLE Books (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Title NVARCHAR(200) NOT NULL,
                        Author NVARCHAR(200) NOT NULL
                    );
                END";

            connection.Execute(sql);
        }
    }
}
