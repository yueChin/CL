using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventBuilder:IEventBuilder {
    protected string _BuildTag;
    protected string _CubeTag;
    protected int _CubeEffectNumber;
    protected Vector3 _Pos;
    public BaseEventBuilder(string buildtag,string cubetag,Vector3 vector3)
    {
        _BuildTag = buildtag;
        _CubeTag = cubetag;
        _Pos = vector3;
        _CubeEffectNumber = 0;
    }
    public virtual void SetCubetag(string cubetag) { _CubeTag = cubetag; }
    protected abstract AdventureType RandomEvent();
    protected abstract void JudgeCube();
    protected virtual AdventureEventType RandomAdventureEvent() { return AdventureEventType.IncreaseTime; }
    protected virtual EnemyType RandomEnemyType() { return EnemyType.Thief; }
    protected virtual OccupationType RandomOccupationType() { return OccupationType.Traveller; }
    public abstract void RandomBuildingEvent();
    protected System.Action<Vector3> _Action;
    public void OnComplete(System.Action<Vector3> action = null)
    {
        _Action = action;
    }
}
