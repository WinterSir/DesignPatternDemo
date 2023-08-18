using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    internal class LineChart : Chart
    {
        public LineChart()
        {
            Console.WriteLine("创建折线图");
        }

        public void Display()
        {
            Console.WriteLine("显示折线图");
        }
    }
}
