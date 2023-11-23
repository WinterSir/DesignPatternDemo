//命令模式

#region 测试代码
//Step1.模拟显示功能键设置窗口
FBSettingWindow window = new FBSettingWindow("功能键设置窗口");

// Step2.假如目前要设置两个功能键
FunctionButton buttonA = new FunctionButton("功能键A");
FunctionButton buttonB = new FunctionButton("功能键B");

// Step3.读取配置文件和反射生成具体命令对象
Command commandA = new HelpCommand();
Command commandB = new MinimizeCommand();

// Step4.将命令注入功能键
buttonA.SetCommand(commandA);
buttonB.SetCommand(commandB);

window.AddFunctionButton(buttonA);
window.AddFunctionButton(buttonB);
window.Display();

// Step5.调用功能键的业务方法
buttonA.OnClick();
buttonB.OnClick();
Console.ReadLine();
#endregion

#region 功能键设置”界面类，充当客户端
public class FBSettingWindow
{
    // 窗口标题
    public string Title { get; set; }
    // 所有功能键集合
    private IList<FunctionButton> functionButtonList = new List<FunctionButton>();

    public FBSettingWindow(string title)
    {
        this.Title = title;
    }

    public void AddFunctionButton(FunctionButton fb)
    {
        functionButtonList.Add(fb);
    }

    public void RemoveFunctionButton(FunctionButton fb)
    {
        functionButtonList.Remove(fb);
    }

    // 显示窗口及功能键
    public void Display()
    {
        Console.WriteLine("显示窗口：{0}", this.Title);
        Console.WriteLine("显示功能键：");

        foreach (var fb in functionButtonList)
        {
            Console.WriteLine(fb.Name);
        }

        Console.WriteLine("------------------------------------------");
    }
}
#endregion

#region FunctionButton：请求发送类，充当调用者
public class FunctionButton
{
    // 功能键名称
    public string Name { get; set; }
    // 维持一个抽象命令对象的引用
    private Command command;

    public FunctionButton(string name)
    {
        this.Name = name;
    }

    // 为功能键注入命令
    public void SetCommand(Command command)
    {
        this.command = command;
    }

    // 发送请求的方法
    public void OnClick()
    {
        Console.WriteLine("点击功能键：");
        if (command != null)
        {
            command.Execute();
        }
    }
}
#endregion

#region Command：抽象命令类
public abstract class Command
{
    public abstract void Execute();
}
#endregion

#region HelpCommand、MinimizeCommand：帮助类、最小化类，充当具体命令类
public class HelpCommand : Command
{
    private HelpHandler hander;

    public HelpCommand()
    {
        hander = new HelpHandler();
    }

    // 命令执行方法，将调用请求接受者的业务方法
    public override void Execute()
    {
        if (hander != null)
        {
            hander.Display();
        }
    }
}

public class MinimizeCommand : Command
{
    private WindowHandler handler;

    public MinimizeCommand()
    {
        handler = new WindowHandler();
    }

    // 命令执行方法，将调用请求接受者的业务方法
    public override void Execute()
    {
        if (handler != null)
        {
            handler.Minimize();
        }
    }
}
#endregion

#region WindowHandler、HelpHandler：最小化处理类、帮助处理类，充当接收者
public class WindowHandler
{
    public void Minimize()
    {
        Console.WriteLine("正在最小化窗口至托盘...");
    }
}

public class HelpHandler
{
    public void Display()
    {
        Console.WriteLine("正在显示帮助文档...");
    }
}
#endregion