using System;

namespace chpt2
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = args[0];
            var qIndex = url.IndexOf('?');
            
            if (qIndex < 0) { // eh
                 return;
            }
            var parameters = url.Remove(0, qIndex + 1).Split('&');
            foreach (var p in parameters) {
                Console.WriteLine(p);
            }
        }
    }
}
