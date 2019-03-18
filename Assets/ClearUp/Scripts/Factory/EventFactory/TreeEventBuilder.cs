using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEventBuilder : BaseEventBuilder, IEventBuilder
{
    public TreeEventBuilder(string buildtag, string cubetag,Vector3 vector3) : base(buildtag, cubetag, vector3) { }

    public override void RandomBuildingEvent()
    {
        AdventureType eventType = RandomEvent();
        //Debug.Log(eventType);
        if (eventType == AdventureType.HaveSomeEnemy)
        {
            if (_CubeTag == "YellowCube" || _CubeTag == "Green")
            {
                AdventureEventManager.GetAdventureEventManager.TriggerEnemyCreature(EnemyType.Brigand, _Pos);
            }
            else
            {
                AdventureEventManager.GetAdventureEventManager.TriggerEnemyCreature(EnemyType.Bandit, _Pos);
            }
        }
        else if (eventType == AdventureType.HaveSomethimg)
        {
            AdventureEventManager.GetAdventureEventManager.TriggerAdventureEvent(RandomAdventureEvent(), _Pos);
        }
        else if (eventType == AdventureType.HaveSomeGoodMan)
        {
            AdventureEventManager.GetAdventureEventManager.TriggerGoodCreature(RandomOccupationType(), _Pos);
        }
        else
        {
            FactoryManager.EventFactory.TriggerAdventureComplete();
        }
    }

    protected override AdventureType RandomEvent()
    {
        JudgeCube();
        int[] proportion = { 700 + _CubeEffectNumber * 2, 750 + _CubeEffectNumber * 2, 850 + _CubeEffectNumber, 1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (AdventureType)i;
    }

    protected override void JudgeCube()
    {
        if (_CubeTag == "RedCube")
        {
            _CubeEffectNumber = 75;
        }
        if (_CubeTag == "BlueCube")
        {
            _CubeEffectNumber = 25;
        }
        if (_CubeTag == "GreenCube")
        {
            _CubeEffectNumber = -50;
        }
    }

    protected override AdventureEventType RandomAdventureEvent()
    {
        int[] proportion = { 150, 300, 400, 0, 550, 0, 0, 600, 650, 750, 800, 900, 950, 1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (AdventureEventType)i;
    }

    protected override OccupationType RandomOccupationType()
    {
        int[] proportion = { 0, 0, 0, 0, 0, 500,1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (OccupationType)i;
    }
}

