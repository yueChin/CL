using UnityEngine;
using System;

[Serializable]
public class PlayerInfo : ISerializationCallbackReceiver
{
    [NonSerialized]
    public PlayerType PlayerType;
    public string playerTypeString;
    public string playerPath;
    
    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        PlayerType playerType = (PlayerType)System.Enum.Parse(typeof(PlayerType), playerTypeString);
        PlayerType = playerType;
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}
