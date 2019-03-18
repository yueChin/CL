using UnityEngine;
using System;

[Serializable]
public class CubeInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public CubeType CubeType;
    public string cubeTypeString;
    public string cubePath;
    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        CubeType cubeType = (CubeType)System.Enum.Parse(typeof(CubeType), cubeTypeString);
        //Debug.Log(cubeType.ToString());
        CubeType = cubeType;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}
