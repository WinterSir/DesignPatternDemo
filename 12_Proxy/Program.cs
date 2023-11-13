//代理模式

#region 测试代码
ISearcher searcher = new ProxySearcher();
if (searcher != null)
{
    string result = searcher.DoSearch("杨过", "玉女心经");
}
Console.ReadLine();
#endregion

#region ISearcher：抽象查询接口，充当抽象主题类
public interface ISearcher
{
    string DoSearch(string userID, string keyword);
}
#endregion

#region RealSearcher：具体查询器，充当真实主题类
public class RealSearcher
{
    /// <summary>
    /// 模拟查询商务信息
    /// </summary>
    /// <returns></returns>
    public string DoSearch(string userID, string keyword)
    {
        Console.WriteLine("{0} 使用关键词 {1}", userID, keyword);
        return "返回具体内容";
    }
}
#endregion

#region AccessValidator、Logger：身份验证类、日志记录类，业务类
public class AccessValidator
{
    /// <summary>
    /// 模拟实现登录验证
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public bool Validate(string userID)
    {
        Console.WriteLine("在数据库中验证用户 {0} 是否是合法用户?", userID);
        if (userID.Equals("杨过", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("{0} 登录成功!", userID);
            return true;
        }
        else
        {
            Console.WriteLine("{0} 登录失败!", userID);
            return false;
        }
    }
}
public class Logger
{
    /// <summary>
    /// 模拟实现日志记录
    /// </summary>
    /// <param name="userID"></param>
    public void Log(string userID)
    {
        Console.WriteLine("更新数据库，用户 {0} 查询次数加1!", userID);
    }
}
#endregion

#region ProxySearcher：代理查询类，充当代理主题类
public class ProxySearcher : ISearcher
{
    private RealSearcher searcher = new RealSearcher(); // 维持一个对真实主题的引用
    private AccessValidator validator;
    private Logger logger;

    public string DoSearch(string userID, string keyword)
    {
        if (Validate(userID))
        {
            string result = searcher.DoSearch(userID, keyword);
            this.Log(userID);
            return result;
        }

        return null;
    }

    /// <summary>
    /// 创建访问验证对象并调用其Validate()方法进行身份验证
    /// </summary>
    /// <returns></returns>
    public bool Validate(string userID)
    {
        validator = new AccessValidator();
        return validator.Validate(userID);
    }

    /// <summary>
    /// 创建日志记录器并调用Log()方法实现日志记录
    /// </summary>
    /// <param name="userID"></param>
    public void Log(string userID)
    {
        logger = new Logger();
        logger.Log(userID);
    }
}
#endregion
