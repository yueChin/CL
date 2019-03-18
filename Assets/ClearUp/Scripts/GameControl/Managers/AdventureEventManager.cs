using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildEvent
{
    public BuildEvent(string buildTag, string cubeTag, Vector3 vector3)
    {
        BuildTag = buildTag;
        CubeTag = cubeTag;
        Pos = vector3;
    }
    public string BuildTag { get; private set; }
    public string CubeTag { get; private set; }
    public Vector3 Pos { get; private set; }
}

public class AdventureEventManager {

    private static AdventureEventManager mAdventureEventManager;
    public static AdventureEventManager GetAdventureEventManager
    {
        get
        {
            if (mAdventureEventManager == null)
            {
                mAdventureEventManager = new AdventureEventManager();
            }
            return mAdventureEventManager;
        }
    }

    //所有的调整部分应该让工厂处理
    public bool TriggerEnemyCreature(EnemyType enemyType,Vector3 pos)
    {
        IAdventureEventAction enemyAction = FactoryManager.CreatureFactory.GetEnemyCreature(enemyType, pos);
        //Debug.Log(enemyAction);
        //gameobject跳出，然后比值大小，显示战斗结果？
        OnTrigger(enemyAction);
        AudioSource.PlayClipAtPoint(FactoryManager.AssetFactory.LoadAudioClip("Audios/TriggerEnemy"), pos);
        return true;
    }

    public bool TriggerGoodCreature(OccupationType occupationType,Vector3 pos)
    {
        //Debug.Log("触发好人职业"+ occupationType);
        IAdventureEventAction goodCreatureAction = FactoryManager.CreatureFactory.GetGoodCreature(occupationType, pos);
        //Debug.Log(goodCreatureAction);
        //gameobjet 跳出，旅人显示金币，其余显示对话界面?
        OnTrigger(goodCreatureAction);
        AudioSource.PlayClipAtPoint(FactoryManager.AssetFactory.LoadAudioClip("Audios/TriggerNpc"), pos);
        return true;
    }

    public bool TriggerAdventureEvent(AdventureEventType adventureEventType,Vector3 pos)
    {
        FactoryManager.AdventureEventFactory.GetAdventureEvent(adventureEventType, pos);
        AudioSource.PlayClipAtPoint(FactoryManager.AssetFactory.LoadAudioClip("Audios/TriggerEvent"), pos);
        return true;
    }

    public void OnTrigger(IAdventureEventAction adventureEventAction)
    {
        adventureEventAction.Show();
        adventureEventAction.OnComplete(OnAdventureEventComplete);
    }

    public void OnAdventureEventComplete()
    {
        FactoryManager.EventFactory.TriggerAdventureComplete();
    }
}
