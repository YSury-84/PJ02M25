using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace PJ02M25.resourses
{
    internal class BusinessLogic
    {
        public int StartMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine();
            Console.WriteLine("1 - Добавить Читателя.");
            Console.WriteLine("2 - Список Читателей.");
            Console.WriteLine("3 - Изменить имя Читателей.");
            Console.WriteLine("4 - Добавить Книгу.");
            Console.WriteLine("5 - Список Книг.");
            Console.WriteLine("6 - Изменить год выпуска книги.");
            Console.WriteLine("7 - Выдать книгу Читателю.");
            Console.WriteLine("8 - Получать список книг определенного жанра и вышедших между определенными годами");
            Console.WriteLine("9 - Получать количество книг определенного автора в библиотеке.");
            Console.WriteLine("10 - Получать количество книг определенного жанра в библиотеке.");
            Console.WriteLine("11 - Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.");
            Console.WriteLine("12 - Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.");
            Console.WriteLine("13 - Получать количество книг на руках у пользователя.");
            Console.WriteLine("14 - Получение последней вышедшей книги.");
            Console.WriteLine("15 - Получение списка всех книг, отсортированного в алфавитном порядке по названию.");
            Console.WriteLine("16 - Получение списка всех книг, отсортированного в порядке убывания года их выхода.");
            Console.WriteLine("0 - Выход из программы.");
            Console.WriteLine();
            try { return Convert.ToInt32(Console.ReadLine()); } catch { return 0; }
        }
        public void Execute(int i) 
        {
            Console.Clear();
            switch (i) 
            {
                case 1:
                    Console.WriteLine("Новый читатель:");
                    Console.WriteLine("Введите имя:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите E-Mail:");
                    string email = Console.ReadLine();
                    if (name == "") name = "NoName";//вместо ошибки
                    using (var dbuser = new UserRepository())
                    {
                        var user = new User { Name = name, Email = email };
                        dbuser.Users.AddRange(user);
                        dbuser.SaveChanges();
                    }
                break;
                case 2:
                    Console.WriteLine("Список читателей:");
                    using (var dbuser = new UserRepository())
                    {
                        var userQerty = from user in dbuser.Users select user;
                        var users = userQerty.ToList();
                        foreach (var user in users)
                            Console.WriteLine(user.Name+" ("+user.Email+")");
                    }
                break;
                case 3:
                    Console.WriteLine("Изменение имени читателя:");
                    Console.WriteLine("Введите старое имя:");
                    string oldname = Console.ReadLine();
                    Console.WriteLine("Введите новое имя:");
                    string newname = Console.ReadLine();
                    using (var dbuser = new UserRepository())
                    {
                        var userQerty = from user in dbuser.Users select user;
                        var users = userQerty.ToList();
                        foreach (var user in users)
                            if (user.Name == oldname)
                                { user.Name = newname; }
                        dbuser.SaveChanges();
                    }
                break;
                case 4:
                    Console.WriteLine("Новая книга:");
                    Console.WriteLine("Введите название:");
                    string title = Console.ReadLine();
                    Console.WriteLine("Введите автора:");
                    string autor = Console.ReadLine();
                    Console.WriteLine("Введите год выпуска:");
                    int year = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите жанр:");
                    string genre = Console.ReadLine();
                    using (var dbbook = new BookRepository())
                    {
                        var book = new Book { Title = title, Autor = autor, Year = year, Genre = genre };
                        dbbook.Books.AddRange(book);
                        dbbook.SaveChanges();
                    }
                break;
                case 5:
                    Console.WriteLine("Список книг:");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books select book;
                        var books = bookQerty.ToList();
                        foreach (var book in books)
                            Console.WriteLine(book.Title + " (" + book.Autor + ","+book.Year+","+book.Genre+","+book.UserID+")");
                    }
                break;
                case 6:
                    Console.WriteLine("Изменение год выпуска книги:");
                    Console.WriteLine("Введите название книги:");
                    title = Console.ReadLine();
                    Console.WriteLine("Введите новый год:");
                    year = Convert.ToInt32(Console.ReadLine());
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books select book;
                        var books = bookQerty.ToList();
                        foreach (var book in books)
                            if (book.Title == title)
                            { book.Year = year; }
                        dbbook.SaveChanges();
                    }
                break;
                case 7:
                    Console.WriteLine("Выдача книги читателю:");
                    Console.WriteLine("Введите название книги:");
                    title = Console.ReadLine();
                    Console.WriteLine("Введите имя читателя:");
                    name = Console.ReadLine();
                    using (var dbbook = new BookRepository())
                    {
                        using (var dbuser = new UserRepository())
                        {
                            var bookQerty = from book in dbbook.Books select book;
                            var books = bookQerty.ToList();
                            var userQerty = from user in dbuser.Users where user.Name == name select user;
                            var users = userQerty.ToList();
                            foreach (var book in books)
                                if (book.Title == title)
                                    foreach (var user in users)
                                        { book.UserID = user.Id; break; }
                            dbbook.SaveChanges();
                        }
                    }
                break;
                case 8:
                    Console.WriteLine("Список книг (8):");
                    using (var dbbook = new BookRepository())
                    {
                        Console.WriteLine("Жанр: Prof выпущенные между 1990 и 2000!");
                        var bookQerty = from book in dbbook.Books where (book.Genre == "Prof") && (book.Year>1990) && (book.Year < 2000) select book;
                        var books = bookQerty.ToList();
                        foreach (var book in books)
                            Console.WriteLine(book.Title + " (" + book.Autor + "," + book.Year + "," + book.Genre + "," + book.UserID + ")");
                    }
                break;
                case 9:
                    Console.WriteLine("Подсчет книг (9):");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books where (book.Autor == "Nemnuygin") select book;
                        Console.WriteLine("В библиотеке книг Немнюгина: "+ bookQerty.Count()+" шт.");
                    }
                break;
                case 10:
                    Console.WriteLine("Подсчет книг (10):");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books where (book.Genre == "Prof") select book;
                        Console.WriteLine("В библиотеке книг Жанра Prof: " + bookQerty.Count() + " шт.");
                    }
                break;
                case 11:
                    Console.WriteLine("Подсчет книг (11):");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books where (book.Autor == "Nemnuygin") && (book.Title == "Turbo Pascal") select book;
                        if (bookQerty.Count() > 0)
                            Console.WriteLine("Да! В библиотеке есть книги(а) Немнюгина TurboPascal.");
                        else
                            Console.WriteLine("Нет - книга не найдена.");
                    }
                break;
                case 12:
                    Console.WriteLine("Подсчет книг (11):");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books where (book.Autor == "Nemnuygin") && (book.Title == "Turbo Pascal") && (book.UserID >=0) select book;
                        if (bookQerty.Count() > 0)
                            Console.WriteLine("Да! Книга Немнюгина TurboPascal - выдана Читателю.");
                        else
                            Console.WriteLine("Нет - книга в библиотеке.");
                    }
                break;
                case 13:
                    Console.WriteLine("Выданные книги читателю:");
                    Console.WriteLine("Введите имя читателя:");
                    name = Console.ReadLine();
                    using (var dbbook = new BookRepository())
                    {
                        using (var dbuser = new UserRepository())
                        {
                            var userQerty = from user in dbuser.Users where user.Name == name select user;
                            var users = userQerty.ToList();
                            if (users.Count() > 0)
                            {
                                var bookQerty = from book in dbbook.Books where book.UserID == users[0].Id select book;
                                var books = bookQerty.ToList();
                                Console.WriteLine("На руах у польователя: " + bookQerty.Count() + " книг, следующие:");
                                foreach (var book in books)
                                    Console.WriteLine(book.Title + " (" + book.Autor + "," + book.Year + "," + book.Genre + "," + book.UserID + ")");
                            }
                        }
                    }
                break;
                case 14:
                    Console.WriteLine("Самая свежая книга:");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books orderby book.Year descending select book;
                        var books = bookQerty.ToList();
                        Console.WriteLine(books[0].Title + " (" + books[0].Autor + "," + books[0].Year + "," + books[0].Genre + "," + books[0].UserID + ")");
                    }
                break;
                case 15:
                    Console.WriteLine("Список книг (в алфавитном порядке):");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books orderby book.Title select book;
                        var books = bookQerty.ToList();
                        foreach (var book in books)
                            Console.WriteLine(book.Title + " (" + book.Autor + "," + book.Year + "," + book.Genre + "," + book.UserID + ")");
                    }
                break;
                case 16:
                    Console.WriteLine("Список книг (в обратном порядке):");
                    using (var dbbook = new BookRepository())
                    {
                        var bookQerty = from book in dbbook.Books orderby book.Title descending select book;
                        var books = bookQerty.ToList();
                        foreach (var book in books)
                            Console.WriteLine(book.Title + " (" + book.Autor + "," + book.Year + "," + book.Genre + "," + book.UserID + ")");
                    }
                break;

            }
        }
    }
}
