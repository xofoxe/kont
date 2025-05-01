using System;
using System.Collections.Generic;

namespace task5
{
    class TextDocumentMemento
    {
        public string Content { get; }

        public TextDocumentMemento(string content)
        {
            Content = content;
        }
    }
    class TextDocument
    {
        private string _content = "";

        public void Write(string text)
        {
            _content += text;
        }

        public string Read()
        {
            return _content;
        }

        public TextDocumentMemento Save()
        {
            return new TextDocumentMemento(_content);
        }

        public void Restore(TextDocumentMemento memento)
        {
            _content = memento.Content;
        }
    }
    class TextEditor
    {
        private TextDocument _document = new TextDocument();
        private Stack<TextDocumentMemento> _history = new Stack<TextDocumentMemento>();

        public void Type(string text)
        {
            _history.Push(_document.Save());
            _document.Write(text);
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                var memento = _history.Pop();
                _document.Restore(memento);
            }
            else
            {
                Console.WriteLine("Немає збережених станів для скасування.");
            }
        }

        public void Print()
        {
            Console.WriteLine(_document.Read());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            TextEditor editor = new TextEditor();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Ввести текст");
                Console.WriteLine("2. Скасувати (Undo)");
                Console.WriteLine("3. Показати документ");
                Console.WriteLine("4. Вийти");
                Console.Write("Виберіть дію: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Введіть текст: ");
                        string input = Console.ReadLine();
                        editor.Type(input);
                        break;

                    case "2":
                        editor.Undo();
                        break;

                    case "3":
                        Console.WriteLine("Вміст документа:");
                        editor.Print();
                        break;

                    case "4":
                        running = false;
                        Console.WriteLine("Завершення програми...");
                        break;

                    default:
                        Console.WriteLine("Невірна команда. Спробуйте ще раз.");
                        break;
                }
            }

        }
    }
}
