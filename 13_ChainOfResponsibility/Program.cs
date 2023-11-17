//职责链模式

#region 测试代码
// 创建职责链
Approver andy = new Director("Andy");
Approver jacky = new VicePresident("Jacky");
Approver ashin = new President("Ashin");
Approver meeting = new Congress("Congress");

andy.SetSuccessor(jacky);
jacky.SetSuccessor(ashin);
ashin.SetSuccessor(meeting);

// 构造采购请求单并发送审批请求
PurchaseRequest request1 = new PurchaseRequest(45000.00, "MANULIFE201706001", "购买PC和显示器");
andy.ProcessRequest(request1);

PurchaseRequest request2 = new PurchaseRequest(60000.00, "MANULIFE201706002", "2017开发团队活动");
andy.ProcessRequest(request2);

PurchaseRequest request3 = new PurchaseRequest(160000.00, "MANULIFE201706003", "2017公司年度旅游");
andy.ProcessRequest(request3);

PurchaseRequest request4 = new PurchaseRequest(800000.00, "MANULIFE201706004", "租用新临时办公楼");
andy.ProcessRequest(request4);

Console.ReadLine();
#endregion

#region PurchaseRequest：采购请求类
public class PurchaseRequest
{
    // 采购金额
    public double Amount { get; set; }
    // 采购单编号
    public string Number { get; set; }
    // 采购目的
    public string Purpose { get; set; }

    public PurchaseRequest(double amount, string number, string purpose)
    {
        Amount = amount;
        Number = number;
        Purpose = purpose;
    }
}
#endregion

#region Approver：审批者类，充当抽象处理者
public abstract class Approver
{
    protected Approver successor; // 定义后继对象
    protected string name;  // 审批者姓名

    public Approver(string name)
    {
        this.name = name;
    }

    // 设置后继者
    public void SetSuccessor(Approver successor)
    {
        this.successor = successor;
    }

    // 抽象请求处理方法
    public abstract void ProcessRequest(PurchaseRequest request);
}
#endregion

#region Director、VicePresident、President、Congress：主管、副总裁、总裁、董事会，充当具体处理类
/// <summary>
/// 主管：具体处理类
/// </summary>
public class Director : Approver
{
    public Director(string name) : base(name)
    {
    }

    // 具体请求处理方法
    public override void ProcessRequest(PurchaseRequest request)
    {
        if (request.Amount < 50000)
        {
            // 处理请求
            Console.WriteLine("主管 {0} 审批采购单：{1}，金额：{2} 元，采购目的：{3}。",
                this.name, request.Number, request.Amount, request.Purpose);
        }
        else
        {
            // 如果处理不了，转发请求给更高层领导
            this.successor.ProcessRequest(request);
        }
    }
}

/// <summary>
/// 副总裁：具体处理类
/// </summary>
public class VicePresident : Approver
{
    public VicePresident(string name) : base(name)
    {
    }

    // 具体请求处理方法
    public override void ProcessRequest(PurchaseRequest request)
    {
        if (request.Amount < 100000)
        {
            // 处理请求
            Console.WriteLine("副总裁 {0} 审批采购单：{1}，金额：{2} 元，采购目的：{3}。",
                this.name, request.Number, request.Amount, request.Purpose);
        }
        else
        {
            // 如果处理不了，转发请求给更高层领导
            this.successor.ProcessRequest(request);
        }
    }
}

/// <summary>
/// 总裁：具体处理者
/// </summary>
public class President : Approver
{
    public President(string name) : base(name)
    {
    }

    // 具体请求处理方法
    public override void ProcessRequest(PurchaseRequest request)
    {
        if (request.Amount < 500000)
        {
            // 处理请求
            Console.WriteLine("总裁 {0} 审批采购单：{1}，金额：{2} 元，采购目的：{3}。",
                this.name, request.Number, request.Amount, request.Purpose);
        }
        else
        {
            // 如果处理不了，转发请求给更高层领导
            this.successor.ProcessRequest(request);
        }
    }
}

/// <summary>
/// 董事会：具体处理者
/// </summary>
public class Congress : Approver
{
    public Congress(string name) : base(name)
    {
    }

    // 具体请求处理方法
    public override void ProcessRequest(PurchaseRequest request)
    {
        // 处理请求
        Console.WriteLine("董事会 {0} 审批采购单：{1}，金额：{2} 元，采购目的：{3}。",
            this.name, request.Number, request.Amount, request.Purpose);
    }
}
#endregion
