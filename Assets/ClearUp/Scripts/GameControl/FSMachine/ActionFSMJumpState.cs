using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFSMJumpState : ActionFSMBaseState
{
    private GamePlayer mGamplayer;
    private float mPointTime;
    private int mDashLevel;
    private int mDoubleJumpLevel;
    private bool mIsClick;
    public ActionFSMJumpState(ActionFSMSystem fsmSystem,GamePlayer gamePlayer) : base(fsmSystem , FSMStateID.JumpingFSMStateID)
    {
        mGamplayer = gamePlayer;
        EventCenter.AddListener(EventType.HitCubeOrDead, HitReason);
    }

    ~ActionFSMJumpState()
    {
        EventCenter.RemoveListener(EventType.HitCubeOrDead, HitReason);
    }

    public override void StateStart()
    {
        base.StateStart();
        mPointTime = 0;
        mIsClick = false;
        mDashLevel = InventoryManager.GetInventoryManager.GetSkillInventory(mGamplayer).GetSkillLevel(SkillType.DashDown.ToString());
        mDoubleJumpLevel = InventoryManager.GetInventoryManager.GetSkillInventory(mGamplayer).GetSkillLevel(SkillType.DoubleJump.ToString());
    }

    public override void StateUpdate()
    {
        if (mDashLevel < 1 && mDoubleJumpLevel < 1) return;
        ///另一种方案是学习了技能以后给gameobject增加脚本，这些脚本判断单一手势情况，然后上抛，执行回传的函数或者上面不回应
        ///坏处就是如果有复杂的逻辑判断（这次是只有跳跃动作时有二次判断），判断当前小球动作状态又是个问题
        if (GameControl.GetGameControl.GameState != GameState.Playing||GameControl.GetGameControl.Player == null) return;
        if (IsPointerOverUIObject()) return;
        //开始计时，播放音效，并通知小球冻结刚体
#if UNITY_ANDROID//输入部分应该只负责输入，比较头疼的是fsm和技能系统的交互，准确的来说技能系统/管理并没有写
        if (mIsClick)
        {
            Debug.Log("跳跃点击");
            if (Time.time - mPointTime < mDashLevel + mDoubleJumpLevel)
            {
                if (Input.touchCount > 0)//计时期间有手指进入
                {
                    //关闭计时，关闭动画，退出黑屏
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        _MousePosOnScreen = Input.touches[0].position;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (Input.touches[0].position.y - _MousePosOnScreen.y < -15)//向下移动
                        {
                            GameControl.GetGameControl.Dash();//向下冲刺
                            TransitionReason();//切换到skillstate 防止误触
                        }
                    }
                    else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
                    {
                        _HoldTime += Time.deltaTime * _Forcetimes;
                    }
                    if ((Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) && _HoldTime > 0.1f) // 放开的时候加力,通知gamecontrol改变playstate？
                    {
                        GameControl.GetGameControl.Jump(_HoldTime, _MousePosOnScreen);    //通知起跳                    
                        TransitionReason();//切换到skillstate 防止误触
                    }                   
                }
                else
                {
                    //不停更新剩余时间显示
                    GameControl.GetGameControl.ShowRemainSkillTime(mDashLevel + mDoubleJumpLevel - (Time.time - mPointTime));
                }
            }
            else
            {
                //超时，解冻，关闭动画，退回黑屏
                GameControl.GetGameControl.Release();
            }
        }
        else
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                mIsClick = true;
                mPointTime = Time.time;
                GameControl.GetGameControl.Frezze();//冻结
                //播放计时音效，和动画，呼出黑屏？
                UIManager.GetUIManager.ShowSkillPausePanel();
            }
            //如果一直没有的点击，怎么知道他何时停止,事件？
        }      
#endif

#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("跳跃点击");
            if (Time.time - mPointTime < mDashLevel + mDoubleJumpLevel)
            {
                //关闭计时，关闭动画，退出黑屏
                if (Input.GetKeyDown(KeyCode.Mouse1))//向下移动
                {
                    _MousePosOnScreen = Input.touches[0].position;
                    GameControl.GetGameControl.Dash();//向下冲刺
                    TransitionReason();//切换到skillstate 防止误触
                }
                else if (Input.GetKey(KeyCode.Mouse0))
                {
                    _HoldTime += Time.deltaTime * _Forcetimes;
                }
                if (Input.GetKeyUp(KeyCode.Mouse0)) // 放开的时候加力,通知gamecontrol改变playstate？
                {
                    _MousePosOnScreen = Input.mousePosition;
                    GameControl.GetGameControl.Jump(_HoldTime, _MousePosOnScreen);    //通知起跳                    
                    TransitionReason();//切换到skillstate 防止误触
                }                
                //不停更新剩余时间显示
                GameControl.GetGameControl.ShowRemainSkillTime(mDashLevel + mDoubleJumpLevel - (Time.time - mPointTime));
            }
            else
            {
                //超时，解冻，关闭动画，退回黑屏
                GameControl.GetGameControl.Release();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                mIsClick = true;
                mPointTime = Time.time;
                GameControl.GetGameControl.Frezze();//冻结
                //播放计时音效，和动画，呼出黑屏？
                UIManager.GetUIManager.ShowSkillPausePanel();
            }
            //如果一直没有的点击，怎么知道他何时停止,事件？
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
        FSMSystem.TransitionFSMState(FSMTransition.SkillClick);
    }

    public void HitReason()
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
