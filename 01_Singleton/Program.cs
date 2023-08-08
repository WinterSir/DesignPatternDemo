using System.Collections.Concurrent;

//单例模式

#region 测试代码

Console.WriteLine("以5.Lazy为例简单测试");
Singleton5 singleton5_1 = Singleton5.GetInstance();
Singleton5 singleton5_2 = Singleton5.GetInstance();
if (singleton5_1 == singleton5_2)
{
    Console.WriteLine("实例唯一\n");
}

Console.WriteLine("以2.懒汉为例多线程不安全测试");
List<Task> singleton2Tasks = new List<Task>();
ConcurrentBag<Singleton2> singleton2List = new ConcurrentBag<Singleton2>();
for (int i = 0; i < 100; i++)
{
    singleton2Tasks.Add(Task.Run(() =>
    {
        singleton2List.Add(Singleton2.GetInstance());
    }));
}
Task.WaitAll(singleton2Tasks.ToArray());
int dis2Count = singleton2List.Distinct().Count();
Console.WriteLine($"实例总个数：{singleton2List.Count}，去重后个数：{dis2Count}，多线程实例{(dis2Count > 1 ? "不" : "")}唯一\n");

Console.WriteLine("以5.懒汉为例多线程安全测试");
List<Task> singleton5Tasks = new List<Task>();
ConcurrentBag<Singleton5> singleton5List = new ConcurrentBag<Singleton5>();
for (int k = 0; k < 100; k++)
{
    singleton5Tasks.Add(Task.Run(() =>
    {
        singleton5List.Add(Singleton5.GetInstance());
    }));
}
Task.WaitAll(singleton5Tasks.ToArray());
int dis5Count = singleton5List.Distinct().Count();
Console.WriteLine($"实例总个数：{singleton5List.Count}，去重后个数：{singleton5List.Distinct().Count()}，多线程实例{(dis5Count > 1 ? "不" : "")}唯一");

Console.ReadLine();
#endregion

#region 1.饿汉
public class Singleton1
{
    private static readonly Singleton1 instance = new Singleton1();

    private Singleton1()
    {

    }

    public static Singleton1 GetInstance()
    {
        return instance;
    }
}
#endregion

#region 2.懒汉
public class Singleton2
{
    private static Singleton2 instance = null;

    private Singleton2()
    {

    }
    public static Singleton2 GetInstance()
    {
        if (instance == null)
        {
            instance = new Singleton2();
        }
        return instance;
    }
}
#endregion

#region 3.双检 Double-Checked Locking
public class Singleton3
{
    private static Singleton3 instance = null;
    private static readonly object syncLocker = new object();
    private Singleton3()
    {

    }
    public static Singleton3 GetInstance()
    {
        if (instance == null)
        {
            lock (syncLocker)
            {
                if (instance == null)
                {
                    instance = new Singleton3();
                }
            }
        }
        return instance;
    }
}
#endregion

#region 4.静态内部类
public class Singleton4
{
    private Singleton4()
    {

    }

    public static Singleton4 GetInstance()
    {
        return Nested.instance;
    }

    private class Nested
    {
        static Nested() { }
        internal static readonly Singleton4 instance = new Singleton4();
    }
}
#endregion

#region 5.Lazy
public class Singleton5
{
    private static readonly Lazy<Singleton5> lazy = new Lazy<Singleton5>(() => new Singleton5());
    private Singleton5() { }
    public static Singleton5 GetInstance()
    {
        return lazy.Value;
    }
}
#endregion

#region 6.CAS
public class Singleton6
{
    private static Singleton6 instance = null;
    private Singleton6() { }
    public static Singleton6 GetInstance()
    {
        if (instance != null)
        {
            return instance;
        }
        var instance_value = new Singleton6();
        Interlocked.CompareExchange(ref instance, instance_value, null);
        return instance;
    }
}
#endregion

