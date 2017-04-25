using System;
using DbCreation.Data;

namespace DbCreation
{
    class Program
    {
        static void Main(string[] args)
        {
            new DataBaseGenerator(new DatabaseConnection(), new MySqDataBaseUrls()).Regenerate();
            Console.WriteLine("done (Enter to exit)");
            Console.ReadLine();
        }
    }
}
