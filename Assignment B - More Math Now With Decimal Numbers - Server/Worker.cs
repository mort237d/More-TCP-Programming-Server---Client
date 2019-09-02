using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Assignment_B___More_Math_Now_With_Decimal_Numbers___Server
{
    internal class Worker
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Server");

            TcpListener server = new TcpListener(IPAddress.Loopback, 3002);
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

                    double result;
                    switch (numbers[1])
                    {
                        case "+":
                            result = Add(double.Parse(numbers[0], new CultureInfo("en-UK")), double.Parse(numbers[2], new CultureInfo("en-UK")));
                            break;
                        case "-":
                            result = Sub(double.Parse(numbers[0], new CultureInfo("en-UK")), double.Parse(numbers[2], new CultureInfo("en-UK")));
                            break;
                        case "*":
                            result = Mul(double.Parse(numbers[0], new CultureInfo("en-UK")), double.Parse(numbers[2], new CultureInfo("en-UK")));
                            break;
                        case "/":
                            result = Div(double.Parse(numbers[0], new CultureInfo("en-UK")), double.Parse(numbers[2], new CultureInfo("en-UK")));
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

        private double Add(double a, double b)
        {
            return a + b;
        }

        private double Sub(double a, double b)
        {
            return a - b;
        }

        private double Mul(double a, double b)
        {
            return a * b;
        }

        private double Div(double a, double b)
        {
            return a / b;
        }
    }
}