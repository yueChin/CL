using System;
using UnityEngine;

[Serializable]
public class RecordMulInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public ScoreMulType MulType;
    public string MulTypeString;
    public float Score;

    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        ScoreMulType type = (ScoreMulType)System.Enum.Parse(typeof(ScoreMulType), MulTypeString);
        MulType = type;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}
