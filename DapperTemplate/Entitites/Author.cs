namespace DapperTemplate.Entitites
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public List<Book> Books { get; set; } = null!;
        public override string ToString()
        {
            return $"{Id} | {FullName} | {BirthDate:d} | Books count: {Books.Count}";
        }
    }
}