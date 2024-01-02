//观察者模式

#region 测试代码
// Step1.定义观察者对象
AllyControlCenter acc = new ConcreteAllyControlCenter("金庸群侠");
// Step2.定义4个观察者对象
IObserver playerA = new Player() { Name = "杨过" };
acc.Join(playerA);
IObserver playerB = new Player() { Name = "令狐冲" };
acc.Join(playerB);
IObserver playerC = new Player() { Name = "张无忌" };
acc.Join(playerC);
IObserver playerD = new Player() { Name = "段誉" };
acc.Join(playerD);
// Step3.当某盟友遭受攻击
playerA.BeAttacked(acc);
Console.ReadLine();

#endregion;

#region IObserver：抽象观察者
public interface IObserver
{
    string Name { get; set; }
    void Help();                                // 声明支援盟友的方法
    void BeAttacked(AllyControlCenter acc);     // 声明遭受攻击的方法
}

#endregion

#region Player：战队成员类，充当具体观察者
public class Player : IObserver
{
    public string Name { get; set; }

    public void BeAttacked(AllyControlCenter acc)
    {
        Console.WriteLine("{0}：我正被攻击，速来援救！", this.Name);
        // 调用战队控制中心类的通知方法来通知盟友
        acc.NotifyObserver(this.Name);
    }

    public void Help()
    {
        Console.WriteLine("{0} ：坚持住，立马来救你！", this.Name);
    }
}

#endregion

#region AllyControlCenter：抽象战队控制中心类，充当抽象目标
public abstract class AllyControlCenter
{
    public string AllyName { get; set; }
    protected IList<IObserver> playerList = new List<IObserver>();

    public void Join(IObserver observer)
    {
        playerList.Add(observer);
        Console.WriteLine("通知：{0} 加入 {1} 战队", observer.Name, this.AllyName);
    }

    public void Quit(IObserver observer)
    {
        playerList.Remove(observer);
        Console.WriteLine("通知：{0} 退出 {1} 战队", observer.Name, this.AllyName);
    }

    // 声明抽象通知方法
    public abstract void NotifyObserver(string name);
}

#endregion

#region ConcreteAllyControlCenter：具体战队控制中心类，充当具体目标
public class ConcreteAllyControlCenter : AllyControlCenter
{
    public ConcreteAllyControlCenter(string allyName)
    {
        Console.WriteLine("系统通知：{0} 战队组建成功！", this.AllyName);
        Console.WriteLine("-------------------------------------------------------");
        this.AllyName = allyName;
    }

    // 实现通知方法
    public override void NotifyObserver(string playerName)
    {
        Console.WriteLine("通知：盟友们，{0} 正遭受敌军攻击，速去抢救！", playerName);
        foreach (var player in playerList)
        {
            if (!player.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase))
            {
                player.Help();
            }
        }
    }
}
#endregion