using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoing :GameBaseState {
    public GameGoing(GameStateControl controller) : base("GoingScene", controller) { }


    public override void StateStart()
    {       
        GameControl.GetGameControl.GameStart();
    }

    public override void StateEnd()
    {
        GameControl.GetGameControl.DoAfterPlay();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        UIManager.GetUIManager.Update();
        GameControl.GetGameControl.Update();
    }

    public override void ChangeState()
    {
        Debug.Log("游戏结束");
        UIManager.GetUIManager.PushPanel(UIPanelType.End);
        _Controller.SetState(new GameEnd(_Controller),false);
    }
}
