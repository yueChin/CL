using UnityEngine;
using System;

public class DoubleLinkedList<T> : ILinkedList<T>
{
    //前驱


    //后置
    private DoubleLinkedListNode<T> mEndDoubleLinkedListNode;
    private DoubleLinkedListNode<T> mHeadDoubleLinkedListNode;

    //长度
    private int size;

    /// <summary>
    ///     判断链表是否是空的
    /// </summary>
    public bool IsEmpty
    {
        get { return this.mHeadDoubleLinkedListNode == null; }
    }

    /// <summary>
    ///     链表中元素的个数
    /// </summary>
    public int Count
    {
        get
        {
            int i = 0;
            DoubleLinkedListNode<T> node = this.mHeadDoubleLinkedListNode;
            while (node != null)
            {
                ++i;
                node = node.NextNode;
            }

            return i;

            //return this.size;
        }
    }

    /// <summary>
    ///     根据索引获取链表中的节点
    /// </summary>
    /// <param name="index">整型索引</param>
    /// <returns>节点</returns>
    public DoubleLinkedListNode<T> this[int index]
    {
        get
        {
            //链表头节点是空的
            if (this.mHeadDoubleLinkedListNode == null)
            {
                //throw new Exception("链表为空.");
                Debug.Log("链表为空.");
                return null; 
            }
            //索引过小
            if (index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            //索引过大
            if (index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
            //取得头节点
            var current = new DoubleLinkedListNode<T>(this);
            //current = head;
            //int i = 0;
            ////遍历链表
            //while (true)
            //{
            //    //找到第index个节点
            //    if (i == index)
            //    {
            //        break;
            //    }
            //    current = current.Next;
            //    i++;
            //}
            //return current;
            //如果索引在前一半，那么从前向后找
            if (index < this.size / 2)
            {
                current = this.mHeadDoubleLinkedListNode;
                int i = 0;
                //遍历链表
                while (true)
                {
                    //找到第index个节点
                    if (i == index)
                    {
                        break;
                    }
                    current = current.NextNode;
                    i++;
                }
                return current;
            }
            else //如果索引在后一半，那么从后向前找
            {
                current = this.mEndDoubleLinkedListNode;
                int i = this.size;
                //遍历链表
                while (true)
                {
                    //找到第index个节点
                    if (i == index)
                    {
                        break;
                    }
                    current = current.PrevNode;
                    i--;
                }
                return current.NextNode;
            }
        }
    }

    public DoubleLinkedListNode<T> HeadDoubleLinkedListNode
    {
        get { return this.mHeadDoubleLinkedListNode; }
        set { this.mHeadDoubleLinkedListNode = value; }
    }

    public DoubleLinkedListNode<T> EndDoubleLinkedListNode
    {
        get { return this.mEndDoubleLinkedListNode; }
        set { this.mEndDoubleLinkedListNode = value; }
    }

    public void AddAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
    {
        this.AddAfter(node, newNode.DataValue);
    }

    public void AddAfter(DoubleLinkedListNode<T> node, T t)
    {
        if (node == null || t == null)
        {
            //throw new Exception("元素为空，不可以进行插入.");
            Debug.Log("元素为空，不可以进行插入.");
            return;
        }
        int index = this.IndexOf(node.DataValue);

        if (index != -1)
        {
            DoubleLinkedListNode<T> currNode = this.GetNodeByIndex(index);
            DoubleLinkedListNode<T> upNode = currNode.PrevNode;
            DoubleLinkedListNode<T> nextNode = currNode.NextNode;
            var newNode = new DoubleLinkedListNode<T>(t,this);
            if (index == 0)
            {
                this.mHeadDoubleLinkedListNode.NextNode = newNode;
                newNode.PrevNode = this.mHeadDoubleLinkedListNode;
                newNode.NextNode = nextNode;
            }
            else if (index == this.Count - 1)
            {
                newNode.PrevNode = this.mEndDoubleLinkedListNode;
                this.mEndDoubleLinkedListNode.NextNode = newNode;
                this.mEndDoubleLinkedListNode = newNode;
            }
            else
            {
                nextNode.PrevNode = newNode;
                currNode.NextNode = newNode;
                newNode.PrevNode = currNode;
                newNode.NextNode = nextNode;
            }
        }
    }

    public void AddBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode)
    {
        this.AddBefore(node, newNode.DataValue);
    }

    public void AddBefore(DoubleLinkedListNode<T> node, T t)
    {
        if (node == null || t == null)
        {
            //throw new Exception("元素为空，不可以进行插入.");
            Debug.Log("元素为空，不可以进行插入.");
            return;

        }
        int index = this.IndexOf(node.DataValue);

        if (index != -1)
        {
            DoubleLinkedListNode<T> currNode = this.GetNodeByIndex(index);
            DoubleLinkedListNode<T> upNode = currNode.PrevNode;
            DoubleLinkedListNode<T> nextNode = currNode.NextNode;
            var newNode = new DoubleLinkedListNode<T>(t,this);
            if (index == 0)
            {
                this.mHeadDoubleLinkedListNode.PrevNode = newNode;
                newNode.NextNode = this.mHeadDoubleLinkedListNode;
                this.mHeadDoubleLinkedListNode = newNode;
            }
            else if (index == this.Count - 1)
            {
                upNode.NextNode = newNode;
                newNode.NextNode = this.mEndDoubleLinkedListNode;
                this.mEndDoubleLinkedListNode.PrevNode = newNode;
            }
            else
            {
                upNode.NextNode = newNode;
                nextNode.PrevNode = newNode;
                currNode.PrevNode = upNode;
                currNode.NextNode = nextNode;
            }
        }
    }

    /// <summary>
    ///     添加节点到链表的开头
    /// </summary>
    /// <param name="t"></param>
    public DoubleLinkedListNode<T> AddFirst(T value)
    {
        var doubleLinkedListNode = new DoubleLinkedListNode<T>(value,this);
        //如果头为null
        if (this.mHeadDoubleLinkedListNode == null)
        {
            //把头节点设置为node
            this.mHeadDoubleLinkedListNode = doubleLinkedListNode;
            //因为是空链表，所以头尾一致
            this.mEndDoubleLinkedListNode = doubleLinkedListNode;
            //大小加一
            this.size++;
            return doubleLinkedListNode;
        }
        //原来头节点的上一个为新节点
        this.mHeadDoubleLinkedListNode.PrevNode = doubleLinkedListNode;
        //新节点的下一个为原来的头节点
        doubleLinkedListNode.NextNode = this.mHeadDoubleLinkedListNode;
        //新头节点为新节点
        this.mHeadDoubleLinkedListNode = doubleLinkedListNode;
        //大小加一
        this.size++;
        return this.mHeadDoubleLinkedListNode;
    }

    public void AddFirst(DoubleLinkedListNode<T> node)
    {
        this.AddFirst(node.DataValue);
    }

    /// <summary>
    ///     添加节点到链表的末尾
    /// </summary>
    /// <param name="t">要添加的数据</param>
    public DoubleLinkedListNode<T> AddLast(T t)
    {
        var doubleLinkedListNode = new DoubleLinkedListNode<T>(t,this);
        //如果头为null
        if (this.mHeadDoubleLinkedListNode == null)
        {
            //把头节点设置为node
            this.mHeadDoubleLinkedListNode = doubleLinkedListNode;
            //因为是空链表，所以头尾一致
            this.mEndDoubleLinkedListNode = doubleLinkedListNode;
            //大小加一
            this.size++;
            return doubleLinkedListNode;
        }
        //将原尾节点的下一个设置为新节点
        this.mEndDoubleLinkedListNode.NextNode = doubleLinkedListNode;
        //将新节点的上一个设置为原尾节点
        doubleLinkedListNode.PrevNode = this.mEndDoubleLinkedListNode;
        //将尾节点重新设置为新节点
        this.mEndDoubleLinkedListNode = doubleLinkedListNode;
        //大小加一
        this.size++;

        return this.mEndDoubleLinkedListNode;
    }

    public void AddLast(DoubleLinkedListNode<T> node)
    {
        this.AddLast(node.DataValue);
    }

    /// <summary>
    ///     移除链表中的节点
    /// </summary>
    /// <param name="index">要移除的节点的索引</param>
    public bool Remove(DoubleLinkedListNode<T> node)
    {
        if (node == null)
        {
            //throw new Exception("节点不可以为空，无法进行删除.");
            Debug.Log("节点不可以为空，无法进行删除.");
            return false;
        }
        int index = this.IndexOf(node.DataValue);
        return this.Remove(index);
    }

    public bool Remove(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            //throw new Exception("索引值非法，无法进行删除.");
            Debug.Log("索引值非法，无法进行删除.");
            return false;
        }
        bool flag = false;
        DoubleLinkedListNode<T> node = this.mHeadDoubleLinkedListNode;
        int i = 0;
        while (node != null)
        {
            if (i == index)
            {
                //DoubleLinkedListNode<T> upNode = node.PrevNode;
                //DoubleLinkedListNode<T> nextNode = node.NextNode;
                if (index == 0)
                {
                    this.mHeadDoubleLinkedListNode = node.NextNode;
                    node = null;
                }
                else if (index == this.Count - 1)
                {
                    this.mEndDoubleLinkedListNode = node.PrevNode;
                    this.mEndDoubleLinkedListNode.NextNode = null;
                    node = null;
                }
                else
                {
                    node.PrevNode.NextNode = node.NextNode;
                    node.NextNode.PrevNode = node.PrevNode;
                    node = null;
                }
                flag = true;
                this.size--;
                break;
            }
            node = node.NextNode;
            ++i;
        }
        return flag;
    }

