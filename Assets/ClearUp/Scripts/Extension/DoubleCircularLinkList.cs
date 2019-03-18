using System;
public class DoubleCircularLinkList<T>
{
    //表头
    private readonly DoubleCircularLinkedListNode<T> mLinkHead;
    //节点个数
    private int mSize;
    public DoubleCircularLinkList()
    {
        mLinkHead = new DoubleCircularLinkedListNode<T>(default(T));//双向链表 
        mLinkHead.Preview = mLinkHead;
        mLinkHead.Next = mLinkHead;
        mSize = 0;
    }
    /// <summary>
    /// 获取链表的容量
    /// </summary>
    /// <returns></returns>
    public int GetSize() { return mSize; }
    /// <summary>
    /// 双链表是否为空
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() { return (mSize == 0); }
    //通过索引查找，内部用
    private DoubleCircularLinkedListNode<T> GetNodeByInde(int index)
    {
        if (index < 0 || index >= mSize)
            UnityEngine.Debug.Log("索引溢出或者链表为空");
            //throw new IndexOutOfRangeException("索引溢出或者链表为空");
        if (index < mSize / 2)//正向查找,向后
        {
            DoubleCircularLinkedListNode<T> node = mLinkHead.Next;
            for (int i = 0; i < index; i++)
                node = node.Next;
            return node;
        }
        //反向查找，向前
        DoubleCircularLinkedListNode<T> rnode = mLinkHead.Preview;
        int rindex = mSize - index - 1;
        for (int i = 0; i < rindex; i++)
            rnode = rnode.Preview;
        return rnode;
    }
    /// <summary>
    /// 通过索引查找对象
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T Get(int index) { return GetNodeByInde(index).Value; }
    /// <summary>
    /// 获取第一个对象
    /// </summary>
    /// <returns></returns>
    public T GetFirst() { return GetNodeByInde(0).Value; }
    /// <summary>
    /// 获取最后一个对象
    /// </summary>
    /// <returns></returns>
    public T GetLast() { return GetNodeByInde(mSize - 1).Value; }
    /// 寻找目标对象所在的节点
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public DoubleCircularLinkedListNode<T> Find(T value)
    {
        if (IsEmpty())
            UnityEngine.Debug.Log("索引溢出或者链表为空");
            //throw new IndexOutOfRangeException("索引溢出或者链表为空");
        DoubleCircularLinkedListNode<T> node = mLinkHead.Next;
        for (int i = 0; i < mSize - 1; i++)
        {
            if (node.Value.GetHashCode() == value.GetHashCode())
            {
                return node;
            }
            node = node.Next;
        }            
        return null;
    }
    /// <summary>
    /// 清除所有节点
    /// </summary>
    public void Clear()
    {
        mLinkHead.Next = mLinkHead;
        mLinkHead.Preview = mLinkHead;
        mSize = 0;
    }
    /// <summary>
    /// 是否包含目标对象
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool Contains(T value)
    {
        if (Find(value) != null) return true;
        else { return false; }
    }

