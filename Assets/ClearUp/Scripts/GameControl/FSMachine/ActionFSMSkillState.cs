using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFSMSkillState : ActionFSMBaseState
{

    public ActionFSMSkillState(ActionFSMSystem fsmSystem) : base(fsmSystem, FSMStateID.ReleaseSkillFsmStateID)
    {
        EventCenter.AddListener(EventType.HitCubeOrDead, TransitionReason);
    }//用事件做？ 

    ~ActionFSMSkillState()
    {
        EventCenter.RemoveListener(EventType.HitCubeOrDead, TransitionReason);
    }

    public override void TransitionReason()
    {
        if (this.FSMSystem == null)
        {
            Debug.Log("目标状态机为空");
            return;
        }
        if (this.FSMSystem.CurrentID.Equals(this.StateID))
            FSMSystem.TransitionFSMState(FSMTransition.HitMapCube);
    }
}
