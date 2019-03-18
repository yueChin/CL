using UnityEngine;
using System;

[Serializable]
public class MapInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public MapType MapType;
    public string mapTypeString;
    public string mapPath;
    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        MapType mapType = (MapType)System.Enum.Parse(typeof(MapType), mapTypeString);
        MapType = mapType;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}
