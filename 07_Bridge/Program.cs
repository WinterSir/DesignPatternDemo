//桥接模式

#region 测试代码
Matrix m = new Matrix();
ImageImplementor wi = new WindowsImplementor();
Image ji = new JPGImage();
ji.SetImageImplementor(wi);
ji.ParstFile("小龙女");
Console.ReadLine();
#endregion

#region Matrix：像素矩阵类，辅助类，各种格式的图像文件最终都会被转化为像素矩阵，不同的操作系统提供不同的方式显示像素矩阵
public class Matrix
{
    //省略实现代码
} 
#endregion

#region Image：抽象图像类，充当抽象类
public abstract class Image
{
    protected ImageImplementor imageImpl;

    public void SetImageImplementor(ImageImplementor imageImpl)
    {
        this.imageImpl = imageImpl;
    }

    public abstract void ParstFile(string fileName);
}
#endregion

#region JPGImage、BMPImage、GIFImage：扩充抽象类

public class JPGImage : Image
{
    public override void ParstFile(string fileName)
    {
        // 模拟解析JPG文件并获得一个像素矩阵对象m
        Matrix m = new Matrix();
        imageImpl.DoPaint(m);
        Console.WriteLine("{0} : 格式为JPG", fileName);
    }
}

public class BMPImage : Image
{
    public override void ParstFile(string fileName)
    {
        // 模拟解析BMP文件并获得一个像素矩阵对象m
        Matrix m = new Matrix();
        imageImpl.DoPaint(m);
        Console.WriteLine("{0} : 格式为BMP", fileName);
    }
}

public class GIFImage : Image
{
    public override void ParstFile(string fileName)
    {
        // 模拟解析GIF文件并获得一个像素矩阵对象m
        Matrix m = new Matrix();
        imageImpl.DoPaint(m);
        Console.WriteLine("{0} : 格式为GIF", fileName);
    }
}

#endregion

#region ImageImplementor：抽象操作系统实现类
public interface ImageImplementor
{
    // 显示像素矩阵
    void DoPaint(Matrix m);
}
#endregion

#region WindowsImplementor、LinuxImplementor、UnixImplementor：具体实现类
public class WindowsImplementor : ImageImplementor
{
    public void DoPaint(Matrix m)
    {
        // 调用Windows的绘制函数绘制像素矩阵
        Console.WriteLine("在Windows系统中显示图像");
    }
}
public class LinuxImplementor : ImageImplementor
{
    public void DoPaint(Matrix m)
    {
        // 调用Linux的绘制函数绘制像素矩阵
        Console.WriteLine("在Linux系统中显示图像");
    }
}
public class UnixImplementor : ImageImplementor
{
    public void DoPaint(Matrix m)
    {
        // 调用Unix的绘制函数绘制像素矩阵
        Console.WriteLine("在Unix系统中显示图像");
    }
}
#endregion
