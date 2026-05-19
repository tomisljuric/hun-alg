using System;

namespace Madjarski_algoritam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Unesi veličinu matrice n:");
            int n = int.Parse(Console.ReadLine());

            Madjarski mad = new Madjarski();
            mad.n = n;
            mad.cost = new int[n][];
            for(int i=0;i<n;i++)
            {
                mad.cost[i] = new int[n];
            }

            Console.WriteLine("Minimalni trošak je: "+mad.Dodjela());
        }
    }
}
