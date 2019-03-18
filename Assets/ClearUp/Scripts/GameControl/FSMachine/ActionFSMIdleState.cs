using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFSMIdleState : ActionFSMBaseState
{

    public ActionFSMIdleState(ActionFSMSystem fsmSystem) : base(fsmSystem, FSMStateID.IdleFSMStateID) { }//用事件做？ 

    public override void StateUpdate()
    {
        //在这里判断输入所需要作的动作，能否做，做什么
        if (GameControl.GetGameControl.GameState != GameState.Playing) return;
        if (IsPointerOverUIObject()) return;
#if UNITY_ANDROID//输入部分应该只负责输入，比较头疼的是fsm和技能系统的交互
        if (Input.touchCount > 1)
        {
            _HoldTime = 0;
            return;
        }
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                _HoldTime += Time.deltaTime * _Forcetimes;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began) //按多久确定力的大小
            {
                _MousePosOnScreen = Input.touches[0].position;
                _HoldTime = 0;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended && _HoldTime > 0.1f) // 放开的时候加力,通知gamecontrol改变playstate？
            {
                //通知起跳
                Debug.Log(StateID + "waaaaaaa");
                GameControl.GetGameControl.Jump(_HoldTime, _MousePosOnScreen);
                _HoldTime = 0;
                TransitionReason();
            }
        }        
#endif

#if UNITY_STANDALONE_WIN
        //if (Input.touchCount > 1)
        //{
        //    _HoldTime = 0;
        //    return;
        //}
        //if (Input.touchCount == 1)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Stationary)
        //    {
        //        _HoldTime += Time.deltaTime * _Forcetimes;
        //    }
        //    if (Input.GetTouch(0).phase == TouchPhase.Began) //按多久确定力的大小
        //    {
        //        _MousePosOnScreen = Input.touches[0].position;
        //    }
        //    if (Input.GetTouch(0).phase == TouchPhase.Ended && _HoldTime > 0.1f) // 放开的时候加力,通知gamecontrol改变playstate？
        //    {
        //        //通知起跳
        //        GameControl.GetGameControl.Jump(_HoldTime, _MousePosOnScreen);
        //        _HoldTime = 0;
        //        TransitionReason();
        //    }
        //}
        if (Input.GetKey(KeyCode.Mouse0)) //按多久确定力的大小
        {
            _HoldTime += Time.deltaTime * _Forcetimes;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) //按下的时候确定方向
        {
            _HoldTime = 0;
            _MousePosOnScreen = Input.mousePosition;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && _HoldTime > 0) // 放开的时候加力
        {
            //通知起跳
            GameControl.GetGameControl.Jump(_HoldTime, _MousePosOnScreen);
            _HoldTime = 0;
            TransitionReason();
        }
#endif        
    }

    public override void TransitionReason()
    {
        if (this.FSMSystem == null)
        {
            Debug.Log("目标状态机为空");
            return;
        }
        FSMSystem.TransitionFSMState(FSMTransition.JumpClick);
    }
}
