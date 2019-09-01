using System;
using System.IO;
using System.Net.Sockets;

namespace Assignment_A___Simple_Math___Client
{
    internal class Worker
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Client");

            using (TcpClient socket = new TcpClient("localhost", 3001))
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string myLine = Console.ReadLine();
                    sw.WriteLine(myLine);
                    sw.Flush();

                    Console.ForegroundColor = ConsoleColor.White;
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }
    }
}