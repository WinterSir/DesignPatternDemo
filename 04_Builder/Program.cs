//建造者模式

#region 测试代码
ActorBuilder ab = new HeroBuilder();
ActorController ac = new ActorController();
Actor at = ac.Construct(ab);

Console.WriteLine(at.Type);
Console.WriteLine(at.Sex);
Console.WriteLine(at.Face);
Console.WriteLine(at.Costume);
Console.WriteLine(at.HairStyle);

Console.ReadLine();
#endregion

#region Actor：角色类复杂产品，简化示例只列出部分属性且类型为string
public class Actor
{
    //角色类型
    public string Type { get; set; }
    //性别
    public string Sex { get; set; }
    //面容
    public string Face { get; set; }
    //服装
    public string Costume { get; set; }
    //发型
    public string HairStyle { get; set; }
}
#endregion

#region ActorBuilder：游戏角色建造器，充当抽象建造者
public abstract class ActorBuilder
{
    protected Actor actor = new Actor();

    public abstract void BuildType();
    public abstract void BuildSex();
    public abstract void BuildFace();
    public abstract void BuildCostume();
    public abstract void BuildHairStyle();

    // 工厂方法 ： 返回一个完整的游戏角色对象
    public Actor CreateActor()
    {
        return actor;
    }
}
#endregion

#region HeroBuilder、AngelBuilder和DevilBuilder：英雄角色、天使角色、魔鬼角色，充当具体建造者。
public class HeroBuilder : ActorBuilder
{
    public override void BuildType()
    {
        actor.Type = "英雄";
    }
    public override void BuildSex()
    {
        actor.Sex = "男";
    }
    public override void BuildFace()
    {
        actor.Face = "英俊";
    }
    public override void BuildCostume()
    {
        actor.Costume = "盔甲";
    }
    public override void BuildHairStyle()
    {
        actor.HairStyle = "飘逸";
    }
}
public class AngelBuilder : ActorBuilder
{
    public override void BuildType()
    {
        actor.Type = "天使";
    }
    public override void BuildSex()
    {
        actor.Sex = "女";
    }
    public override void BuildFace()
    {
        actor.Face = "漂亮";
    }
    public override void BuildCostume()
    {
        actor.Costume = "白裙";
    }
    public override void BuildHairStyle()
    {
        actor.HairStyle = "披肩长发";
    }
}
public class DevilBuilder : ActorBuilder
{
    public override void BuildType()
    {
        actor.Type = "恶魔";
    }
    public override void BuildSex()
    {
        actor.Sex = "妖";
    }
    public override void BuildFace()
    {
        actor.Face = "丑陋";
    }
    public override void BuildCostume()
    {
        actor.Costume = "黑衣";
    }
    public override void BuildHairStyle()
    {
        actor.HairStyle = "光头";
    }
}
#endregion

#region ActorController：角色控制器，充当指挥者
public class ActorController
{
    public Actor Construct(ActorBuilder builder)
    {
        builder.BuildType();
        builder.BuildSex();
        builder.BuildFace();
        builder.BuildCostume();
        builder.BuildHairStyle();

        return builder.CreateActor(); ;
    }
}
#endregion