    /// <summary>
    /// 在下标索引前增加一个对象
    /// </summary>
    /// <param name="index"></param>
    /// <param name="t"></param>
    public void Insert(int index, T t)
    {
        if (mSize < 1 || index >= mSize)
            throw new Exception("没有可插入的点或者索引溢出了");
        if (index == 0)
            Append(mSize, t);
        else
        {
            DoubleCircularLinkedListNode<T> inode = GetNodeByInde(index);
            DoubleCircularLinkedListNode<T> tnode = new DoubleCircularLinkedListNode<T>(t, inode.Preview, inode);
            inode.Preview.Next = tnode;
            inode.Preview = tnode;
            mSize++;
        }
    }
    /// <summary>
    /// 在下标索引后增加一个对象
    /// </summary>
    /// <param name="index">目标索引</param>
    /// <param name="t">目标对象</param>
    public void Append(int index, T t)
    {
        DoubleCircularLinkedListNode<T> inode;
        if (index == 0)
            inode = mLinkHead;
        else
        {
            index = index - 1;
            if (index < 0)
                throw new IndexOutOfRangeException("位置不存在");
            inode = GetNodeByInde(index);
        }
        DoubleCircularLinkedListNode<T> tnode = new DoubleCircularLinkedListNode<T>(t, inode, inode.Next);
        inode.Next.Preview = tnode;
        inode.Next = tnode;
        mSize++;
    }
    /// <summary>
    /// 在某个节点后增加对象
    /// </summary>
    /// <param name="node">目标节点</param>
    /// <param name="value">往后增加的对象</param>
    /// <returns></returns>
    public DoubleCircularLinkedListNode<T> AddAfter(DoubleCircularLinkedListNode<T> node, T value)
    {
        DoubleCircularLinkedListNode<T> DoubleCircularLinkedListNode = new DoubleCircularLinkedListNode<T>(value);
        AddAfter(node, DoubleCircularLinkedListNode);
        return DoubleCircularLinkedListNode;
    }
    /// <summary>
    /// 在某个节点后增加节点
    /// </summary>
    /// <param name="node">当前项</param>
    /// <param name="newNode">后一项</param>
    public void AddAfter(DoubleCircularLinkedListNode<T> node, DoubleCircularLinkedListNode<T> newNode)
    {
        node.Next.Preview = newNode;
        newNode.Next = node.Next;
        node.Next = newNode;
        newNode.Preview = node;
        mSize++;
    }
    /// <summary>
    /// 在某个节点前增加对象
    /// </summary>
    /// <param name="node">目标节点</param>
    /// <param name="value">往前增加的对象</param>
    /// <returns></returns>
    public DoubleCircularLinkedListNode<T> AddBefore(DoubleCircularLinkedListNode<T> node, T value)
    {
        DoubleCircularLinkedListNode<T> dclnode = new DoubleCircularLinkedListNode<T>(value);
        AddBefore(node, dclnode);
        return dclnode;
    }
    /// <summary>
    /// 某个节点在某个节点前增加
    /// </summary>
    /// <param name="node">当前项</param>
    /// <param name="newNode">前一项</param>
    public void AddBefore(DoubleCircularLinkedListNode<T> node, DoubleCircularLinkedListNode<T> newNode)
    {
        node.Preview.Next = newNode;
        newNode.Preview = node.Preview;
        node.Preview = newNode;
        newNode.Next = node;
        mSize++;
    }
    /// <summary>
    /// 在第一个对象前增加节点
    /// </summary>
    /// <param name="node">目标节点</param>
    public void AddFirst(DoubleCircularLinkedListNode<T> node)
    {
        DoubleCircularLinkedListNode<T> firstNode = GetNodeByInde(0);
        AddBefore(firstNode, node);
    }
    /// <summary>
    /// 在最后一个节点后增加对象
    /// </summary>
    /// <param name="node"></param>
    public void AddLast(DoubleCircularLinkedListNode<T> node)
    {
        DoubleCircularLinkedListNode<T> laseNode = GetNodeByInde(mSize - 1);
        AddAfter(laseNode, node);
    }
    /// <summary>
    /// 通过下标索引删除节点
    /// </summary>
    /// <param name="index"></param>
    public void DelByIndex(int index)
    {
        DoubleCircularLinkedListNode<T> inode = GetNodeByInde(index);
        inode.Preview.Next = inode.Next;
        inode.Next.Preview = inode.Preview;
        mSize--;
    }
    /// <summary>
    /// 删除头节点
    /// </summary>
    public void DelFirst() { DelByIndex(0); }
    /// <summary>
    /// 删除尾节点
    /// </summary>
    public void DelLast() { DelByIndex(mSize - 1); }
    /// <summary>
    /// 移除一个对象
    /// </summary>
    /// <param name="value">目标对象</param>
    /// <returns></returns>
    public bool Remove(T value)
    {
        DoubleCircularLinkedListNode<T> node = Find(value);
        if (node == null) { return false; }
        else
        {
            Remove(node);
            return true;
        }
    }
    /// <summary>
    /// 移除一个节点
    /// </summary>
    /// <param name="node">目标节点</param>
    public void Remove(DoubleCircularLinkedListNode<T> node)
    {
        node.Preview.Next = node.Next;
        node.Next.Preview = node.Preview;
        mSize--;
    }
    public void ShowAll()
    {
        Console.WriteLine("******************* 链表数据如下 *******************");
        for (int i = 0; i < mSize; i++)
            Console.WriteLine("(" + i + ")=" + Get(i));
        Console.WriteLine("******************* 链表数据展示完毕 *******************\n");
    }
}
