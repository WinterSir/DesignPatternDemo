//策略模式

#region 测试代码
MovieTicket mt = new MovieTicket();
double originalPrice = 60.0;
double currentPrice = originalPrice;

mt.Price = originalPrice;
Console.WriteLine("原始票价：{0}", originalPrice);
Console.WriteLine("----------------------------------------");

IDiscount discount = new VIPDiscount();
if (discount != null)
{
    mt.Discount = discount;
    currentPrice = mt.Price;
}
Console.WriteLine("折后票价：{0}", currentPrice);
Console.ReadLine();
#endregion

#region MovieTicket：环境类
public class MovieTicket
{
    private double _price;
    private IDiscount _discount;

    public double Price
    {
        get
        {
            return _discount.Calculate(_price);
        }
        set
        {
            _price = value;
        }
    }

    public IDiscount Discount
    {
        set
        {
            _discount = value;
        }
    }
}
#endregion

#region IDiscount：抽象策略类
public interface IDiscount
{
    double Calculate(double price);
}
#endregion

#region StudentStrategy、VIPStrategy、ChildrenStrategy：学生折扣、VIP折扣、儿童折扣，充当具体策略
public class StudentDiscount : IDiscount
{
    public double Calculate(double price)
    {
        Console.WriteLine("学生票：");
        return price * 0.8;
    }
}

public class VIPDiscount : IDiscount
{
    public double Calculate(double price)
    {
        Console.WriteLine("VIP票：");
        Console.WriteLine("增加积分！");
        return price * 0.5;
    }
}

public class ChildrenDiscount : IDiscount
{
    public double Calculate(double price)
    {
        Console.WriteLine("儿童票：");
        return price - 10;
    }
}
#endregion