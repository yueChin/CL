using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// 状态ID
/// </summary>
public enum FSMStateID
{
    NullFSMStateID,
    IdleFSMStateID,
    JumpingFSMStateID,
    ReleaseSkillFsmStateID,
}


/// <summary>
/// 状态转化条件
/// </summary>
public enum FSMTransition
{
    JumpClick,
    SkillClick,
    HitMapCube
}

public class ActionFSMSystem
{
    public FSMStateID CurrentID { get { return mCurrentState.StateID; } }
    private FSMStateID mCurrentStateID;
    private ActionFSMBaseState mCurrentState;
    private GameControl mGameControl;
    private Dictionary<FSMStateID, ActionFSMBaseState> mFSMStateDic = new Dictionary<FSMStateID, ActionFSMBaseState>();

    public ActionFSMSystem(GameControl gameControl)
    {
        mGameControl = gameControl;
    }

    public void AddFSMSate(ActionFSMBaseState state)
    {
        if (state == null)
        {
            UnityEngine.Debug.Log("角色状态为空，无法添加");
            return;
        }
        if (mCurrentState == null)
        {            
            //第一个添加的状态被作为系统首个运行的状态
            mCurrentStateID = state.StateID;           
            mCurrentState = state;
            //Debug.Log(mCurrentState.StateID);
            mCurrentState.StateStart();
        }
        if (mFSMStateDic.ContainsValue(state))
        {
            UnityEngine.Debug.Log("容器内存在该状态");
            return;
        }
        mFSMStateDic.Add(state.StateID, state);
    }

    public void DeleteFSMSate(ActionFSMBaseState state)
    {
        if (state == null)
        {
            UnityEngine.Debug.Log("角色状态为空，无法添加");
            return;
        }
        if (!mFSMStateDic.ContainsValue(state))
        {
            UnityEngine.Debug.Log("容器内不存在该状态");
            return;
        }
        mFSMStateDic.Remove(state.StateID);
    }

    //更新（执行）系统
    public void UpdateSystem()
    {
        if (mCurrentState != null)
        {
            mCurrentState.StateUpdate();
        }
    }

    //转换状态
    public void TransitionFSMState(FSMTransition transition)
    {
        //Debug.Log(transition);
        FSMStateID stateID = mCurrentState.GetStateIdByTransition(transition);
        if (stateID != FSMStateID.NullFSMStateID)
        {
            mCurrentStateID = stateID;
            mCurrentState.StateEnd();
            //换状态
            mCurrentState = mFSMStateDic.FirstOrDefault(q => q.Key == stateID).Value;
            mCurrentState.StateStart();
        }
    }
}