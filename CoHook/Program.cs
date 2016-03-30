using System;
using CoHook;

namespace COProxy
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Python pythonScript = new Python("Scripts");
            pythonScript.Execute("FlashTaskBar.py");

            Console.ReadKey();
        }
    }
}