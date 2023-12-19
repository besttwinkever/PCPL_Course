using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLabs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle(3, 5);
            rectangle.Print();
            Square square = new Square(6);
            square.Print();
            Circle circle = new Circle(7);
            circle.Print();
            Console.ReadKey();

        }
    }
}
