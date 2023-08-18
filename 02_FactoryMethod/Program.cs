using _02_FactoryMethod;

//工厂方法模式

#region 简单工厂
Console.WriteLine("\n**********简单工厂**********");
Chart chart = ChartFactory.GetChart("bar");
chart.Display();
#endregion

#region 工厂方法
//Console.WriteLine("\n**********工厂方法**********");
//LoggerFactory loggerFactory = new DataBaseLoggerFactory();
//Logger logger = loggerFactory.CreateLogger();
//logger.WriteLog();
#endregion

Console.ReadLine();
