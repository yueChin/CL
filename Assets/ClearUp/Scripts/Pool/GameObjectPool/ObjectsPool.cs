using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool {
    ///单种类子弹的缓存池
    /// <summary>
    /// 名称，作为标识
    /// </summary>
    public string Name;

    /// <summary>
    /// 对象列表，存储同一个名称的所有对象
    /// </summary>
    public Dictionary<int,Object> ObjectList;

    public ObjectsPool(string name)
    {
        this.Name = name;
        this.ObjectList = new Dictionary<int, Object>();
    }

    /// <summary>
    /// 添加对象，往同一对象池里添加对象
    /// </summary>
    public void PushObject(GameObject gameObject)
    {
        int hashKey = gameObject.GetHashCode();
        if (!this.ObjectList.ContainsKey(hashKey))
        {
            this.ObjectList.Add(hashKey,new Object(gameObject));
        }
        else
        {
            this.ObjectList[hashKey].Active();
        }
    }

    /// <summary>
    /// 销毁对象，调用PoolItemTime中的destroy，即也没有真正销毁
    /// </summary>
    public void DestoryObject(GameObject gameObject)
    {
        int hashKey = gameObject.GetHashCode();
        if (this.ObjectList.ContainsKey(hashKey))
        {
            this.ObjectList[hashKey].Destroy();
        }
    }

    /// <summary>
    /// 返回没有真正销毁的第一个对象（即池中的DestoryStatus为true的对象）
    /// </summary>
    public GameObject GetObject()
    {
        if (this.ObjectList == null || this.ObjectList.Count == 0)
        {
            return null;
        }
        foreach (Object O in this.ObjectList.Values)
        {
            if (O.DestoryStatus)
            {
                //Debug.Log(FB);
                return O.Active();
            }
            //Debug.Log(FB);
        }
        return null;
    }

    /// <summary>
    /// 移除并销毁单个对象，真正的销毁对象!!
    /// </summary>
    public void RemoveObject(GameObject gameObject)
    {
        int hashKey = gameObject.GetHashCode();
        if (this.ObjectList.ContainsKey(hashKey))
        {
            GameObject.Destroy(gameObject);
            this.ObjectList.Remove(hashKey);
        }
    }

    /// <summary>
    /// 销毁对象，把所有的同类对象全部删除，真正的销毁对象!!
    /// </summary>
    public void Destory()
    {
        IList<Object> poolIList = new List<Object>();
        foreach (Object poolBO in this.ObjectList.Values)
        {
            poolIList.Add(poolBO);
        }
        while (poolIList.Count > 0)
        {
            if (poolIList[0] != null && poolIList[0].GameObject != null)
            {
                GameObject.Destroy(poolIList[0].GameObject);
                poolIList.RemoveAt(0);
            }
        }
        this.ObjectList = new Dictionary<int, Object>();
    }

    /// <summary>
    /// 超时检测，超时的就直接删除了，真正的删除!!
    /// </summary>
    public void BeyondObject()
    {
        IList<Object> beyondTimeList = new List<Object>();
        foreach (Object poolBO in this.ObjectList.Values)
        {
            if (poolBO.IsBeyondAliveTime())
            {
                beyondTimeList.Add(poolBO);
            }
        }
        int beyondTimeCount = beyondTimeList.Count;
        for (int i = 0; i < beyondTimeCount; i++)
        {
            this.RemoveObject(beyondTimeList[i].GameObject);
        }
    }
}
