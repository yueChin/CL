using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameContructInfo :ISerializationCallbackReceiver {
    //public List<List<ISerializationCallbackReceiver>> gameContructInfoList;
    public List<PlayerInfo> playerInfoList;
    public List<MapInfo> mapInfoList;
    public List<CubeInfo> cubeInfoList;
    public List<BuildInfo> buildInfoList;        

    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        //gameContructInfoList.Add(playerInfoList);
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}
