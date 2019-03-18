using System.Collections.Generic;
/// <summary>
/// 双向链表节点
/// </summary>
/// <typeparam name="T"></typeparam>
public class DoubleCircularLinkedListNode<T> {
    public T Value { set; get; }
    public DoubleCircularLinkedListNode<T> Next { set; get; }
    public DoubleCircularLinkedListNode<T> Preview { set; get; }
    public DoubleCircularLinkedListNode(T val, DoubleCircularLinkedListNode<T> preview = null, DoubleCircularLinkedListNode<T> next = null)
    {
        this.Value = val;
        this.Preview = preview;
        this.Next = next;
    }
}
