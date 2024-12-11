using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Usporedba_dva_XML_dokumenta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XElement prvi = XElement.Load("prvi.xml");
            XElement drugi = XElement.Load("drugi.xml");

            var prviBooks = prvi.Descendants("book")
                .Select(b => new
                {
                    Id = (string)b.Attribute("id"),
                    Image = (string)b.Attribute("image"),
                    Name = (string)b.Attribute("name")
                }).ToList();

            var drugiBooks = drugi.Descendants("book")
                .Select(b => new
                {
                    Id = (string)b.Attribute("id"),
                    Image = (string)b.Attribute("image"),
                    Name = (string)b.Attribute("name")
                }).ToList();

            var differentBooks = prviBooks
                .Union(drugiBooks)
                .GroupBy(b => b.Id)
                .Where(g => g.Count() == 1 || g.First().Image != g.Last().Image || g.First().Name != g.Last().Name)
                .SelectMany(g => g)
                .ToList();

            if (differentBooks.Any())
            {
                Console.WriteLine("Razlike između XML dokumenata:");
                foreach (var book in differentBooks)
                {
                    Console.WriteLine($"ID: {book.Id}, Image: {book.Image}, Name: {book.Name}");
                }
            }
            else
            {
                Console.WriteLine("Nema razlika između XML dokumenata.");
            }

            Console.ReadKey();
        }
    }
}