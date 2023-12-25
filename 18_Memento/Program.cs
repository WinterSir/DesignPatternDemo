//备忘录模式

#region 测试代码
int index = -1;
MementoCaretaker mementoCaretaker = new MementoCaretaker();

Chessman chess = new Chessman("车", 1, 1);
Play(chess);
chess.Y = 4;
Play(chess);
chess.X = 5;
Play(chess);

Undo(chess, index);
Undo(chess, index);
Redo(chess, index);
Redo(chess, index);

// 下棋
void Play(Chessman chess)
{
    // 保存备忘录
    mementoCaretaker.SetMemento(chess.Save());
    index++;

    Console.WriteLine("棋子 {0} 当前位置为 第 {1} 行 第 {2} 列", chess.Label, chess.X, chess.Y);
}

// 悔棋
void Undo(Chessman chess, int i)
{
    Console.WriteLine("---------- Sorry，俺悔棋了 ---------");
    index--;
    // 撤销到上一个备忘录
    chess.Restore(mementoCaretaker.GetMemento(i - 1));

    Console.WriteLine("棋子 {0} 当前位置为 第 {1} 行 第 {2} 列", chess.Label, chess.X, chess.Y);
}

// 撤销悔棋
void Redo(Chessman chess, int i)
{
    Console.WriteLine("---------- Sorry，撤销悔棋 ---------");
    index++;
    // 恢复到下一个备忘录
    chess.Restore(mementoCaretaker.GetMemento(i + 1));

    Console.WriteLine("棋子 {0} 当前位置为 第 {1} 行 第 {2} 列", chess.Label, chess.X, chess.Y);
}
Console.ReadLine();
#endregion

#region Chessman：原发器
public class Chessman
{
    public string Label { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Chessman(string label, int x, int y)
    {
        Label = label;
        X = x;
        Y = y;
    }

    // 保存状态
    public ChessmanMemento Save()
    {
        return new ChessmanMemento(Label, X, Y);
    }

    // 恢复状态
    public void Restore(ChessmanMemento memento)
    {
        Label = memento.Label;
        X = memento.X;
        Y = memento.Y;
    }
}
#endregion

#region ChessmanMemento：备忘录
public class ChessmanMemento
{
    public string Label { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public ChessmanMemento(string label, int x, int y)
    {
        Label = label;
        X = x;
        Y = y;
    }
}
#endregion

#region MementoCaretaker：负责人
public class MementoCaretaker
{
    private IList<ChessmanMemento> mementoList = new List<ChessmanMemento>();

    public ChessmanMemento GetMemento(int i)
    {
        return mementoList[i];
    }

    public void SetMemento(ChessmanMemento memento)
    {
        mementoList.Add(memento);
    }
}
#endregion