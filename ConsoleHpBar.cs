using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProperties
{
    public class ConsoleHpBar
    {
        public RangeInt hp;

        public ConsoleHpBar(int hpMax)
        {
            hp = new RangeInt(hpMax);
        }

        public void Print()
        {
            for (int i = hp.Min; i <= hp.Max; i++)
            {
                if (i <= hp.Current - 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("O");
                }
                else if (i > hp.Current - 3 && i <= hp.Current)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("O");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X");
                }
            }

            Console.WriteLine();
        }
    }
}
