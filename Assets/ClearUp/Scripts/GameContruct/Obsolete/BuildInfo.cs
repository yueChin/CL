
using UnityEngine;
using System;

[Serializable]
public class BuildInfo :ISerializationCallbackReceiver{

    [NonSerialized]
    public BuildType BuildType;
    public string buildTypeString;
    public string buildPath;

    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        BuildType buildType = (BuildType)System.Enum.Parse(typeof(BuildType), buildTypeString); //具体的枚举名称储存
        //Debug.Log(buildType.ToString());
        BuildType = buildType;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}
