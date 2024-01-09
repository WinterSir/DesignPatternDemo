//状态模式
#region 测试代码
Account acc = new Account("段誉", 0.0);
acc.Deposit(1000);
acc.Withdraw(2000);
acc.Deposit(3000);
acc.Withdraw(4000);
acc.Withdraw(1000);
acc.ComputeInterest();
Console.ReadLine();
#endregion

#region Account：银行账户，充当环境类
public class Account
{
    private AccountState state;  //维持一个对抽象状态对象的引用
    private string owner;        //开户名
    private double balance = 0;  //账户余额
    public Account(string owner, double init)
    {
        this.owner = owner;
        this.balance = init;
        this.state = new NormalState(this);
        Console.WriteLine("{0}开户,初始金额为{1}", this.owner, init);
        Console.WriteLine("------------------------------------");
    }
    //设置初始状态
    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }
    public void SetState(AccountState state)
    {
        this.state = state;
    }
    public void Deposit(double amount)
    {
        Console.WriteLine("{0}存款{1}", this.owner, amount);
        state.Deposit(amount);// 调用状态对象的Deposit()方法
        Console.WriteLine("现在余额为{0}", this.Balance);
        Console.WriteLine("现在账户状态为{0}", this.state.GetType().ToString());
        Console.WriteLine("------------------------------------");
    }
    public void Withdraw(double amount)
    {
        Console.WriteLine("{0}取款{1}", this.owner, amount);
        state.Withdraw(amount); //调用状态对象的Withdraw()方法
        Console.WriteLine("现在余额为{0}", this.Balance);
        Console.WriteLine("现在账户状态为{0}", this.state.GetType().ToString());
        Console.WriteLine("------------------------------------");
    }
    public void ComputeInterest()
    {
        state.ComputeInterest();   //调用状态对象的computeInterest方法
    }
}
#endregion

#region AccountState：账户状态类，充当抽象状态类
public abstract class AccountState
{
    private Account acc;
    public Account Acc
    {
        get { return acc; }
        set { acc = value; }
    }
    public abstract void Deposit(double amount);
    public abstract void Withdraw(double amount);
    public abstract void ComputeInterest();
    public abstract void StateCheck();
}
#endregion

#region NormalState、OverdraftState、RestrictedState：正常状态、透支状态、受限状态，充当具体状态类
public class NormalState : AccountState
{
    public NormalState(Account acc)
    {
        this.Acc = acc;
    }
    public NormalState(AccountState state)
    {
        this.Acc = state.Acc;
    }

    public override void Deposit(double amount)
    {
        Acc.Balance = Acc.Balance + amount;
        StateCheck();
    }
    public override void Withdraw(double amount)
    {
        Acc.Balance = Acc.Balance - amount;
        StateCheck();
    }

    public override void ComputeInterest()
    {
        Console.WriteLine("正常状态，无需支付利息！");
    }

    public override void StateCheck()
    {
        if (Acc.Balance > -2000 && Acc.Balance <= 0)
        {
            Acc.SetState(new OverdraftState(this));
        }
        else if (Acc.Balance == -2000)
        {
            Acc.SetState(new RestrictedState(this));
        }
        else if (Acc.Balance < -2000)
        {
            Console.WriteLine("操作受限！");
        }
    }
}

public class OverdraftState : AccountState
{
    public OverdraftState(AccountState state)
    {
        this.Acc = state.Acc;
    }
    public override void Deposit(double amount)
    {
        Acc.Balance = Acc.Balance + amount;
        StateCheck();
    }
    public override void Withdraw(double amount)
    {
        Acc.Balance = Acc.Balance - amount;
        StateCheck();
    }

    public override void ComputeInterest()
    {
        Console.WriteLine("计算利息！");
    }

    public override void StateCheck()
    {
        if (Acc.Balance > 0)
        {
            Acc.SetState(new NormalState(this));
        }
        else if (Acc.Balance == -2000)
        {
            Acc.SetState(new OverdraftState(this));
        }
        else if (Acc.Balance < -2000)
        {
            Console.WriteLine("操作受限！");
        }
    }
}

public class RestrictedState : AccountState
{
    public RestrictedState(AccountState state)
    {
        this.Acc = state.Acc;
    }
    public override void Deposit(double amount)
    {
        Acc.Balance = Acc.Balance + amount;
        StateCheck();
    }
    public override void Withdraw(double amount)
    {
        Console.WriteLine("账户受限，取款失败！");
    }

    public override void ComputeInterest()
    {
        Console.WriteLine("计算利息！");
    }

    public override void StateCheck()
    {
        if (Acc.Balance > 0)
        {
            Acc.SetState(new NormalState(this));
        }
        else if (Acc.Balance > -2000)
        {
            Acc.SetState(new OverdraftState(this));
        }
    }
}
#endregion