    /// <summary>
    ///     移除头节点
    /// </summary>
    public void RemoveHeadNode()
    {
        //链表头节点是空的
        if (this.IsEmpty)
        {
            //throw new Exception("链表为空.");
            Debug.Log("链表为空.");
            return;
        }
        //如果size为1，那就是清空链表。
        if (this.size == 1)
        {
            this.Clear();
            return;
        }
        //将头节点设为原头结点的下一个节点，就是下一个节点上移
        this.mHeadDoubleLinkedListNode = this.mHeadDoubleLinkedListNode.NextNode;
        //处理上一步遗留问题，原来的第二个节点的上一个是头结点，现在第二个要变成头节点，那要把它的Prev设为null才能成为头节点
        this.mHeadDoubleLinkedListNode.PrevNode = null;
        //大小减一
        this.size--;
    }

    /// <summary>
    ///     移除尾节点
    /// </summary>
    public void RemoveEndNode()
    {
        //链表头节点是空的
        if (this.IsEmpty)
        {
            //throw new Exception("链表为空.");
            Debug.Log("链表为空.");
            return;
        }
        //如果size为1，那就是清空链表。
        if (this.size == 1)
        {
            this.Clear();
            return;
        }
        //尾节点设置为倒数第二个节点
        this.mEndDoubleLinkedListNode = this.mEndDoubleLinkedListNode.PrevNode;
        //将新尾节点的Next设为null，表示它是新的尾节点
        this.mEndDoubleLinkedListNode.NextNode = null;
        //大小减一
        this.size--;
    }

