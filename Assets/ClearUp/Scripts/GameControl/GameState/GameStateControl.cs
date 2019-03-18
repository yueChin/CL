using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateControl  {

    private GameBaseState mState;
    private AsyncOperation mAO;
    private bool mIsRunStart = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state">下一个状态是</param>
    /// <param name="isLoadScene">是否已执行过事前清理</param>
    public void SetState(GameBaseState state, bool isLoadScene = false)
    {
        //Debug.Log("SetState");
        if (mState != null)
        {
            mState.StateEnd();//让上一个场景状态做一下清理工作
        }
        mState = state;
        GameControl.GetGameControl.GameBaseState = mState;
        if (isLoadScene)
        {
            //mAO = SceneManager.LoadSceneAsync(mState.SceneName);每次状态切换需要做的事           
            mIsRunStart = false;
        }
        else
        {
            mState.StateStart();
            mIsRunStart = true;
        }

    }

    public void StateFixedUpdate()
    {
        if (mAO != null && mAO.isDone == false) return;
        if (mIsRunStart == false && mAO != null && mAO.isDone == true) return;
        if (mState != null)
        {
            mState.StateFixedUpdate();
        }
    }

    public void StateUpdate()
    {
        if (mAO != null && mAO.isDone == false) return;
        if (mIsRunStart == false && mAO != null && mAO.isDone == true)
        {
            mState.StateStart();
            mIsRunStart = true;
        }
        if (mState != null)
        {
            mState.StateUpdate();
        }
    }

    public void StateLateUpdate()
    {
        if (mAO != null && mAO.isDone == false) return;
        if (mIsRunStart == false && mAO != null && mAO.isDone == true) return;
        if (mState != null)
        {
            mState.StateLateUpdate();
        }
    }
}
