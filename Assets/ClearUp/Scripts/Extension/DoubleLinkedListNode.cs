public class DoubleLinkedListNode<T>
{
    //前驱
    private DoubleLinkedListNode<T> mNextDoubleLinkedListNode;
    private DoubleLinkedListNode<T> mPrevDoubleLinkedListNode;
    private DoubleLinkedList<T> mDoubleLinkedList;

    //数据
    private T dataValue;

    /// <summary>
    ///     无参构造函数 该节点只有默认值，前驱和后继都 ——>null
    /// </summary>
    public DoubleLinkedListNode(DoubleLinkedList<T> doubleLinkedList)
    {
        this.dataValue = default(T);
        this.mNextDoubleLinkedListNode = null;
        this.mPrevDoubleLinkedListNode = null;
        this.mDoubleLinkedList = doubleLinkedList;
    }

    /// <summary>
    ///     构造方法：实例化该节点只有传入的data域，前驱和后继都指向null
    /// </summary>
    /// <param name="value"></param>
    public DoubleLinkedListNode(T value, DoubleLinkedList<T> doubleLinkedList)
    {
        this.mPrevDoubleLinkedListNode = null;
        this.dataValue = value;
        this.mNextDoubleLinkedListNode = null;
        this.mDoubleLinkedList = doubleLinkedList;
    }

    /// <summary>
    ///     构造函数：实例化一个正常的Node 数据域 前驱后继都有值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="prev"></param>
    /// <param name="next"></param>
    public DoubleLinkedListNode(T value, DoubleLinkedListNode<T> prev, DoubleLinkedListNode<T> next, DoubleLinkedList<T> doubleLinkedList)
    {
        this.mPrevDoubleLinkedListNode = prev;
        this.dataValue = value;
        this.mNextDoubleLinkedListNode = next;
        this.mDoubleLinkedList = doubleLinkedList;
    }

    public DoubleLinkedListNode<T> PrevNode
    {
        get { return this.mPrevDoubleLinkedListNode; }
        set { this.mPrevDoubleLinkedListNode = value; }
    }

    public T DataValue
    {
        get { return this.dataValue; }
        set { this.dataValue = value; }
    }

    public DoubleLinkedListNode<T> NextNode
    {
        get { return this.mNextDoubleLinkedListNode; }
        set { this.mNextDoubleLinkedListNode = value; }
    }

    public DoubleLinkedList<T> List
    {
        get { return this.mDoubleLinkedList; }
    }
    /// <summary>
    ///     Show 方法，调试用
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        T p = this.mPrevDoubleLinkedListNode == null ? default(T) : this.mPrevDoubleLinkedListNode.dataValue;
        T n = this.mNextDoubleLinkedListNode == null ? default(T) : this.mNextDoubleLinkedListNode.dataValue;
        string s = string.Format("Data:{0},Prev:{1},Next:{2}", this.dataValue, p, n);
        return s;
    }
}
