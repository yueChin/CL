internal interface ILinkedList<T>
{
    /// <summary>
    ///     头元素
    /// </summary>
    DoubleLinkedListNode<T> HeadDoubleLinkedListNode { get; }

    /// <summary>
    ///     尾元素
    /// </summary>
    DoubleLinkedListNode<T> EndDoubleLinkedListNode { get; }

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 中指定的现有节点后添加指定的新节点。
    /// </summary>
    /// <param name="node"></param>
    /// <param name="newNode"></param>
    void AddAfter(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 中的现有节点后添加新的节点或值。
    /// </summary>
    /// <param name="node"></param>
    /// <param name="t"></param>
    void AddAfter(DoubleLinkedListNode<T> node, T t);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 中指定的现有节点前添加指定的新节点。
    /// </summary>
    /// <param name="node"></param>
    /// <param name="newNode"></param>
    void AddBefore(DoubleLinkedListNode<T> node, DoubleLinkedListNode<T> newNode);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 中指定的现有节点前添加包含指定值的新节点。
    /// </summary>
    /// <param name="node"></param>
    /// <param name="t"></param>
    void AddBefore(DoubleLinkedListNode<T> node, T t);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 的开头处添加包含指定值的新节点。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    DoubleLinkedListNode<T> AddFirst(T value);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 的开头处添加指定的新节点
    /// </summary>
    /// <param name="node"></param>
    void AddFirst(DoubleLinkedListNode<T> node);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 的结尾处添加包含指定值的新节点。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    DoubleLinkedListNode<T> AddLast(T value);

    /// <summary>
    ///     在 LinkedList<(Of <(T>)>) 的结尾处添加指定的新节点
    /// </summary>
    /// <param name="node"></param>
    void AddLast(DoubleLinkedListNode<T> node);

    /// <summary>
    ///     从 LinkedList<(Of <(T>)>) 中移除指定的节点。
    /// </summary>
    /// <param name="node"></param>
    bool Remove(DoubleLinkedListNode<T> node);

    /// <summary>
    ///     从 LinkedList<(Of <(T>)>) 中按索引删除节点。
    /// </summary>
    /// <param name="node"></param>
    bool Remove(int index);

    /// <summary>
    ///     移除头结点
    /// </summary>
    void RemoveHeadNode();

    /// <summary>
    ///     移除尾节点
    /// </summary>
    void RemoveEndNode();

    /// <summary>
    ///     从 LinkedList<(Of <(T>)>) 中查找第一个匹配项
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    DoubleLinkedListNode<T> Find(T value);

    /// <summary>
    ///     从 LinkedList<(Of <(T>)>) 中查找最后一个匹配项
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    DoubleLinkedListNode<T> FindLast(T value);

    /// <summary>
    ///     查询元素的索引
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int IndexOf(T item);

    /// <summary>
    ///     能过索引得到元素的元素
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    T GetElementByIndex(int index);

    /// <summary>
    ///     通过索引得到节点对象
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    DoubleLinkedListNode<T> GetNodeByIndex(int index);
}
