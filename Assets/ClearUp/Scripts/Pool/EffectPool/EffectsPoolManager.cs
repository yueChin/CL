using System.Collections.Generic;
using UnityEngine;

public class EffectsPoolManager {

    public static Dictionary<string, EffectsPool> EffectsList;
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
        if (EffectsList == null)
            EffectsList = new Dictionary<string, EffectsPool>();
        if (!EffectsList.ContainsKey(name))
            EffectsList.Add(name, new EffectsPool(name));
    }

    /// <summary>
    /// 添加单个对象（首先寻找对象组->添加单个对象）
    /// </summary>
    public static void PushEffect(string name, GameObject gameObject)
    {
        if (EffectsList == null || !EffectsList.ContainsKey(name))
        {
            PushData(name);//添加对象组
                            //添加对象
        }
        EffectsList[name].PushEffect(gameObject);
    }

    /// <summary>
    /// 移除单个对象，真正的销毁!!
    /// </summary>
    public static void RemoveEffect(string name, GameObject gameObject)
    {
        if (EffectsList == null || !EffectsList.ContainsKey(name))
            return;
        EffectsList[name].RemoveEffect(gameObject);
    }    

    /// <summary>
    /// 销毁对象，真正的销毁!!
    /// </summary>
    public static void BeyondTimeObject()
    {
        if (EffectsList == null)
        {
            return;
        }
        foreach (EffectsPool EPool in EffectsList.Values)
        {
            EPool.BeyondEffect();
        }
    }

    /// <summary>
    /// 销毁对象池，真正的销毁!!
    /// </summary>
    public static void Destroy()
    {
        if (EffectsList == null)
        {
            return;
        }
        foreach (EffectsPool EPool in EffectsList.Values)
        {
            EPool.Destory();
        }
        EffectsList = null;
    }

    /// <summary>
    /// 拖尾特效，如果父物体不存在，会自动调用偏移特效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="parentGameObject"></param>
    /// <param name="offset"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static void FollowEffect(string effectName, GameObject parentGameObject, Vector3 offset, float scale)
    {        
        Effect ge = PointOffsetEffect(effectName, Vector3.zero, offset, scale);
        if (ge == null) return;
        ge.SetParent(parentGameObject);
        ge.transform.position += offset;
    }

    /// <summary>
    ///  相对于原点的偏移的特效，如果偏移不存在，会自动调用原点特效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="offset"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Effect PointOffsetEffect(string effectName, Vector3 absPosition, Vector3 offset, float scale)
    {
        Effect ge = PointFixedEffect(effectName,absPosition, scale);
        if (ge == null) return null;
        ge.transform.position += offset;
        return ge;
    }

    /// <summary>
    /// 固定点特效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="absPosition"></param>
    /// <param name="scale"></param>
    public static Effect PointFixedEffect(string effectName, Vector3 absPosition, float scale)
    {
        Effect ge = PointEffect(effectName, scale);
        if (ge == null) return null;        
        ge.transform.position = absPosition;
        return ge;
    }

    /// <summary>
    /// 特效原点特效，如果不存在，会自动创建
    /// </summary>
    /// <param name="name"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static Effect PointEffect(string name, float scale)
    {
        if (EffectsList == null || !EffectsList.ContainsKey(name))
        {
            CreateEffect(name);
        }
        return EffectsList[name].PlayEffect(scale);
    }

    /// <summary>
    /// 创建特效池,并把特效添加到特效池中
    /// </summary>
    /// <param name="name"></param>
    /// <param name="scale"></param>
    /// <returns></returns>
    public static void CreateEffect(string name)
    {
        GameObject go = FactoryManager.EffectFactory.LoadEffect(name);
        go.SetActive(false);
        PushEffect(name, go);
    }
}
