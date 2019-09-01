using System;

namespace More_TCP_Programming_Server___Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.Start();
        }
    }
}