    public DoubleLinkedListNode<T> Find(T value)
    {
        int index = this.IndexOf(value);
        return this[index];
    }

    public DoubleLinkedListNode<T> FindLast(T value)
    {
        int index = this.IndexOf(value);
        return this[index];
    }

    public int IndexOf(T item)
    {
        if (item == null || this.Count == 0 || this.mHeadDoubleLinkedListNode == null)
        {
            //throw new Exception("无法得到元素索引.");
            Debug.Log("无法得到元素索引.");
            return -1;
        }
        int reInt = -1;
        int i = 0;
        DoubleLinkedListNode<T> node = this.mHeadDoubleLinkedListNode;
        while (node != null)
        {
            if (node.DataValue.Equals(item))
            {
                reInt = i;
                break;
            }
            ++i;
            node = node.NextNode;
        }
        return reInt;
    }

    public T GetElementByIndex(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            //throw new Exception("索引值非法,无法得对象.");
            Debug.Log("索引值非法,无法得对象.");
            return default(T);
        }
        DoubleLinkedListNode<T> reNode = this.mHeadDoubleLinkedListNode;
        int i = 0;
        while (reNode != null)
        {
            if (i == index)
            {
                break;
            }
            reNode = reNode.NextNode;
            ++i;
        }
        return reNode.DataValue;
    }

    public DoubleLinkedListNode<T> GetNodeByIndex(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            //throw new Exception("索引值非法,无法得对象节点.");
            Debug.Log("索引值非法,无法得对象节点.");
            return null;
        }
        DoubleLinkedListNode<T> reNode = this.mHeadDoubleLinkedListNode;
        int i = 0;
        while (reNode != null)
        {
            if (i == index)
            {
                break;
            }
            reNode = reNode.NextNode;
            ++i;
        }
        return reNode;
    }

    /// <summary>
    ///     清除链表中的数据
    /// </summary>
    public void Clear()
    {
        this.mHeadDoubleLinkedListNode = null;
        this.mEndDoubleLinkedListNode = null;
        this.size = 0;
    }

    /// <summary>
    ///     查找是否包含当前元素，true为包含 false为不包含
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool Contains(T item)
    {
        if (item == null)
        {
            //throw new Exception("元素为空,无法检测对象是否存在与链表中.");
            Debug.Log("元素为空,无法检测对象是否存在与链表中.");
            return false;
        }
        bool flag = false;
        if (this.Count != 0)
        {
            DoubleLinkedListNode<T> node = this.mHeadDoubleLinkedListNode;
            while (node != null)
            {
                if (node.DataValue.Equals(item))
                {
                    flag = true;
                    break;
                }
                node = node.NextNode;
            }
        }
        return flag;
    }

    /// <summary>
    ///     初始化链表添加节点
    /// </summary>
    /// <param name="item"></param>
    public void Add(T item)
    {
        if (item == null)
        {
            //throw new Exception("元素为空,元法进行添加!");
            Debug.Log("元素为空,元法进行添加!");
            return;
        }
        //要添加的节点
        var addItem = new DoubleLinkedListNode<T>(item,this);

        //把新节点添加到链表的表尾
        if (this.mHeadDoubleLinkedListNode == null)
        {
            //如果没有过行过添加
            this.mHeadDoubleLinkedListNode = addItem;
            //第一次添加时,头尾指针都应是一个对象
            this.mEndDoubleLinkedListNode = this.mHeadDoubleLinkedListNode;
        }
        else
        {
            //如果链表中不为空
            this.mEndDoubleLinkedListNode.NextNode = addItem;
            addItem.PrevNode = this.mEndDoubleLinkedListNode;
            this.mEndDoubleLinkedListNode = addItem;
        }
    }

    /// <summary>
    ///     在给定的索引处插入数据
    /// </summary>
    /// <param name="index">索引</param>
    /// <param name="t">要插入的数据</param>
    public void Insert(int index, T t)
    {
        var doubleLinkedListNode = new DoubleLinkedListNode<T>(t,this);
        //索引过小
        if (index < 0)
        {
            //throw new Exception("索引过小");
            Debug.Log("索引过小");
            return;
        }
        //索引过大
        if (index >= this.Count)
        {
            //throw new Exception("索引过小");
            Debug.Log("索引过大");
            return;
        }
        //如果链表是空的，而且索引大于0
        if (this.IsEmpty && index > 0)
        {
            //throw new Exception("链表为空，而索引大于0");
            Debug.Log("链表为空，而索引大于0");
            return;
        }
        //如果索引为0，意味着向链表头部添加节点。
        if (index == 0)
        {
            AddFirst(t);
            return;
        }
        //要插入位置的节点
        DoubleLinkedListNode<T> current = this.mHeadDoubleLinkedListNode;
        int i = 0;
        while (true)
        {
            if (i == index)
            {
                break;
            }
            i++;
            current = current.NextNode;
        }
        //此处非常重要，特别要注意先后次序
        //当前节点的上一个的下一个设置为新节点
        current.PrevNode.NextNode = doubleLinkedListNode;
        //新节点的上一个设置为当前节点的上一个
        doubleLinkedListNode.PrevNode = current.PrevNode;
        //新节点的下一个设置为当前节点
        doubleLinkedListNode.NextNode = current;
        //当前节点的上一个设置为新节点
        current.PrevNode = doubleLinkedListNode;
        //大小加一
        this.size++;
    }

    /// <summary>
    ///     Copy到Array
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            //throw new Exception("目标数组不可以为空,无法复制");
            Debug.Log("目标数组不可以为空,无法复制");
            return;
        }
        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            //throw new Exception("索引值非法,无法进行复制.");
            Debug.Log("索引值非法,无法进行复制.");
            return;
        }
        if ((array.Length - arrayIndex) < this.Count || array.Length < this.Count)
        {
            //throw new Exception("目标数组容量不够,无法进行复制.");
            Debug.Log("目标数组容量不够,无法进行复制.");
            return;
        }
        for (int i = 0; i < this.Count; i++)
        {
            T item = this.GetElementByIndex(i);
            array[arrayIndex] = item;
            ++arrayIndex;
        }
    }

    /// <summary>
    ///     彩蛋一枚，研究研究！！！！
    ///     归并排序，非递归版本
    ///     步长为1的一轮过后，相邻的两个链表节点已经有序了
    ///     步长为2的一轮过后，前两个和紧挨着的后两个就有序了。
    ///     步长为4的一轮过后，前四个和紧挨着的后四个就有序了。
    ///     跟数组的差别在于：对于数组来说，长度易得，将数组分成两部分容易，将已经分成两部分的再次拆分也容易。
    ///     对于链表来说，长度难得，拆分更难得，因而，将链表划成两部分更难得。
    ///     对于链表的处理用以下方式：
    ///     首先将步长设为1，那么将链表两两分组，这两个节点中的前面一个作为第一个链表，后面的一个作为第二个链表。
    ///     将这两个链表进行合并，合并完成的结果必然是个有序的链表，当步长为1的合并进行完成之后，链表中相邻的两个
    ///     就排好序了，可是整体上可能还是无序的，那么将步长增加一倍，步长为2，继续进行合并，这个时候，第一个链表中
    ///     就存放了两个已经排好序的节点，而第二个链表中也是两个已经排好序的节点，继续对这两个链表进行合并，合并的
    ///     结果是4个一组的链表排序好了，这样一直进行下去，总该有个头吧，那是自然，头在何处呢？头就在合并次数这里；
    ///     如果链表中有10个节点，那么第一次进行合并的时候，分成了5组，也就是进行了5次合并；第二次进行合并的时候，
    ///     步长为2，分成了3组，合并进行了3次，第三次合并的时候，步长为4，合并进行2次，第四次合并的时候，步长为8
    ///     合并只需进行1次就可以结束了。
    ///     从单向链表的归并，到双向链表的归并非常简单，补上前向节点和尾节点就够了。
    /// </summary>
    public void MergeSort()
    {
        //如果是空链表或只有一个节点的链表
        //那么是不需要排序的
        if (this.IsEmpty || this.size == 1)
        {
            return;
        }
        int nstep = 1; //步长
                       //使劲循环
        while (true)
        {
            DoubleLinkedListNode<T> first; //第一个链表
            DoubleLinkedListNode<T> second; //第二个链表
            DoubleLinkedListNode<T> mEndDoubleLinkedListNode; //链表尾部
            DoubleLinkedListNode<T> temp; //临时节点
            int firstSize, secondSize; //第一个链表和第二个链表的大小
                                       //第一个链表从头开始
            first = this.mHeadDoubleLinkedListNode;
            //设置链表尾为null
            mEndDoubleLinkedListNode = null;
            int mergeCount = 0; //合并次数
                                //第一个链表不空
            while (first != null)
            {
                //需要合并的数量加一
                mergeCount++;
                //把第一个链表给第二个链表
                second = first;
                //第一个链表的大小设置为0
                firstSize = 0;
                //根据步长得到第一个链表的长度
                //下面这个循环还有个目的是确定第二个链表的起始位置
                //firstSize是第一个链表的实际长度，而secondSize长度可能大于实际的长度
                for (int i = 0; i < nstep; i++)
                {
                    firstSize++;
                    second = second.NextNode;
                    if (second == null)
                    {
                        break;
                    }
                }
                //让第二个链表的长度为步长
                secondSize = nstep;
                //如果第一个链表的长度大于零，或者第二个链表的长度大于零并且第二个链表不为空
                //这个循环是用来合并两个有序链表的
                while (firstSize > 0 || (secondSize > 0 && second != null))
                {
                    //下面这一大段if的意思是，从first或second中掐出来一个较小的节点
                    //放入temp这个临时节点中
                    //如果第一个链表的长度为0
                    if (firstSize == 0)
                    {
                        //将第二个链表中的第一个节点放入临时节点中
                        temp = second;
                        //将第二个链表下移一位，让第二个链表的第一个节点独立出来
                        second = second.NextNode;
                        //第二个链表的长度减一
                        secondSize--;
                    } //如果第二个链表为空或大小是零
                    else if (second == null || secondSize == 0)
                    {
                        //那么新链表就是第一个链表
                        temp = first;
                        //将第一个链表下移一位，让第一个链表的第一个节点独立出来
                        first = first.NextNode;
                        //第一个链表的长度减一
                        firstSize--;
                    } //到这里的时候，第一第二链表都不空
                      //如果第一个链表的第一个节点的数值小于第二个链表的第一个节点的数值
                    else if (first.DataValue.ToString().CompareTo(second.DataValue) <= 0)
                    {
                        //让新链表是第一个链表的第一个节点
                        temp = first;
                        //第一个链表下移一位，让第一个链表的第一个节点独立出来
                        first = first.NextNode;
                        //第一个链表的长度减一
                        firstSize--;
                    }
                    else //到这里的时候，第一第二链表都不空，而且第二个链表的第一个节点的数值小于第一个链表的第一个节点的数值
                    {
                        //让新链表是第二个链表
                        temp = second;
                        //第二个链表下移一位，让第二个链表的第一个节点独立出来
                        second = second.NextNode;
                        //第二个链表的长度减一
                        secondSize--;
                    }
                    //将得到的较小的哪个临时节点先放入endNode这个临时链表中
                    //如果临时链表不是空的，意味着这个节点已经不是两个节点中最小的那个节点了
                    if (mEndDoubleLinkedListNode != null)
                    {
                        //那么将得到的临时节点附加在临时链表的最后
                        mEndDoubleLinkedListNode.NextNode = temp;


                        temp.PrevNode = mEndDoubleLinkedListNode;
                        //最后将临时节点作为临时链表
                        //这一句每轮可能会进行很多次
                        mEndDoubleLinkedListNode = temp;
                    }
                    else
                    {
                        //如果临时链表是空的
                        //每一轮会为headNode赋一次值
                        //每轮的第一次会将两个链表中最小的那个节点赋给headNode
                        //最后一轮的时候headNode一定是最小的哪个节点
                        this.mHeadDoubleLinkedListNode = temp;


                        this.mHeadDoubleLinkedListNode.PrevNode = null;
                        //最后将临时节点作为临时链表
                        mEndDoubleLinkedListNode = temp;
                    }
                }
                //将第一个链表设置为第二个链表
                //到这里的时候second已经前进了一个步长了
                //新的first链表就从second开始
                first = second;
            }
            //走到这里的时候，以某个步长为步进的合并已经完成了
            //下面的工作，要么结束，要么将步长设为原来的两倍
            //临时链表的下一个设置为null，表示这是个尾节点
            //每一轮结束的时候，endNode中放置的是两个链表中最后一个节点
            //因为这是最后一个节点，所以就没有下一个了，因而将Next设置为null
            //如果不设置为null的话，那么链表有可能构成循环链表。
            mEndDoubleLinkedListNode.NextNode = null;


            this.mEndDoubleLinkedListNode = mEndDoubleLinkedListNode;
            //如果要合并的节点数量小于等于1，就直接返回，表明排序完成了
            if (mergeCount <= 1)
            {
                return;
            }
            //否则步长加倍，继续排序
            nstep = nstep * 2;
        }
    }
}
