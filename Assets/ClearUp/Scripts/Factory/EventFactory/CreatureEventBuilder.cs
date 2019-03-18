using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEventBuilder :BaseEventBuilder
{
    public CreatureEventBuilder(string buildtag, string cubetag,Vector3 vector3) : base(buildtag, cubetag, vector3) { }

    public override void RandomBuildingEvent()
    {
        AdventureType eventType = RandomEvent();
        //Debug.Log(eventType);
        if (eventType == AdventureType.HaveSomeGoodMan)
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
        int[] proportion = { 0 + _CubeEffectNumber, 1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (AdventureType)i;
    }

    protected override void JudgeCube()
    {
        if (_CubeTag == "RedCube")
        {
            _CubeEffectNumber = 700;
        }
        else if (_CubeTag == "BlueCube")
        {
            _CubeEffectNumber = 500;
        }
        else if (_CubeTag == "GreenCube")
        {
            _CubeEffectNumber = 300;
        }
    }

    protected override OccupationType RandomOccupationType()
    {
        int[] proportion = { 600, 0, 0, 700, 900, 950, 1000 };
        int i = TypeRandom.ControlRandomProportion(proportion);
        return (OccupationType)i;
    }
}
