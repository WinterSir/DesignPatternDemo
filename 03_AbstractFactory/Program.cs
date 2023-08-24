//抽象工厂模式

#region 测试代码
Console.WriteLine("加载Spring皮肤\n");

ISkinFactory skinFactory = new SpringSkinFactory();

IButton button = skinFactory.CreateButton();
ITextField textField = skinFactory.CreateTextField();
IComboBox comboBox = skinFactory.CreateComboBox();

button.Display();
textField.Display();
comboBox.Display();

Console.ReadLine();
#endregion

#region 抽象产品接口：Button、TextField、ComboBox
public interface IButton
{
    void Display();
}

public interface ITextField
{
    void Display();
}

public interface IComboBox
{
    void Display();
}
#endregion

#region 具体产品类
//SpringButton、SpringTextField、SpringComboBox，Spring风格按钮、文本框、组合框
//SummerButton、SummerTextField、SummerComboBox，Summer风格按钮、文本框、组合框
public class SpringButton : IButton
{
    public void Display()
    {
        Console.WriteLine("显示浅绿色按钮。");
    }
}

public class SpringTextField : ITextField
{
    public void Display()
    {
        Console.WriteLine("显示绿色边框文本框。");
    }
}

public class SpringComboBox : IComboBox
{
    public void Display()
    {
        Console.WriteLine("显示绿色边框下拉框。");
    }
}

public class SummerButton : IButton
{
    public void Display()
    {
        Console.WriteLine("显示浅绿色按钮。");
    }
}

public class SummerTextField : ITextField
{
    public void Display()
    {
        Console.WriteLine("显示绿色边框文本框。");
    }
}

public class SummerComboBox : IComboBox
{
    public void Display()
    {
        Console.WriteLine("显示绿色边框下拉框。");
    }
}
#endregion

#region 抽象皮肤工厂接口：ISkinFactory
public interface ISkinFactory
{
    IButton CreateButton();
    ITextField CreateTextField();
    IComboBox CreateComboBox();
}
#endregion

#region 具体皮肤工厂类：SpringSkinFactory、SummerSkinFactory
public class SpringSkinFactory : ISkinFactory
{
    public IButton CreateButton()
    {
        return new SpringButton();
    }

    public IComboBox CreateComboBox()
    {
        return new SpringComboBox();
    }

    public ITextField CreateTextField()
    {
        return new SpringTextField();
    }
}
public class SummerSkinFactory : ISkinFactory
{
    public IButton CreateButton()
    {
        return new SummerButton();
    }

    public IComboBox CreateComboBox()
    {
        return new SummerComboBox();
    }

    public ITextField CreateTextField()
    {
        return new SummerTextField();
    }
}
#endregion
