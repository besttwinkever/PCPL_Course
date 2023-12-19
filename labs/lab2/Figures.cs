using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpLabs
{
    abstract class Figure : IPrint
    {
        public abstract float CalculateSquare();

        public override string ToString()
        {
            return "Неизвестная геометрическая фигура";
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }

    class Rectangle : Figure
    {
        protected float width { get; set; }
        protected float height { get; set; }

        public Rectangle(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        public override float CalculateSquare()
        {
            return width * height;
        }

        public override string ToString()
        {
            return String.Format("Прямоугольник. Ширина: {0}, высота: {1}, площадь: {2}", width, height, CalculateSquare());
        }

    }

    class Square : Rectangle
    {
        public Square(float length) : base(length, length)
        {
            this.width = length;
            this.height = length;
        }

        public override string ToString()
        {
            return String.Format("Квадрат. Длина стороны: {0}, площадь: {1}", width, CalculateSquare());
        }
    }

    class Circle : Figure
    {
        private float radius { get; set; }

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public override float CalculateSquare()
        {
            return (float)Math.PI * radius * radius;
        }

        public override string ToString()
        {
            return String.Format("Круг. Радиус: {0}, площадь: {1}", radius, CalculateSquare());
        }

    }
}
