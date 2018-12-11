using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public static class Log
    {
        private static StreamWriter _stream_log = new StreamWriter("log.txt");

        public static void ConsoleWrite(string line)
        {
            Console.WriteLine(line);
        }

        public static void FileWrite(string line)
        {
            _stream_log.WriteLine(line);
        }

        public static void Close()
        {
            _stream_log.Close();
        }

        
    }
}
