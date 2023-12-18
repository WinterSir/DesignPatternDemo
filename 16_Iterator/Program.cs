//迭代器模式

#region 测试代码
IList<object> products = new List<object>();
products.Add("倚天剑");
products.Add("屠龙刀");
products.Add("断肠草");
products.Add("葵花宝典");
products.Add("四十二章经");

AbstractObjectList objectList = new ProductList(products);      // 创建聚合对象
AbstractIterator iterator = objectList.CreateIterator();        // 创建迭代器对象

Console.WriteLine("正向遍历");
while (!iterator.IsLast())
{
    Console.Write(iterator.GetNextItem() + ",");
    iterator.Next();
}

Console.WriteLine();
Console.WriteLine("-------------------------------------------------------");
Console.WriteLine("逆向遍历");
while (!iterator.IsFirst())
{
    Console.Write(iterator.GetPreviousItem() + ",");
    iterator.Previous();
}
Console.ReadLine();
#endregion

#region AbstractObjectList：抽象聚合类
public abstract class AbstractObjectList
{
    protected IList<object> objectList = new List<object>();

    public AbstractObjectList(IList<object> objectList)
    {
        this.objectList = objectList;
    }

    public void AddObject(object obj)
    {
        this.objectList.Add(obj);
    }

    public void RemoveObject(object obj)
    {
        this.objectList.Remove(obj);
    }

    public IList<Object> GetObjectList()
    {
        return this.objectList;
    }

    // 声明创建迭代器对象的抽象工厂方法
    public abstract AbstractIterator CreateIterator();
}
#endregion

#region ProductList、ProductIterator：具体聚合类、具体迭代器，具体迭代器是具体聚合类的内部类
public class ProductList : AbstractObjectList
{
    public ProductList(IList<object> objectList) : base(objectList)
    {
    }

    public override AbstractIterator CreateIterator()
    {
        return new ProductIterator(this);
    }

    private class ProductIterator : AbstractIterator
    {
        private ProductList productList;
        private IList<object> products;
        private int cursor1;    // 定义一个游标，用于记录正向遍历的位置
        private int cursor2;    // 定义一个游标，用于记录逆向遍历的位置

        public ProductIterator(ProductList productList)
        {
            this.productList = productList;
            this.products = productList.GetObjectList();       // 获取集合对象
            this.cursor1 = 0;                                  // 设置正向遍历游标的初始值
            this.cursor2 = this.products.Count - 1;            // 设置逆向遍历游标的初始值
        }

        public object GetNextItem()
        {
            return products[cursor1];
        }

        public object GetPreviousItem()
        {
            return products[cursor2];
        }

        public bool IsFirst()
        {
            return cursor2 == -1;
        }

        public bool IsLast()
        {
            return cursor1 == products.Count;
        }

        public void Next()
        {
            if (cursor1 < products.Count)
            {
                cursor1++;
            }
        }

        public void Previous()
        {
            if (cursor2 > -1)
            {
                cursor2--;
            }
        }
    }
}
#endregion

#region AbstractIterator：抽象迭代器
public interface AbstractIterator
{
    void Next();               // 移动至下一个元素
    bool IsLast();             // 判断是否为最后一个元素
    void Previous();           // 移动至上一个元素
    bool IsFirst();            // 判断是否为第一个元素
    object GetNextItem();      // 获取下一个元素
    object GetPreviousItem();  // 获取上一个元素
}
#endregion