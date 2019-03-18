using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseEventBuilder :BaseEventBuilder
{
    //private BaseCreature mBaseCreature;

    public HouseEventBuilder(string buildtag, string cubetag,Vector3 vector3) : base(buildtag, cubetag, vector3) { }

    public override void RandomBuildingEvent()
    {
        AdventureType eventType = RandomEvent();
        //Debug.Log(eventType);
        if (eventType == AdventureType.HaveSomeGoodMan)
        {
            //直接从工厂拿 然后show？
            AdventureEventManager.GetAdventureEventManager.TriggerGoodCreature(RandomOccupationType(), _Pos);
        }
        else if (eventType == AdventureType.HaveSomeEnemy)
        {
            AdventureEventManager.GetAdventureEventManager.TriggerEnemyCreature(RandomEnemyType(), _Pos);
        }
        else
        {
            FactoryManager.EventFactory.TriggerAdventureComplete();
        }
    }

    protected override AdventureType RandomEvent()
    {
        JudgeCube();
        int[] proportion = { 500 + _CubeEffectNumber * 2, 750 + _CubeEffectNumber, 1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (AdventureType)i;
    }

    protected override void JudgeCube()
    {
        if (_CubeTag == "RedCube")
        {
            _CubeEffectNumber = 100;
        }
        else if (_CubeTag == "YellowCube")
        {
            _CubeEffectNumber = -50;
        }
    }

    protected override OccupationType RandomOccupationType()
    {
        int[] proportion = { 300, 450, 600, 750,1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (OccupationType)i;
    }

    protected override EnemyType RandomEnemyType()
    {
        int[] proportion = {500,1000};
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (EnemyType)i;
    }
    
}
