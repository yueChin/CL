using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameObjectInfo :ISerializationCallbackReceiver {
    //public List<List<ISerializationCallbackReceiver>> gameContructInfoList;
    public List<BuildGameObject> BuildGOList;
    public List<MapGameObject> MapGOList;
    public List<PlayerGameObject> PlayerGOList;
    public List<CubeGameObject> CubeGOList;
    public List<EnemyGameObject> EnemyGOList;
    public List<NPCGameObject> NPCGOList;

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

[Serializable]
public class BuildGameObject
{
    public string GameObjectString;
    public string GameObjectPath;
}

[Serializable]
public class MapGameObject
{
    public string GameObjectString;
    public string GameObjectPath;
}

[Serializable]
public class PlayerGameObject
{
    public string GameObjectString;
    public string GameObjectPath;
}

[Serializable]
public class CubeGameObject
{
    public string GameObjectString;
    public string GameObjectPath;
}

[Serializable]
public class EnemyGameObject
{
    public string GameObjectString;
    public string GameObjectPath;
}

[Serializable]
public class NPCGameObject
{
    public string GameObjectString;
    public string GameObjectPath;
}