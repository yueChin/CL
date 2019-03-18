using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart :GameBaseState{

    public GameStart(GameStateControl controller) : base("StartScene", controller) { }
    private bool mIsCanChange;

    public override void StateStart()
    {
        //GameControl.GetGameControl.Init();
        GameControl.GetGameControl.Init();
        UIManager.GetUIManager.Init();
        mIsCanChange = true;
    }

    public override void StateUpdate()
    {
        ChangeState();
    }

    public override void ChangeState()
    {
        if (mIsCanChange)
        {
            _Controller.SetState(new GameBegin(_Controller), false);
        }
    }
}
