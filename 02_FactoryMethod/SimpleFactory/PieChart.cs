using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    internal class PieChart : Chart
    {
        public PieChart()
        {
            Console.WriteLine("创建饼图");
        }

        public void Display()
        {
            Console.WriteLine("显示饼图");
        }
    }
}
