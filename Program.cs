using magazzinoApp.Models;
using System;

namespace magazzinoApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // open excel

            // read worksheets

            // create boxes for every shelf

            Box box1 = new Box(
                productCodes: "12341234",
                sizes: "123",
                location: "AR1B01");

            string html = box1.GenerateHTML();
        }
    }
}