//解释器模式

#region 测试代码
string instruction = "LOOP 2 PRINT 杨过 SPACE SPACE PRINT 小龙女 BREAK END PRINT 郭靖 SPACE SPACE PRINT 黄蓉";
Context context = new Context(instruction);

Node node = new ExpressionNode();
node.Interpret(context);

Console.WriteLine("源指令 : {0}", instruction);
Console.WriteLine("解释后 : ");

node.Execute();
Console.ReadLine();
#endregion

#region Context：环境类
/// <summary>
/// 环境类：用于存储和操作需要解释的语句，
/// 在本实例中每一个需要解释的单词都可以称为一个动作标记（ActionToker）或命令
/// </summary>
public class Context
{
    private int index = -1;
    private string[] tokens;
    private string currentToken;

    public Context(string text)
    {
        text = text.Replace("  ", " ");
        tokens = text.Split(' ');
        NextToken();
    }

    // 获取下一个标记
    public string NextToken()
    {
        if (index < tokens.Length - 1)
        {
            currentToken = tokens[++index];
        }
        else
        {
            currentToken = null;
        }

        return currentToken;
    }

    // 返回当前的标记
    public string GetCurrentToken()
    {
        return currentToken;
    }

    // 跳过一个标记
    public void SkipToken(string token)
    {
        if (!token.Equals(currentToken, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("错误提示：{0} 解释错误！", currentToken);
        }

        NextToken();
    }

    // 如果当前的标记是一个数字，则返回对应的数值
    public int GetCurrentNumber()
    {
        int number = 0;
        try
        {
            // 将字符串转换为整数
            number = Convert.ToInt32(currentToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine("错误提示：{0}", ex.Message);
        }

        return number;
    }
}
#endregion

#region Node：抽象节点类，充当抽象表达式
public abstract class Node
{
    // 声明一个方法用于解释语句
    public abstract void Interpret(Context context);
    // 声明一个方法用于执行标记对应的命令
    public abstract void Execute();
}
#endregion

#region ExpressionNode、CommandNode、LoopCommandNode：表达式节点类、语句命令节点类、循环命令类，充当非终结符表达式

public class ExpressionNode : Node
{
    // 用于存储多条命令的集合
    private IList<Node> nodeList = new List<Node>();

    public override void Interpret(Context context)
    {
        // 循环处理Context中的标记
        while (true)
        {
            // 如果已经没有任何标记，则退出解释
            if (context.GetCurrentToken() == null)
            {
                break;
            }
            // 如果标记为END，则不解释END并结束本次解释过程，可以继续之后的解释
            else if (context.GetCurrentToken().Equals("END", StringComparison.OrdinalIgnoreCase))
            {
                context.SkipToken("END");
                break;
            }
            // 如果为其它标记，则解释标记并加入命令集合
            else
            {
                Node node = new CommandNode();
                node.Interpret(context);
                nodeList.Add(node);
            }
        }
    }

    // 循环执行命令集合中的每一条指令
    public override void Execute()
    {
        foreach (var node in nodeList)
        {
            node.Execute();
        }
    }
}

public class CommandNode : Node
{
    private Node node;

    public override void Interpret(Context context)
    {
        // 处理LOOP指令
        if (context.GetCurrentToken().Equals("LOOP", StringComparison.OrdinalIgnoreCase))
        {
            node = new LoopCommand();
            node.Interpret(context);
        }
        // 处理其他指令
        else
        {
            node = new PrimitiveCommand();
            node.Interpret(context);
        }
    }

    public override void Execute()
    {
        node.Execute();
    }
}

public class LoopCommand : Node
{
    // 循环次数
    private int number;
    // 循环语句中的表达式
    private Node commandNode;

    public override void Interpret(Context context)
    {
        context.SkipToken("LOOP");
        number = context.GetCurrentNumber();
        context.NextToken();
        // 循环语句中的表达式
        commandNode = new ExpressionNode();
        commandNode.Interpret(context);
    }

    public override void Execute()
    {
        for (int i = 0; i < number; i++)
        {
            commandNode.Execute();
        }
    }
}
#endregion

#region PrimitiveCommandNode：基本命令类，充当终结符表达式
public class PrimitiveCommand : Node
{
    private string name;
    private string text;

    public override void Interpret(Context context)
    {
        name = context.GetCurrentToken();
        context.SkipToken(name);

        if (!name.Equals("PRINT", StringComparison.OrdinalIgnoreCase)
            && !name.Equals("BREAK", StringComparison.OrdinalIgnoreCase)
            && !name.Equals("SPACE", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("非法命令！");
        }

        if (name.Equals("PRINT", StringComparison.OrdinalIgnoreCase))
        {
            text = context.GetCurrentToken();
            context.NextToken();
        }
    }

    public override void Execute()
    {
        if (name.Equals("PRINT", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write(text);
        }
        else if (name.Equals("SPACE", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write(" ");
        }
        else if (name.Equals("BREAK", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("\r\n");
        }
    }
}
#endregion