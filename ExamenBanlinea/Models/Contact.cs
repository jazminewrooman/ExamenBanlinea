using System;
//using SQLite;

namespace ExamenBanlinea.Models
{
    public class Contact
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public string Photo { get; set; }

        public Contact()
        {
        }
    }
}
