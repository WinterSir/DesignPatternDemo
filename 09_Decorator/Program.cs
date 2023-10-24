//装饰模式

#region 测试代码
Component component = new Window();
Component componentSB = new ScrollBarDecorator(component);
componentSB.Display();
Component componentBB = new BlackBorderDecorator(componentSB);
componentBB.Display();
Console.ReadLine(); 
#endregion

#region Component：抽象界面构件类，充当抽象构件
public abstract class Component
{
    public abstract void Display();
}
#endregion

#region Window、TextBox、ListBox：窗体、文本框、列表框类，充当具体构件类
public class Window : Component
{
    public override void Display()
    {
        Console.WriteLine("显示窗体!");
    }
}

public class TextBox : Component
{
    public override void Display()
    {
        Console.WriteLine("显示文本框!");
    }
}

public class ListBox : Component
{
    public override void Display()
    {
        Console.WriteLine("显示列表框!");
    }
}
#endregion

#region ComponentDecorator：构件装饰类，充当抽象装饰类
public class ComponentDecorator : Component
{
    private Component component;

    public ComponentDecorator(Component component)
    {
        this.component = component;
    }

    public override void Display()
    {
        component.Display();
    }
}
#endregion

#region ScrollBarDecorator、BlackBorderDecorator：滚动条装饰类、黑色边框装饰类，充当具体装饰类
public class ScrollBarDecorator : ComponentDecorator
{
    public ScrollBarDecorator(Component component) : base(component)
    {

    }

    public override void Display()
    {
        this.SetScrollBar();
        base.Display();
    }

    public void SetScrollBar()
    {
        Console.WriteLine("为构件增加滚动条!");
    }
}
public class BlackBorderDecorator : ComponentDecorator
{
    public BlackBorderDecorator(Component component) : base(component)
    {

    }

    public override void Display()
    {
        this.SetScrollBar();
        base.Display();
    }

    public void SetScrollBar()
    {
        Console.WriteLine("为构件增加黑色边框!");
    }
}
#endregion