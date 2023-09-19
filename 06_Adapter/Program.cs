//适配器模式

#region 测试代码
IScoreOperation operation = new OperationAdapter();
if (operation == null)
{
    return;
}

int[] scores = { 84, 76, 50, 69, 90, 91, 88, 96 };
int[] result;
int score;

Console.WriteLine("测试成绩排序结果：");
result = operation.Sort(scores);
foreach (int s in result)
{
    Console.Write("{0},", s.ToString());
}
Console.WriteLine();

Console.WriteLine("查找是否有90分的人：");
score = operation.Search(scores, 90);
if (score == -1)
{
    Console.WriteLine("抱歉，这个真没找到~~~");
}
else
{
    Console.WriteLine("恭喜，的确存在90分选手~~~");
}

Console.WriteLine("查找是否有92分的人：");
score = operation.Search(scores, 92);
if (score == -1)
{
    Console.WriteLine("抱歉，这个真没找到~~~");
}
else
{
    Console.WriteLine("恭喜，的确存在92分选手~~~");
}
Console.ReadLine(); 
#endregion

#region IScoreOperation：抽象成绩操作接口，充当目标抽象类
public interface IScoreOperation
{
    int[] Sort(int[] array);
    int Search(int[] array, int key);
}
#endregion

#region QuickSortHelper、BinarySearchHelper：快速排序算法类、二分查找算法类，充当适配者类

public class QuickSortHelper
{
    public int[] QuickSort(int[] array)
    {
        Sort(array, 0, array.Length - 1);
        return array;
    }

    public void Sort(int[] array, int p, int r)
    {
        int q = 0;
        if (p < r)
        {
            q = Partition(array, p, r);
            Sort(array, p, q - 1);
            Sort(array, q + 1, r);
        }
    }

    public int Partition(int[] array, int p, int r)
    {
        int x = array[r];
        int j = p - 1;

        for (int i = p; i <= r - 1; i++)
        {
            if (array[i] <= x)
            {
                j++;
                Swap(array, j, i);
            }
        }

        Swap(array, j + 1, r);
        return j + 1;
    }

    public void Swap(int[] array, int i, int j)
    {
        int t = array[i];
        array[i] = array[j];
        array[j] = t;
    }
}

public class BinarySearchHelper
{
    public int BinarySearch(int[] array, int key)
    {
        int low = 0;
        int high = array.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            int midVal = array[mid];

            if (midVal < key)
            {
                low = mid + 1;
            }
            else if (midVal > key)
            {
                high = mid - 1;
            }
            else
            {
                return 1;   // 找到元素返回1
            }
        }

        return -1;  // 未找到元素返回-1
    }
}
#endregion

#region OperationAdapter：成绩操作类，充当适配器类
public class OperationAdapter : IScoreOperation
{
    private QuickSortHelper sortTarget;
    private BinarySearchHelper searchTarget;

    public OperationAdapter()
    {
        sortTarget = new QuickSortHelper();
        searchTarget = new BinarySearchHelper();
    }

    public int Search(int[] array, int key)
    {
        return searchTarget.BinarySearch(array, key);
    }

    public int[] Sort(int[] array)
    {
        return sortTarget.QuickSort(array);
    }
}
#endregion