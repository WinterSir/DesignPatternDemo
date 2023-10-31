//外观模式
using System.Text;

#region 测试代码
EncryptFacade facade = new EncryptFacade();
facade.FileEncrypt("src.txt", "des.txt");
Console.ReadLine();
#endregion

#region FileReader、CipherMachie、FileWriter：文件读取类、数据加密类、文件保存类，充当子系统类
public class FileReader
{
    public string Read(string fileNameSrc)
    {
        Console.WriteLine("读取文件，获取明文：");
        FileStream fs = null;
        StringBuilder sb = new StringBuilder();
        try
        {
            fs = new FileStream(fileNameSrc, FileMode.Open);
            int data;
            while ((data = fs.ReadByte()) != -1)
            {
                sb.Append((char)data);
            }
            fs.Close();
            Console.WriteLine(sb.ToString());
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("文件不存在");
        }
        catch (IOException e)
        {
            Console.WriteLine("文件操作错误");
        }
        return sb.ToString();
    }
}

public class CipherMachine
{
    public string Encrypt(string plainText)
    {
        Console.WriteLine("数据加密，将明文转换为密文：");
        string es = "";
        char[] chars = plainText.ToCharArray();
        foreach (char ch in chars)
        {
            string c = (ch % 7).ToString();
            es += c;
        }
        Console.WriteLine(es);
        return es;
    }
}

public class FileWriter
{
    public void Write(string encryptedStr, string fileNameDes)
    {
        Console.WriteLine("保存密文，写入文件");
        FileStream fs = null;
        StringBuilder sb = new StringBuilder();
        try
        {
            fs = new FileStream(fileNameDes, FileMode.Create);
            byte[] str = Encoding.Default.GetBytes(encryptedStr);
            fs.Write(str, 0, str.Length);
            fs.Flush();
            fs.Close();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("文件不存在");
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("文件操作错误");
        }
    }
}
#endregion

#region EncrytFacade：外观类
public class EncryptFacade
{
    private FileReader reader;
    private CipherMachine cipher;
    private FileWriter writer;

    public EncryptFacade()
    {
        reader = new FileReader();
        cipher = new CipherMachine();
        writer = new FileWriter();
    }

    public void FileEncrypt(string fileNameSrc, string fileNameDes)
    {
        string plainStr = reader.Read(fileNameSrc);
        string encryptedStr = cipher.Encrypt(plainStr);
        writer.Write(encryptedStr, fileNameDes);
    }
}
#endregion
