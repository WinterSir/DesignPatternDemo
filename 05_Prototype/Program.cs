//原型模式

#region 测试代码
using System.Text.Json;

Console.WriteLine("浅克隆");
WeeklyLog log_previous, log_new;
log_previous = new WeeklyLog();
log_previous.Attachment = new Attachment();
log_new = log_previous.Clone();
Console.WriteLine("周报是否相同：{0}", (log_previous == log_new ? "是" : "否"));
Console.WriteLine("附件是否相同：{0}", (log_previous.Attachment == log_new.Attachment ? "是" : "否"));

Console.WriteLine("深克隆");
log_new = log_previous.CloneDeep();
Console.WriteLine("周报是否相同：{0}", (log_previous == log_new ? "是" : "否"));
Console.WriteLine("附件是否相同：{0}", (log_previous.Attachment == log_new.Attachment ? "是" : "否"));
Console.ReadLine();
#endregion

#region WeeklyLog、Attachment：周报类、附件类，实际业务较为复杂，示例简化只列出部分属性
public class WeeklyLog
{
    public string Name { get; set; }
    public string Date { get; set; }
    public string Content { get; set; }
    public Attachment Attachment { get; set; }

    public WeeklyLog Clone()
    {
        return this.MemberwiseClone() as WeeklyLog;
    }
    public WeeklyLog CloneDeep()
    {
        return JsonSerializer.Deserialize<WeeklyLog>(JsonSerializer.Serialize(this));
    }
}
public class Attachment
{
    public string Name { get; set; }
}
#endregion
