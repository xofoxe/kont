using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{ 
    class Logger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LOG: {message}");
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: {message}");
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"WARNING: {message}");
            Console.ResetColor();
        }
    }

    class FileWriter
    {
        private string filePath;

        public FileWriter(string path)
        {
            filePath = path;
        }

        public void Write(string message)
        {
            File.AppendAllText(filePath, message);
        }

        public void WriteLine(string message)
        {
            File.AppendAllText(filePath, message + Environment.NewLine);
        }
    }

    class FileLoggerAdapter : Logger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(string filePath)
        {
            _fileWriter = new FileWriter(filePath);
        }

        public new void Log(string message)
        {
            _fileWriter.WriteLine($"LOG: {message}");
        }

        public new void Error(string message)
        {
            _fileWriter.WriteLine($"ERROR: {message}");
        }

        public new void Warn(string message)
        {
            _fileWriter.WriteLine($"WARNING: {message}");
        }
    }

    class Program
    {
        static void Main()
        {
            Logger consoleLogger = new Logger();
            consoleLogger.Log("Це інформаційне повідомлення.");
            consoleLogger.Error("Це повідомлення про помилку.");
            consoleLogger.Warn("Це попередження.");

            Logger fileLogger = new FileLoggerAdapter("D:\\log.txt");
            fileLogger.Log("Файловий лог: інформаційне повідомлення.");
            fileLogger.Error("Файловий лог: помилка!");
            fileLogger.Warn("Файловий лог: попередження!");

            Console.WriteLine("Логування завершено. Перевірте log.txt");
        }
    }
}
