using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace More_TCP_Programming_Server___Client
{
    internal class Worker
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Server");

            TcpListener server = new TcpListener(IPAddress.Loopback, 3001);
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

                    int result;
                    switch (numbers[1])
                    {
                        case "+":
                            result = Add(int.Parse(numbers[0]), int.Parse(numbers[2]));
                            break;
                        case "-":
                            result = Sub(int.Parse(numbers[0]), int.Parse(numbers[2]));
                            break;
                        case "*":
                            result = Mul(int.Parse(numbers[0]), int.Parse(numbers[2]));
                            break;
                        case "/":
                            result = Div(int.Parse(numbers[0]), int.Parse(numbers[2]));
                            break;
                        default:
                            result = 404;
                            break;
                    }

                    Console.WriteLine("= " + result);

                    sw.WriteLine("= " + result);

                    Console.WriteLine("Result send...");

                    sw.Flush();
                }
            }
        }

        private int Add(int a, int b)
        {
            return a + b;
        }

        private int Sub(int a, int b)
        {
            return a - b;
        }

        private int Mul(int a, int b)
        {
            return a * b;
        }

        private int Div(int a, int b)
        {
            return a / b;
        }
    }
}