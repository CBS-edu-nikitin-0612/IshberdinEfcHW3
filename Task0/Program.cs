using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Task0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using Context db = new Context();
            {
                Country rus = new Country()
                {
                    Name = "Russia",
                    Citys = new List<City>()
                    {
                        new City() { Name = "Moscow" },
                        new City() { Name = "SPB" }
                    }
                };
                Country usa = new Country()
                {
                    Name = "USA",
                    Citys = new List<City>()
                    {
                        new City() { Name = "New York" },
                        new City() { Name = "Los Santos" }
                    }
                };
                db.AddRange(rus, usa);
                db.SaveChanges();

                foreach (var country in db.Countries.Include(c=>c.Citys))
                {
                    Console.WriteLine($"{country.Name}:");
                    foreach (var city in country.Citys)
                    {
                        Console.WriteLine(city.Name);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.ReadKey();
                db.Database.EnsureDeleted();
            }
        }
    }


    public class Country
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20), MinLength(2)]
        [Required]
        public string Name { get; set; }
        public List<City> Citys { get; set; }
        public Country()
        {
            Citys = new List<City>();
        }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
