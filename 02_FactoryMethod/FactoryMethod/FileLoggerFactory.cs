using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    //具体产品工厂：文件记录日志工厂类
    public class FileLoggerFactory : LoggerFactory
    {
        public Logger CreateLogger()
        {
            //创建文件业务代码略
            Logger logger = new FileLogger();
            return logger;
        }
    }
}
