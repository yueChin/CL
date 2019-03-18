using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SpriteInfo :ISerializationCallbackReceiver {
    //public List<List<ISerializationCallbackReceiver>> gameContructInfoList;
    public List<PlayerSprite> PlayerSpriteList;
    public List<NPCSprite> NPCSpriteList;
    public List<EnemySprite> EnemySpriteList;
    public List<WeaponSprite> WeaponSpriteList;
    public List<AmorSprite> AmorSpriteList;
    public List<EquipmentSprite> EquipmentSpriteList;
    public List<NormalSkillSprite> NormalSkillSpriteList;
    public List<BattleSkillSprite> BattleSkillSpriteList;
    public List<ItemSprite> ItemSpriteList;

    // 反序列化   从文本信息 到对象
    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class PlayerSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class NPCSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class EnemySprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class WeaponSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class AmorSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class EquipmentSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class NormalSkillSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class BattleSkillSprite
{
    public string SpriteString;
    public string SpritePath;
}

[Serializable]
public class ItemSprite
{
    public string SpriteString;
    public string SpritePath;
}