using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBaseState {

    private string mStateName;
    protected GameStateControl _Controller;
    public GameBaseState(string stateName,GameStateControl controller)
    {
        mStateName = stateName;
        _Controller = controller;
    }

    public string StateName
    {
        get { return mStateName; }
    }
    //每次进入到这个状态的时候调用
    public virtual void StateStart() { }
    public virtual void StateEnd() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
    public virtual void StateLateUpdate() { }
    public virtual void ChangeState() { }
}
