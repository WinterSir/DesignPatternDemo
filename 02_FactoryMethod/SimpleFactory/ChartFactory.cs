using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    internal class ChartFactory
    {
        public static Chart GetChart(string type)
        {
            Chart chart = null;
            if (type.Equals("bar"))
            {
                chart = new BarChart();
            }
            else if (type.Equals("line"))
            {
                chart = new LineChart();
            }
            else if (type.Equals("pie"))
            {
                chart = new PieChart();
            }
            return chart;
        }
    }
}
