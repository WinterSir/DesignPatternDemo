﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    class BarChart : Chart
    {
        public BarChart()
        {
            Console.WriteLine("创建柱状图");
        }

        public void Display()
        {
            Console.WriteLine("显示柱状图");
        }
    }
}
