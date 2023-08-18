using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    //具体产品：文件记录日志类
    public class FileLogger : Logger
    {
        public void WriteLog()
        {
            Console.WriteLine("文件记录日志");
        }
    }
}
