namespace DapperTemplate.Entitites
{
    public class Passport
    {
        public int Id { get; set; }
        public string PassportNumber { get; set; } = null!;
        public int VisitorId { get; set; }

        public override string ToString()
        {
            return $"{PassportNumber}";
        }
    }
}