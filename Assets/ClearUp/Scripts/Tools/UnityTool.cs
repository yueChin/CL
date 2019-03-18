using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class UnityTool
{
    public static Transform[] FindAllObject()
    {
        Transform[] pAllObjects = (Transform[])Resources.FindObjectsOfTypeAll(typeof(Transform));
        for (int i = 0; i < pAllObjects.Length; i++)
        {
            if (pAllObjects[i].transform.parent != null)
            {
                continue;//如果父对象非能，跳过父对象
            }
            if (pAllObjects[i].hideFlags == HideFlags.NotEditable || pAllObjects[i].hideFlags == HideFlags.HideAndDontSave)
            {
                continue;
            }
            //if (Application.isEditor)
            //{
            //    string sAssetPath = AssetDatabase.GetAssetPath(pAllObjects[i].transform.root.gameObject);
            //    if (!string.IsNullOrEmpty(sAssetPath))
            //    {
            //        continue;
            //    }
            //}
            //Debug.Log(pAllObjects[i].name);
        }
        return pAllObjects;
    }

    public static GameObject FindOneOfActiveChild(GameObject parent, string childName)
    {
        if (!parent.activeSelf || parent == null)
        {
            Debug.Log("父对象为空，无法查找");
            return null;
        }
        Transform[] children = parent.transform.GetComponentsInChildren<Transform>(false);
        bool isFinded = false;
        Transform child = null;
        for(int i=0;i < children.Length;i++)
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
        if (isFinded) return child.gameObject;
        return null;
    }

    public static Transform FindOneOfAllChild(Transform parent, string childName)
    {
        if (!parent.gameObject.activeSelf)
        {
            Debug.Log("父对象为空，无法查找");
            return null;
        }
        Transform[] children = parent.GetComponentsInChildren<Transform>(true);
        bool isFinded = false;
        Transform child = null;
        //Debug.Log(parent.name +" "+ childName);
        for (int i = 0; i < children.Length; i++)
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
        if (child == null) { Debug.Log("无法在游戏物体"+parent.name + " 下找到" + childName); }
        return child;
    }

    public static List<Transform> FindAllChild(Transform parent)
    {
        List<Transform> transforms = new List<Transform>();
        foreach (Transform t in parent)
        {
            transforms.Add(t.transform);
        }
        return transforms;
    }

    public static void Attach(Transform parent, Transform child)
    {
        child.parent = parent;
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
    }
}

