using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    using System;

    interface IRenderer
    {
        void Render(string shape);
    }

    class VectorRenderer : IRenderer
    {
        public void Render(string shape)
        {
            Console.WriteLine($"Drawing {shape} as vectors");
        }
    }

    class RasterRenderer : IRenderer
    {
        public void Render(string shape)
        {
            Console.WriteLine($"Drawing {shape} as pixels");
        }
    }

    abstract class Shape
    {
        protected IRenderer _renderer;

        public Shape(IRenderer renderer)
        {
            _renderer = renderer;
        }

        public abstract void Draw();
    }

    class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer) { }
        public override void Draw()
        {
            _renderer.Render("Circle");
        }
    }

    class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) { }
        public override void Draw()
        {
            _renderer.Render("Square");
        }
    }

    class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer) { }
        public override void Draw()
        {
            _renderer.Render("Triangle");
        }
    }

    class Program
    {
        static void Main()
        {
            IRenderer vectorRenderer = new VectorRenderer();
            IRenderer rasterRenderer = new RasterRenderer();

            Shape vectorCircle = new Circle(vectorRenderer);
            Shape rasterSquare = new Square(rasterRenderer);
            Shape vectorTriangle = new Triangle(vectorRenderer);

            vectorCircle.Draw();
            rasterSquare.Draw();
            vectorTriangle.Draw();
        }
    } 
}
