using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ComboSkillList<T> where T : BattleSkill {

    //头和尾
    private ComboSkill<T> mEndComboSkill;
    private ComboSkill<T> mHeadComboSkill;
    private BattleSystem mBattleSystem;
    //长度
    private int mSize;

    /// <summary>
    ///     判断链表是否是空的
    /// </summary>
    public bool IsEmpty
    {
        get { return this.mHeadComboSkill == null; }
    }

    /// <summary>
    ///     链表中元素的个数
    /// </summary>
    public int Count
    {
        get
        {
            int i = 0;
            ComboSkill<T> node = this.mHeadComboSkill;
            //Debug.Log(node+"节点value");
            while (node != null)
            {
                ++i;
                //Debug.Log(i);
                node = node.NextNode;
            }
            return i;
            //return this.mSize;
        }
    }

    public ComboSkillList(BattleSystem battleSystem)
    {
        this.mBattleSystem = battleSystem;
        mSize = 0;
    }

    /// <summary>
    ///     根据索引获取链表中的节点
    /// </summary>
    /// <param name="index">整型索引</param>
    /// <returns>节点</returns>
    public ComboSkill<T> this[int index]
    {
        get
        {
            //链表头节点是空的
            if (this.mHeadComboSkill == null)
            {
                //throw new Exception("链表为空.");
                Debug.Log("链表为空.");
                return null;
            }
            //索引过小
            if (index < 0)
            {
                Debug.Log("索引过小.");
                return null;
            }
            //索引过大
            if (index >= this.Count)
            {
                Debug.Log("索引过大.");
                return null;
            }
            //取得头节点
            var current = new ComboSkill<T>(this,null);
            //如果索引在前一半，那么从前向后找
            if (index < this.mSize / 2)
            {
                current = this.mHeadComboSkill;
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
                current = this.mEndComboSkill;
                int i = this.mSize;
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

    public ComboSkill<T> HeadComboSkill
    {
        get { return this.mHeadComboSkill; }
        set { this.mHeadComboSkill = value; }
    }

    public ComboSkill<T> EndComboSkill
    {
        get { return this.mEndComboSkill; }
        set { this.mEndComboSkill = value; }
    }

    public void AddAfter(ComboSkill<T> node, ComboSkill<T> newNode)
    {
        if (node == null || newNode == null)
        {
            //throw new Exception("元素为空，不可以进行插入.");
            Debug.Log("元素为空，不可以进行插入.");
            return;
        }
        newNode.PrevNode = node;
        newNode.NextNode = node.NextNode;
        node.NextNode.PrevNode = newNode;
        node.NextNode = newNode;
        mSize++;
    }

    public void AddAfter(ComboSkill<T> node, T t)
    {
        if (node == null || t == null)
        {
            //throw new Exception("元素为空，不可以进行插入.");
            Debug.Log("元素为空，不可以进行插入.");
            return;
        }
        if (node.Equals(mEndComboSkill))
        {
            AddLast(t);
        }
        else
        {
            if (!node.PushInEmpty(t))
            {
                var comboSkill = new ComboSkill<T>(this,t);
                AddAfter(node,comboSkill);
            }
        }
    }

    public void AddBefore(ComboSkill<T> node, ComboSkill<T> newNode)
    {
        if (node == null || newNode == null)
        {
            //throw new Exception("元素为空，不可以进行插入.");
            Debug.Log("元素为空，不可以进行插入.");
            return;
        }
        newNode.PrevNode = node.PrevNode;
        newNode.NextNode = node;
        node.PrevNode.NextNode = newNode;
        node.PrevNode = newNode;
        mSize++;
    }

    public void AddBefore(ComboSkill<T> node, T t)
    {
        if (node == null || t == null)
        {
            //throw new Exception("元素为空，不可以进行插入.");
            Debug.Log("元素为空，不可以进行插入.");
            return;

        }
        if (node.Equals(mHeadComboSkill))
        {
            AddFirst(t);
        }
        else
        {
            if (!node.PushInEmpty(t))
            {
                var comboSkill = new ComboSkill<T>(this, t);
                AddBefore(node, comboSkill);
            }
        }
    }

    public void AddFirst(ComboSkill<T> node)
    {
        this.mHeadComboSkill.PrevNode = node;
        //新节点的下一个为原来的头节点
        node.NextNode = this.mHeadComboSkill;
        //新头节点为新节点
        this.mHeadComboSkill = node;
        //大小加一
        this.mSize++;
    }

    /// <summary>
    ///     添加节点到链表的开头
    /// </summary>
    /// <param name="t"></param>
    public ComboSkill<T> AddFirst(T t)
    {
        if (t == null)
        {
            //throw new Exception("元素为空,元法进行添加!");
            Debug.Log("元素为空,元法进行添加!");
            return null;
        }        
        //如果头为null
        if (this.mHeadComboSkill == null)
        {
            var ComboSkill = new ComboSkill<T>(this, t);
            //把头节点设置为node
            this.mHeadComboSkill = ComboSkill;
            //因为是空链表，所以头尾一致
            this.mEndComboSkill = ComboSkill;
            //大小加一
            this.mSize++;
        }
        else if (!mHeadComboSkill.PushInEmpty(t))
        {
            var comboSkill = new ComboSkill<T>(this, t);
            AddFirst(comboSkill);
        }
        return this.mHeadComboSkill;
    }

    public void AddLast(ComboSkill<T> node)
    {
        this.mEndComboSkill.NextNode = node;
        //将新节点的上一个设置为原尾节点
        node.PrevNode = this.mEndComboSkill;
        //将尾节点重新设置为新节点
        this.mEndComboSkill = node;
        //大小加一
        this.mSize++;
    }

    /// <summary>
    ///     添加节点到链表的末尾
    /// </summary>
    /// <param name="t">要添加的数据</param>
    public ComboSkill<T> AddLast(T t)
    {
        if (t == null)
        {
            //throw new Exception("元素为空,元法进行添加!");
            Debug.Log("元素为空,元法进行添加!");
            return null;
        }        
        //如果头为null
        //Debug.Log("增加技能");
        if (this.mHeadComboSkill == null)
        {
            //Debug.Log("增加tou技能");
            var ComboSkill = new ComboSkill<T>(this, t);
            //把头节点设置为node
            this.mHeadComboSkill = ComboSkill;
            //因为是空链表，所以头尾一致
            this.mEndComboSkill = ComboSkill;
            //大小加一
            this.mSize++;
        }
        if (!mEndComboSkill.PushInEmpty(t))
        {
            var comboSkill = new ComboSkill<T>(this,t);
            AddLast(comboSkill);
            Debug.Log("增加新节点");
        }
        return this.mEndComboSkill;
    }

    /// <summary>
    /// 移除链表中的节点
    /// </summary>
    /// <param name="index">要移除的节点的索引</param>
    public bool Remove(ComboSkill<T> node)
    {
        if (node == null)
        {
            //throw new Exception("节点不可以为空，无法进行删除.");
            Debug.Log("节点不可以为空，无法进行删除.");
            return false;
        }
        if (node.Equals(mHeadComboSkill))
        {
            this.mHeadComboSkill = node.NextNode;
            node = null;
        }
        else if (node.Equals(mEndComboSkill))
        {
            this.mEndComboSkill = node.PrevNode;
            this.mEndComboSkill.NextNode = null;
            node = null;
        }
        else
        {
            // 如果前后的名字相同,且前后有一个三消，直接删除这个节点即可
            if (node.PrevNode.Name == node.NextNode.Name)
            {
                if (!node.PrevNode.IsAnyEmpty() || !node.NextNode.IsAnyEmpty())// 3+n n+3的情况
                {
                    node.PrevNode.NextNode = node.NextNode;
                    node.NextNode.PrevNode = node.PrevNode;
                    node = null;
                }
                else
                {
                    bool flagOne = true;
                    T[] prevSkills = node.PrevNode.GetSkills();
                    T[] nextSkills = node.NextNode.GetSkills();
                    if (prevSkills.Length + nextSkills.Length <= node.MaxCombo) //1+1 1+2 2+1的情况,把后面往前面合并
                    {
                        for (int i = 0; i < nextSkills.Length; i++)
                        {
                            flagOne = node.PrevNode.PushInEmpty(nextSkills[i]);
                            if (flagOne == false)
                            {
                                UnityEngine.Debug.Log("合并块 情况有变，速来支援");
                                return false;
                            }
                        }
                        node.PrevNode.NextNode = node.NextNode.NextNode;
                        node.NextNode.NextNode.PrevNode = node.PrevNode;
                        node = null;
                        node.NextNode = null;
                    }
                    else//2+2的情况 变3 + 1
                    {                        
                        bool flagTwo = true;
                        flagOne = node.PrevNode.PushInEmpty(nextSkills[0]);
                        flagTwo = node.NextNode.Remove(nextSkills[1]);//?会失败吗？
                        if (flagOne && flagTwo)
                        {
                            UnityEngine.Debug.Log("分离块 情况有变，速来支援");
                            return false;
                        }
                        node.PrevNode.NextNode = node.NextNode;
                        node.NextNode.PrevNode = node.PrevNode;
                        node = null;
                    }
                }
            }
            else
            {
                node.PrevNode.NextNode = node.NextNode;
                node.NextNode.PrevNode = node.PrevNode;
                node = null;
            }            
        }
        mSize--;
        return true;
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
        ComboSkill<T> node = this.mHeadComboSkill;
        int i = 0;
        while (node != null)
        {
            if (i == index)
            {
                Remove(this[i]);
                flag = true;
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
        //如果mSize为1，那就是清空链表。
        if (this.mSize == 1)
        {
            this.Clear();
            return;
        }
        //将头节点设为原头结点的下一个节点，就是下一个节点上移
        this.mHeadComboSkill = this.mHeadComboSkill.NextNode;
        //处理上一步遗留问题，原来的第二个节点的上一个是头结点，现在第二个要变成头节点，那要把它的Prev设为null才能成为头节点
        this.mHeadComboSkill.PrevNode = null;
        //大小减一
        this.mSize--;
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
        //如果mSize为1，那就是清空链表。
        if (this.mSize == 1)
        {
            this.Clear();
            return;
        }
        //尾节点设置为倒数第二个节点
        this.mEndComboSkill = this.mEndComboSkill.PrevNode;
        //将新尾节点的Next设为null，表示它是新的尾节点
        this.mEndComboSkill.NextNode = null;
        //大小减一
        this.mSize--;
    }

    public ComboSkill<T> Find(T value)
    {
        int index = this.IndexOf(value);
        return this[index];
    }

    public int IndexOf(ComboSkill<T> comboSkill)
    {
        if (comboSkill == null || this.Count == 0 || this.mHeadComboSkill == null)
        {
            //throw new Exception("无法得到元素索引.");
            Debug.Log("无法得到元素索引.");
            return -1;
        }
        int reInt = -1;
        int i = 0;
        ComboSkill<T> node = this.mHeadComboSkill;
        while (node != null)
        {
            if (node.Equals(comboSkill))
            {
                reInt = i;
                break;
            }
            ++i;
            node = node.NextNode;
        }
        return reInt;
    }

    public int IndexOf(T t)
    {
        if (t == null || this.Count == 0 || this.mHeadComboSkill == null)
        {
            //throw new Exception("无法得到元素索引.");
            Debug.Log("无法得到元素索引." +"技能"+ t + "个数"+this.Count + "头节点"+this.mHeadComboSkill);
            return -1;
        }
        int reInt = -1;
        int i = 0;
        ComboSkill<T> node = this.mHeadComboSkill;
        while (node != null)
        {
            if (node.IsContain(t))
            {
                reInt = i;
                break;
            }
            ++i;
            node = node.NextNode;
        }
        return reInt;
    }

    public ComboSkill<T> GetNodeByIndex(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            //throw new Exception("索引值非法,无法得对象节点.");
            Debug.Log("索引值非法,无法得对象节点.");
            return null;
        }
        ComboSkill<T> reNode = this.mHeadComboSkill;
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
        this.mHeadComboSkill = null;
        this.mEndComboSkill = null;
        this.mSize = 0;
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
            ComboSkill<T> node = this.mHeadComboSkill;
            while (node != null)
            {
                if (node.IsContain(item))
                {
                    flag = true;
                    break;
                }
                node = node.NextNode;
            }
        }
        return flag;
    }

    public bool ReleaseSkill(T t, ref RelaseSkill relaseSkill)
    {
        bool isTrue = false;
        try
        {
            isTrue = GetNodeByIndex(IndexOf(t)).ReleaseComboSkill(ref relaseSkill);
        }
        catch(Exception e)
        {
            Debug.Log(e.Message.ToString());
        }
        return isTrue;
    }
}
