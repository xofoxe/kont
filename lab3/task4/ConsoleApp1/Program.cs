namespace ConsoleApp1
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public interface ISubject
    {
        char[][] Read(); 
    }

    class SmartTextReader : ISubject
    {
        private string _filePath;

        public SmartTextReader(string filePath)
        {
            _filePath = filePath;
        }
        
        public string GetPath()
        {
            return _filePath;
        }
        
        public char[][] Read()
        {
            string[] lines = File.ReadAllLines(_filePath);
            char[][] result = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return result;
        }
    }

    class SmartTextChecker : ISubject
    {
        private SmartTextReader SmartTextReader;

        public SmartTextChecker(SmartTextReader SmartTextReader)
        {
            this.SmartTextReader = SmartTextReader;
        }

        public char[][] Read()
        {
            Console.WriteLine("Opening file...");
            char[][] result = SmartTextReader.Read();
            Console.WriteLine("File read successfully.");
            Console.WriteLine($"Total lines: {result.Length}");

            int totalChars = 0;
            foreach (var line in result)
            {
                totalChars += line.Length;
            }

            Console.WriteLine($"Total characters: {totalChars}");
            Console.WriteLine("Closing file...");
            return result;
        }
    }

    class SmartTextReaderLocker : ISubject
    {
        private Regex _restrictionPattern;
        private SmartTextReader SmartTextReader;

        public SmartTextReaderLocker(SmartTextReader SmartTextReader, string restrictionPattern)
        { 
            _restrictionPattern = new Regex(restrictionPattern);
            this.SmartTextReader = SmartTextReader;
        }

        public char[][] Read()
        {
            if (_restrictionPattern.IsMatch(SmartTextReader.GetPath()))
            {
                Console.WriteLine("Access denied!");
                return new char[0][];
            }

            return SmartTextReader.Read();
        }
    }

    class Program
    {
        static void Main()
        {
            string filePath = "text.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }
            SmartTextReader SmartTextReader = new SmartTextReader(filePath);

            Console.WriteLine("Using SmartTextChecker:");
            SmartTextChecker checker = new SmartTextChecker(SmartTextReader);
            checker.Read();

            Console.WriteLine("\nUsing SmartTextReaderLocker:");
            SmartTextReaderLocker locker = new SmartTextReaderLocker(SmartTextReader, @"txt");
            locker.Read();
             
        }
    }
}
