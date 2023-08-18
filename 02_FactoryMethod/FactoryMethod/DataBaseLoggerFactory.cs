using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_FactoryMethod
{
    //具体产品工厂：数据库记录日志工厂类
    public class DataBaseLoggerFactory : LoggerFactory
    {
        public Logger CreateLogger()
        {
            //创建数据库业务代码略
            Logger logger = new DataBaseLogger();
            return logger;
        }
    }
}
