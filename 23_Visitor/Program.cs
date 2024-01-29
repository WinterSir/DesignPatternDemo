//访问者模式

#region 测试代码
EmployeeList empList = new EmployeeList();
IEmployee fteA = new FullTimeEmployee("梁思成", 3200.00, 45);
IEmployee fteB = new FullTimeEmployee("徐志摩", 2000, 40);
IEmployee fteC = new FullTimeEmployee("梁徽因", 2400, 38);
IEmployee fteD = new PartTimeEmployee("方鸿渐", 80, 20);
IEmployee fteE = new PartTimeEmployee("唐宛如", 60, 18);

empList.AddEmployee(fteA);
empList.AddEmployee(fteB);
empList.AddEmployee(fteC);
empList.AddEmployee(fteD);
empList.AddEmployee(fteE);

Department dept = new HRDepartment();
if (dept != null)
{
    empList.Accept(dept);
}
Console.ReadLine();
#endregion

#region IEmployee：员工接口，充当抽象元素
public interface IEmployee
{
    void Accept(Department handler);
}
#endregion

#region FullTimeEmployee，PartTimeEmployee：正式员工类、临时员工类，充当具体元素
public class FullTimeEmployee : IEmployee
{
    public string Name { get; set; }
    public double WeeklyWage { get; set; }
    public int WorkTime { get; set; }

    public FullTimeEmployee(string name, double weeklyWage, int workTime)
    {
        this.Name = name;
        this.WeeklyWage = weeklyWage;
        this.WorkTime = workTime;
    }

    public void Accept(Department handler)
    {
        handler.Visit(this);
    }
}

public class PartTimeEmployee : IEmployee
{
    public string Name { get; set; }
    public double HourWage { get; set; }
    public int WorkTime { get; set; }

    public PartTimeEmployee(string name, double hourWage, int workTime)
    {
        this.Name = name;
        this.HourWage = hourWage;
        this.WorkTime = workTime;
    }

    public void Accept(Department handler)
    {
        handler.Visit(this);
    }
}
#endregion

#region EmployeeList：员工集合类，充当对象结构
public class EmployeeList
{
    private IList<IEmployee> empList = new List<IEmployee>();

    public void AddEmployee(IEmployee emp)
    {
        this.empList.Add(emp);
    }

    public void Accept(Department handler)
    {
        foreach (var emp in empList)
        {
            emp.Accept(handler);
        }
    }
}
#endregion

#region Department：部门抽象类，充当抽象访问者
public abstract class Department
{
    // 声明一组重载的访问方法，用于访问不同类型的具体元素
    public abstract void Visit(FullTimeEmployee employee);
    public abstract void Visit(PartTimeEmployee employee);
}
#endregion

#region FinanceDepartment，HRDepartment：财务部门类、人力部门类，充当具体访问者
public class FinanceDepartment : Department
{
    // 实现财务部对兼职员工数据的访问
    public override void Visit(PartTimeEmployee employee)
    {
        int workTime = employee.WorkTime;
        double hourWage = employee.HourWage;
        Console.WriteLine("临时工 {0} 实际工资为：{1} 元", employee.Name, workTime * hourWage);
    }

    // 实现财务部对全职员工数据的访问
    public override void Visit(FullTimeEmployee employee)
    {
        int workTime = employee.WorkTime;
        double weekWage = employee.WeeklyWage;

        if (workTime > 40)
        {
            weekWage = weekWage + (workTime - 40) * 50;
        }
        else if (workTime < 40)
        {
            weekWage = weekWage - (40 - workTime) * 80;
            if (weekWage < 0)
            {
                weekWage = 0;
            }
        }

        Console.WriteLine("正式员工 {0} 实际工资为：{1} 元", employee.Name, weekWage);
    }
}

public class HRDepartment : Department
{
    // 实现人力资源部对兼职员工数据的访问
    public override void Visit(PartTimeEmployee employee)
    {
        int workTime = employee.WorkTime;
        Console.WriteLine("临时工 {0} 实际工作时间为：{1} 小时", employee.Name, workTime);
    }

    // 实现人力资源部对全职员工数据的访问
    public override void Visit(FullTimeEmployee employee)
    {
        int workTime = employee.WorkTime;
        Console.WriteLine("正式员工 {0} 实际工作时间为：{1} 小时", employee.Name, workTime);

        if (workTime > 40)
        {
            Console.WriteLine("正式员工 {0} 加班时间为：{1} 小时", employee.Name, workTime - 40);
        }
        else if (workTime < 40)
        {
            Console.WriteLine("正式员工 {0} 请假时间为：{1} 小时", employee.Name, 40 - workTime);
        }
    }
}
#endregion