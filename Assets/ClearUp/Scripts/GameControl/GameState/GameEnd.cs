using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd  :GameBaseState{

    public GameEnd(GameStateControl controller) : base("EndScene", controller) { }


    public override void StateStart()
    {
        //有成就系统的话，这里要记录数值
    }

    public override void StateEnd()
    {
        //Debug.Log("标题界面");
        //UIManager.GetUIManager.PushPanel(UIPanelType.Start);
    }

    public override void ChangeState()
    {
        _Controller.SetState(new GameBegin(_Controller),false);
    }
}
