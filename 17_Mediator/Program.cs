//中介者模式

#region 测试代码
// Step1.定义中介者对象
ConcreteMediator mediator = new ConcreteMediator();

// Step2.定义同事对象
Button addButton = new Button();
List list = new List();
ComboBox cb = new ComboBox();
TextBox userNameTextBox = new TextBox();

addButton.SetMediator(mediator);
list.SetMediator(mediator);
cb.SetMediator(mediator);
userNameTextBox.SetMediator(mediator);

mediator.addButton = addButton;
mediator.list = list;
mediator.cb = cb;
mediator.userNameTextBox = userNameTextBox;

// Step3.点击增加按钮
addButton.Changed();

Console.WriteLine("---------------------------------------------");

// Step4.从列表框选择客户
list.Changed();
Console.ReadLine();
#endregion

#region Mediator：抽象中介者
public abstract class Mediator
{
    public abstract void ComponenetChanged(Component c);
}
#endregion

#region ConcreteMediator：具体中介者
public class ConcreteMediator : Mediator
{
    // 维持对各个同事对象的引用
    public Button addButton;
    public List list;
    public TextBox userNameTextBox;
    public ComboBox cb;

    // 封装同事对象之间的交互
    public override void ComponenetChanged(Component c)
    {
        // 单击按钮
        if (c == addButton)
        {
            Console.WriteLine("-- 单击增加按钮 --");
            list.Update();
            cb.Update();
            userNameTextBox.Update();
        }
        // 从列表框选择客户
        else if (c == list)
        {
            Console.WriteLine("-- 从列表框选择客户 --");
            cb.Select();
            userNameTextBox.SetText();
        }
        // 从组合框选择客户
        else if (c == cb)
        {
            Console.WriteLine("-- 从组合框选择客户 --");
            cb.Select();
            userNameTextBox.SetText();
        }
    }
}
#endregion

#region Component：抽象组件，充当抽象同事类（Colleague）
public abstract class Component
{
    protected Mediator mediator;

    public void SetMediator(Mediator mediator)
    {
        this.mediator = mediator;
    }

    // 转发调用
    public void Changed()
    {
        mediator.ComponenetChanged(this);
    }

    public abstract void Update();
}
#endregion

#region Button、List、ComboBox、TextBox：按钮、列表框、组合框、文本框，具体组件，充当具体同事类（ConcreteColleague）
public class Button : Component
{
    public override void Update()
    {
        // 按钮不产生响应
    }
}

public class List : Component
{
    public override void Update()
    {
        Console.WriteLine("列表框增加一项：张无忌");
    }

    public void Select()
    {
        Console.WriteLine("列表框选中项：小龙女");
    }
}

public class ComboBox : Component
{
    public override void Update()
    {
        Console.WriteLine("组合框增加一项：张无忌");
    }

    public void Select()
    {
        Console.WriteLine("组合框选中项：小龙女");
    }
}

public class TextBox : Component
{
    public override void Update()
    {
        Console.WriteLine("客户信息增加成功后文本框清空");
    }

    public void SetText()
    {
        Console.WriteLine("文本框显示：小龙女");
    }
}
#endregion