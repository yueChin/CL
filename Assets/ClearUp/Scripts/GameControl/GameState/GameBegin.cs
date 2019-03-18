using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBegin:GameBaseState {

    public GameBegin(GameStateControl controller) : base("BeginScene", controller) { }

    
    public override void StateStart()
    {
        UIManager.GetUIManager.PushPanel(UIPanelType.Start);
        GameControl.GetGameControl.DoBeforePlay();
        //进入游戏画面/场景,加载资源啥的
    }

    public override void StateEnd()
    {
        UIManager.GetUIManager.PushPanel(UIPanelType.Play);
    }

    public override void ChangeState()
    {
        //Debug.Log("场景转换");
        _Controller.SetState(new GameGoing(_Controller), false);
    }
}
