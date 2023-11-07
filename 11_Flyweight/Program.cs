//享元模式
using System.Collections;

#region 测试代码
// 获取享元工厂
IgoChessmanFactory chessFactory = IgoChessmanFactory.GetInstance();
// 通过享元工厂获取3颗黑子
IgoChessman blackChess1 = chessFactory.GetIgoChessman("b");
IgoChessman blackChess2 = chessFactory.GetIgoChessman("b");
IgoChessman blackChess3 = chessFactory.GetIgoChessman("b");

Console.WriteLine("判断两颗黑子是否相同：{0}", object.ReferenceEquals(blackChess1, blackChess2));
// 通过享元工厂获取2颗白子
IgoChessman whiteChess1 = chessFactory.GetIgoChessman("w");
IgoChessman whiteChess2 = chessFactory.GetIgoChessman("w");

Console.WriteLine("判断两颗白子是否相同：{0}", object.ReferenceEquals(whiteChess1, whiteChess2));
// 显示棋子
blackChess1.Display(new Coordinates(1, 2));
blackChess2.Display(new Coordinates(3, 4));
blackChess3.Display(new Coordinates(1, 3));
whiteChess1.Display(new Coordinates(2, 5));
whiteChess2.Display(new Coordinates(2, 4));
Console.ReadLine();
#endregion

#region IgoChessman：抽象享元类
public abstract class IgoChessman
{
    public abstract string GetColor();

    public void Display(Coordinates coord)
    {
        Console.WriteLine("棋子颜色：{0}，棋子位置：{1}", GetColor(), coord.X + "," + coord.Y);
    }
}

/// <summary>
/// 外部状态：棋子坐标
/// </summary>
public class Coordinates
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinates()
    {

    }

    public Coordinates(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}
#endregion

#region BlackIgoChessman、WhiteIgoChessman：黑棋、白棋享元类，充当具体享元类
public class BlackIgoChessman : IgoChessman
{
    public override string GetColor()
    {
        return "黑色";
    }
}

public class WhiteIgoChessman : IgoChessman
{
    public override string GetColor()
    {
        return "白色";
    }
}
#endregion

#region IgoChessmanFactory：享元工厂类
public class IgoChessmanFactory
{
    private static readonly IgoChessmanFactory instance = new IgoChessmanFactory(); // 使用单例模式实现享元
    private static Hashtable ht;    // 使用Hashtable来存储享元对象，充当享元池

    private IgoChessmanFactory()
    {
        ht = new Hashtable();
        IgoChessman blackChess = new BlackIgoChessman();
        ht.Add("b", blackChess);
        IgoChessman whiteChess = new WhiteIgoChessman();
        ht.Add("w", whiteChess);
    }

    public static IgoChessmanFactory GetInstance()
    {
        return instance;
    }

    public IgoChessman GetIgoChessman(string color)
    {
        IgoChessman chess = ht[color] as IgoChessman;
        return chess;
    }
}
#endregion