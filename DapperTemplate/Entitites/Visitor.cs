using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTemplate.Entitites
{
    public class Visitor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Passport passport { get; set; } = null!;

        public override string ToString()
        {
            return $"{Id} | {FullName} | {PhoneNumber} | {BirthDate:d} | {passport.ToString()}";
        }
    }
}
