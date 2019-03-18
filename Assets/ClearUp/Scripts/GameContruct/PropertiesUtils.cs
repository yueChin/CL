using System;
using System.Collections.Generic;
using System.Reflection;

public class PropertiesUtils
{
    //要不要带缓存？内存压力如何？
    private static Dictionary<Type, Dictionary<string, string>> cacheName = new Dictionary<Type, Dictionary<string, string>>();
    private static Dictionary<Type, Dictionary<string, string>> cacheDesc = new Dictionary<Type, Dictionary<string, string>>();
    private static Dictionary<Type, Dictionary<string, PartOfBodyType>> cachePart = new Dictionary<Type, Dictionary<string, PartOfBodyType>>();
    private static Dictionary<Type, Dictionary<string, int>> cacheValue = new Dictionary<Type, Dictionary<string, int>>();

    /// <summary>
    /// 单独获取不带缓存？
    /// </summary>
    //public static string GetDescByProperties(object p)
    //{
    //    Type type = p.GetType();
    //    FieldInfo[] fields = type.GetFields();
    //    foreach (FieldInfo field in fields)
    //    {
    //        if (field.Name.Equals(p.ToString()))
    //        {
    //            object[] objs = field.GetCustomAttributes(typeof(PropertiesGear), true);
    //            if (objs != null && objs.Length > 0)
    //            {
    //                return ((PropertiesGear)objs[0]).Desc;
    //            }
    //            else
    //            {
    //                return p.ToString() + "没有附加PropertiesDesc信息";
    //            }
    //        }
    //    }
    //    return "没有相应文本 : " + p;
    //}

    //public static PartOfBodyType GetPartByProperties(object p)
    //{
    //    Type type = p.GetType();
    //    FieldInfo[] fields = type.GetFields();
    //    foreach (FieldInfo field in fields)
    //    {
    //        if (field.Name.Equals(p.ToString()))
    //        {
    //            object[] objs = field.GetCustomAttributes(typeof(PropertiesGear), true);
    //            if (objs != null && objs.Length > 0)
    //            {
    //                return ((PropertiesGear)objs[0]).PartOfBodyType;
    //            }
    //            else
    //            {
    //                return default(PartOfBodyType);
    //            }
    //        }
    //    }
    //    return default(PartOfBodyType);
    //}

    //public static int GetValueByProperties(object p)
    //{
    //    Type type = p.GetType();
    //    FieldInfo[] fields = type.GetFields();
    //    foreach (FieldInfo field in fields)
    //    {
    //        if (field.Name.Equals(p.ToString()))
    //        {
    //            object[] objs = field.GetCustomAttributes(typeof(PropertiesGear), true);
    //            if (objs != null && objs.Length > 0)
    //            {
    //                return ((PropertiesGear)objs[0]).ArmementValue;
    //            }
    //            else
    //            {
    //                return 0;
    //            }
    //        }
    //    }
    //    return 0;
    //}
    public static string GetNameByProperties(object p)
    {
        var type = p.GetType();//Equipment
        if (!cacheName.ContainsKey(type))
        {
            CacheName(type);
        }
        var fieldNameToDesc = cacheName[type];
        var fieldName = p.ToString();
        return fieldNameToDesc.ContainsKey(fieldName) ? fieldNameToDesc[fieldName] : string.Format("没有找到相应的描述 `{0}` 类型名是 `{1}`", fieldName, type.Name);
    }


    //例如，穿一个Equipment.shoot;
    public static string GetDescByProperties(object p)
    {
        var type = p.GetType();//Equipment
        if (!cacheDesc.ContainsKey(type))
        {
            CacheDesc(type);
        }
        var fieldNameToDesc = cacheDesc[type];
        var fieldName = p.ToString();
        return fieldNameToDesc.ContainsKey(fieldName) ? fieldNameToDesc[fieldName] : string.Format("没有找到相应的描述 `{0}` 类型名是 `{1}`", fieldName, type.Name);
    }

    public static PartOfBodyType GetPartByProperties(object p)
    {
        var type = p.GetType();
        if (!cachePart.ContainsKey(type))
        {
            CachePart(type);
        }
        var fieldNameToDesc = cachePart[type];
        var fieldName = p.ToString();
        return fieldNameToDesc.ContainsKey(fieldName) ? fieldNameToDesc[fieldName] : default(PartOfBodyType);
    }

    public static int GetValueByProperties(object p)
    {
        var type = p.GetType();
        if (!cacheValue.ContainsKey(type))
        {
            CacheValue(type);
        }
        var fieldNameToDesc = cacheValue[type];
        var fieldName = p.ToString();
        return fieldNameToDesc.ContainsKey(fieldName) ? fieldNameToDesc[fieldName] : default(int); 
    }

    private static void CacheName(Type type)
    {
        var dict = new Dictionary<string, string>();
        cacheName.Add(type, dict);
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            var objs = field.GetCustomAttributes(typeof(PropertiesChinaName), true);
            if (objs.Length > 0)
            {
                dict.Add(field.Name, ((PropertiesChinaName)objs[0]).NameDesc);
            }
        }
    }

    private static void CacheDesc(Type type)
    {
        var dict = new Dictionary<string, string>();
        cacheDesc.Add(type, dict);
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            var objs = field.GetCustomAttributes(typeof(PropertiesDescription), true);
            if (objs.Length > 0)
            {
                dict.Add(field.Name, ((PropertiesDescription)objs[0]).Desc);
            }
        }
    }

    private static void CacheValue(Type type)
    {
        var dict = new Dictionary<string, int>();
        cacheValue.Add(type, dict);
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            var objs = field.GetCustomAttributes(typeof(PropertiesArmament), true);
            if (objs.Length > 0)
            {
                dict.Add(field.Name, ((PropertiesArmament)objs[0]).ArmementValue);
            }
        }
    }

    private static void CachePart(Type type)
    {
        var dict = new Dictionary<string, PartOfBodyType>();
        cachePart.Add(type, dict);
        var fields = type.GetFields();
        foreach (var field in fields)
        {
            var objs = field.GetCustomAttributes(typeof(PropertiesPartOfbody), true);
            if (objs.Length > 0)
            {
                dict.Add(field.Name, ((PropertiesPartOfbody)objs[0]).PartOfBodyType);
            }
        }
    }
}
