using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UITool  {

    public static GameObject GetCanvas(string name = "Canvas")
    {
        return GameObject.Find(name);
    }

    public static T FindChild<T>(Transform parent, string childName)
    {
        Transform uiGO = UnityTool.FindOneOfAllChild(parent, childName);
        if (uiGO == null)
        {
            Debug.LogError("在游戏物体" + parent + "下面查找不到" + childName);
            return default(T);
        }
        return uiGO.GetComponent<T>();
    }

    [Obsolete("FindOneOfActiveChild<T> 已过时，请使用FindChild<T>")]
    public static T FindOneOfActiveChild<T>(GameObject parent, string childName)
    {
        RectTransform[] children = parent.GetComponentsInChildren<RectTransform>();
        //Debug.Log(children);
        bool isFinded = false;
        T child = default(T);
        for (int i = 0; i < children.Length; i++)
        {          
            if (children[i].name == childName)
            {
                if (children[i].GetType() == typeof(T))
                {
                    if (isFinded)
                    {
                        Debug.LogWarning("在游戏对象" + parent + "下存在不止一个同名子物体:" + childName);
                    }
                    isFinded = true;
                    child = children[i].gameObject.GetComponent<T>();
                }
            }
        }
        if (isFinded) return child;
        return default(T);
    }

    public static Transform FindOneOfActiveChild(Transform parent, string childName)
    {
        RectTransform[] children = parent.GetComponentsInChildren<RectTransform>();
        Debug.Log(children);
        bool isFinded = false;
        RectTransform child = null;
        for(int i=0;i< children.Length;i++)
        {
            if (children[i].name == childName)
            {
                if (isFinded)
                {
                    Debug.LogWarning("在游戏物体" + parent + "下存在不止一个子物体:" + childName);
                }
                isFinded = true;
                child = children[i];
            }
        }
        if (isFinded) return child;
        return null;
    }

    public static Transform FindOneOfAllChild(Transform parent, string childName)
    {
        bool isFinded = false;
        RectTransform child = null;
        //Debug.Log(parent.name + "+" + childName);
        foreach (RectTransform t in parent.transform)
        {
            if (t.name == childName)
            {
                if (isFinded)
                {
                    Debug.LogWarning("在游戏物体" + parent + "下存在不止一个子物体:" + childName);
                }
                isFinded = true;
                child = t;
            }
        }
        return child;
    }

    /// <summary>
    /// 改变颜色，如果输入256，那么该值不改变
    /// </summary>
    /// <param name="color"></param>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    public static Color ChangeColor(Color color,float red,float green,float blue,float alpha,float speed)
    {
        float _alpha = color.a;
        float _green = color.g;
        float _blue = color.b;
        float _red = color.r;
        if (red == 256)
        {
            red = _red;
        }
        if (blue == 256)
        {
            blue = _blue;
        }
        if (green == 256)
        {
            green = _green;
        }
        if (alpha == 256)
        {
            alpha = _alpha;
        }    
        color = new Color(
            Mathf.Lerp(_red, red, speed * Time.deltaTime) / 255,
            Mathf.Lerp(_green, green, speed * Time.deltaTime) /255,
            Mathf.Lerp(_blue, blue, speed * Time.deltaTime) /255,
            Mathf.Lerp(_alpha, alpha, speed * Time.deltaTime) / 255);
        return color;
    }
}
