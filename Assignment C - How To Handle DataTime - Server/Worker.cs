using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Assignment_C___How_To_Handle_DataTime___Server
{
    internal class Worker
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Server");

            TcpListener server = new TcpListener(IPAddress.Loopback, 3003);
            server.Start();

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }
        }

        private void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                    Console.WriteLine("Calc...");

                    string[] numbers = line.Split(' ');

                    try
                    {
                        DateTime result = DateTime.ParseExact(numbers[0] + " " + numbers[1], "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        Console.WriteLine("Valid");

                        sw.WriteLine("Valid");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);

                        Console.WriteLine("Not Valid");

                        sw.WriteLine("Not Valid");
                    }

                    Console.WriteLine("Result send...");

                    sw.Flush();
                }
            }
        }
    }
}