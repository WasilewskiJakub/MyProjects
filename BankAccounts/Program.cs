using System;
using System.Collections.Generic;
using System.IO;
namespace ZabwazIO
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>()
            {
                new Person
                {
                    Name = "Jakub", Surname = "Wasilewski", Gender = GenderType.Male, BornDate = Convert.ToDateTime("28/05/2001") }
                ,
                 new Person
                {
                    Name = "Anna", Surname = "Wasilewska", Gender = GenderType.Female, BornDate = Convert.ToDateTime("02/03/1970") }
                ,
                  new Person
                {
                    Name = "Artur", Surname = "Wasilewski", Gender = GenderType.Male, BornDate = Convert.ToDateTime("17/06/1973") }
                ,
                   new Person
                {
                    Name = "Wiktor", Surname = "Zalewski", Gender = GenderType.Male, BornDate = Convert.ToDateTime("22/10/2002") }
                ,
                    new Person
                {
                    Name = "Jan", Surname = "Kowalski", Gender = GenderType.Female, BornDate = Convert.ToDateTime("18/09/2001") }
                ,
                    new Person
                {
                    Name = "Wojtek", Surname = "Żmuda", Gender = GenderType.Male, BornDate = Convert.ToDateTime("19/02/1998") },
                    new Person
                {
                    Name = "Kacper", Surname = "Kucharczk", Gender = GenderType.Male, BornDate = Convert.ToDateTime("20/12/1988") },
                    new Person
                {
                    Name = "Wojtek", Surname = "Wasil", Gender = GenderType.Male, BornDate = Convert.ToDateTime("01/07/2008") },
                    new Person
                {
                    Name = "Kasia", Surname = "Basia", Gender = GenderType.Female, BornDate = Convert.ToDateTime("22/10/1979") },
                    new Person
                {
                    Name = "Jan",Surname="Kowalewski", Gender = GenderType.Male, BornDate = Convert.ToDateTime("15/11/1996") }

            };

            foreach (var pp in people)
                pp.PrepareObj();
            
            BST<Person> tree = new BST<Person>((item1, item2) =>
            {
                return (int)(item2.account.Money.Amount - item1.account.Money.Amount);
                });
            
            foreach(var pp in people)
            {
                tree.Add(pp);
            }
            Console.WriteLine("TREE:");
            foreach (var pp in tree)
            {
                Console.WriteLine(pp.ToString());
            }
            BST<Person>.SerilizeToDirectory(Path.Combine(Directory.GetCurrentDirectory(), "BST"), tree);
            BST<Person> ReadedTree = BST<Person>.DeserilizeDirectory(Path.Combine(Directory.GetCurrentDirectory(), "BST"));
            
            Console.WriteLine("DESERIALIZED TREE");
            foreach (var pp in ReadedTree)
            {
                Console.WriteLine(pp.ToString());
            }
            Console.WriteLine("TEST OF FIND FUNCTION!");
            Person finded;
            try
            {
                finded = ReadedTree.Find((x) => { return 21 - x.Age; });
                Console.WriteLine(finded.ToString());
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            try
            {
                finded = ReadedTree.Find((x) => { return 48 - x.Age; });
                Console.WriteLine(finded.ToString());
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
    }
}
