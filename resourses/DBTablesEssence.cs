using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Описание сущеостей таблиц СУБД
namespace PJ02M25.resourses
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Autor { get; set; }
        public string Genre { get; set; }
        public int UserID { get; set; }

    }
}
