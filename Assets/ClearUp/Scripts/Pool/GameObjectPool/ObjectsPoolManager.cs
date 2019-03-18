using System.Collections.Generic;
using UnityEngine;

public static class ObjectsPoolManager {
    public static Dictionary<string, ObjectsPool> ObjectsList;
    /// <summary>
    /// 超时时间
    /// </summary>
    public const int Alive_Time = 1 * 60;

    /// <summary>
    /// 对象池
    /// </summary>

    /// <summary>
    /// 添加一个对象组
    /// </summary>
    public static void PushData(string name)
    {
        if (ObjectsList == null)
            ObjectsList = new Dictionary<string, ObjectsPool>();
        if (!ObjectsList.ContainsKey(name))
            ObjectsList.Add(name, new ObjectsPool(name));
    }

    /// <summary>
    /// 添加单个对象（首先寻找对象组->添加单个对象）
    /// </summary>
    public static void PushObject(string name, GameObject gameObject)
    {
        if (ObjectsList == null || !ObjectsList.ContainsKey(name))
        {
            PushData(name);//添加对象组
                            //添加对象
        }
        ObjectsList[name].PushObject(gameObject);
    }

    /// <summary>
    /// 移除单个对象，真正的销毁!!
    /// </summary>
    public static void RemoveObject(string name, GameObject gameObject)
    {
        if (ObjectsList == null || !ObjectsList.ContainsKey(name))
            return;
        ObjectsList[name].RemoveObject(gameObject);
    }

    /// <summary>
    /// 获取缓存中的对象
    /// </summary>
    public static GameObject GetObject(string name)
    {
        if (ObjectsList == null || !ObjectsList.ContainsKey(name))
        {           
            return null;
        }
        //Debug.Log(name);
        return ObjectsList[name].GetObject();
    }

    /// <summary>
    /// 销毁对象，没有真正的销毁!!
    /// </summary>
    public static void DestroyActiveObject(string name, GameObject gameObject)
    {
        if (ObjectsList == null || !ObjectsList.ContainsKey(name))
        {
            return;
        }
        ObjectsList[name].DestoryObject(gameObject);
    }

    /// <summary>
    /// 销毁对象，真正的销毁!!
    /// </summary>
    public static void BeyondTimeObject()
    {
        if (ObjectsList == null)
        {
            return;
        }
        foreach (ObjectsPool OPool in ObjectsList.Values)
        {
            OPool.BeyondObject();
        }
    }

    /// <summary>
    /// 销毁所有对象，真正的销毁!!
    /// </summary>
    public static void Destroy()
    {
        if (ObjectsList == null)
        {
            return;
        }
        foreach (ObjectsPool OPool in ObjectsList.Values)
        {
            OPool.Destory();
        }
        ObjectsList = null;
    }
}
