using System;
using UnityEngine;

public abstract class AdventureEvent :IAdventureEventAction
{
    private System.Action mAction;
    public virtual void OnComplete(Action action)
    {
        mAction = action;
    }
    public virtual void Hide()
    {
        //Debug.Log(mAction);
        if(mAction!= null)
            mAction();
    }
    public virtual void Show() { }
}
