//组合模式

#region 测试代码
AbstractFile folder1 = new Folder("EDC的资料");
AbstractFile folder2 = new Folder("图像文件");
AbstractFile folder3 = new Folder("文本文件");
AbstractFile folder4 = new Folder("视频文件");

AbstractFile image1 = new ImageFile("小龙女.jpg");
AbstractFile image2 = new ImageFile("张无忌.gif");

AbstractFile text1 = new TextFile("九阴真经.txt");
AbstractFile text2 = new TextFile("葵花宝典.doc");

AbstractFile video1 = new VideoFile("笑傲江湖.rmvb");
AbstractFile video2 = new VideoFile("天龙八部.mp4");

folder2.Add(image1);
folder2.Add(image2);

folder3.Add(text1);
folder3.Add(text2);

folder4.Add(video1);
folder4.Add(video2);

folder1.Add(folder2);
folder1.Add(folder3);
folder1.Add(folder4);

folder1.KillVirus();

Console.ReadLine();
#endregion

#region AbstractFile：抽象文件类，充当抽象构件
public abstract class AbstractFile
{
    public abstract void Add(AbstractFile file);
    public abstract void Remove(AbstractFile file);
    public abstract AbstractFile GetChild(int index);
    public abstract void KillVirus();
}
#endregion

#region ImageFile、VideoFile、TextFile：图像文件、视频文件、文本文件类，充当叶子构件
public class ImageFile : AbstractFile
{
    private string name;

    public ImageFile(string name)
    {
        this.name = name;
    }

    public override void Add(AbstractFile file)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
    }

    public override void Remove(AbstractFile file)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
    }

    public override AbstractFile GetChild(int index)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
        return null;
    }

    public override void KillVirus()
    {
        // 此处模拟杀毒操作
        Console.WriteLine("**** 对图像文件‘{0}’进行杀毒", name);
    }
}

public class TextFile : AbstractFile
{
    private string name;

    public TextFile(string name)
    {
        this.name = name;
    }

    public override void Add(AbstractFile file)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
    }

    public override void Remove(AbstractFile file)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
    }

    public override AbstractFile GetChild(int index)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
        return null;
    }

    public override void KillVirus()
    {
        // 此处模拟杀毒操作
        Console.WriteLine("**** 对文本文件‘{0}’进行杀毒", name);
    }
}

public class VideoFile : AbstractFile
{
    private string name;

    public VideoFile(string name)
    {
        this.name = name;
    }

    public override void Add(AbstractFile file)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
    }

    public override void Remove(AbstractFile file)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
    }

    public override AbstractFile GetChild(int index)
    {
        Console.WriteLine("对不起，系统不支持该方法！");
        return null;
    }

    public override void KillVirus()
    {
        // 此处模拟杀毒操作
        Console.WriteLine("**** 对视频文件‘{0}’进行杀毒", name);
    }
}
#endregion

#region Folder：文件夹类，充当容器构件
public class Folder : AbstractFile
{
    private IList<AbstractFile> fileList = new List<AbstractFile>();
    private string name;

    public Folder(string name)
    {
        this.name = name;
    }

    public override void Add(AbstractFile file)
    {
        fileList.Add(file);
    }

    public override void Remove(AbstractFile file)
    {
        fileList.Remove(file);
    }

    public override AbstractFile GetChild(int index)
    {
        return fileList[index];
    }

    public override void KillVirus()
    {
        // 此处模拟杀毒操作
        Console.WriteLine("---- 对文件夹‘{0}’进行杀毒", name);

        foreach (var item in fileList)
        {
            item.KillVirus();
        }
    }
}
#endregion
