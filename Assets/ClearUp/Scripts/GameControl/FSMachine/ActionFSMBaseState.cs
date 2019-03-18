using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public abstract class ActionFSMBaseState
{
    public FSMStateID StateID { get; set; }    //状态ID
    public ActionFSMSystem FSMSystem { get; set; }   //该对象属于在哪个状态机
    protected float _Forcetimes = 10;
    protected float _HoldTime = 0;
    protected Vector3 _MousePosOnScreen;
    private Dictionary<FSMTransition, FSMStateID> mFSMStateIdDic;//注意静态

    public ActionFSMBaseState(ActionFSMSystem fsmSystem, FSMStateID stateID)
    {
        this.FSMSystem = fsmSystem;
        this.StateID = stateID;
    }

    public void AddTransition(FSMTransition transition, FSMStateID stateID)
    {
        //Debug.Log(this.StateID + "增加:" + transition + "=>" + stateID);
        if (mFSMStateIdDic == null)
        {
            mFSMStateIdDic = new Dictionary<FSMTransition, FSMStateID>();
        }
        if (mFSMStateIdDic.ContainsKey(transition))
        {
            UnityEngine.Debug.Log(this.StateID + "本状态已经包含了该转换条件:" + transition+ "=>" + stateID);
            return;
        }
        mFSMStateIdDic.Add(transition, stateID);
    }

    public void DeleteTransition(FSMTransition transition)
    {
        if (mFSMStateIdDic == null)
        {
            UnityEngine.Debug.Log("容器未创建，无法删除");
            return;
        }
        if (!mFSMStateIdDic.ContainsKey(transition))
        {
            UnityEngine.Debug.Log(transition + "容器中没有该转换条件");
            return;
        }
        mFSMStateIdDic.Remove(transition);
    }

    public FSMStateID GetStateIdByTransition(FSMTransition transition)
    {
        if (mFSMStateIdDic == null)
        {
            UnityEngine.Debug.Log("容器未创建，无法获取状态");
            return FSMStateID.NullFSMStateID;
        }
        if (!mFSMStateIdDic.ContainsKey(transition))
        {
            UnityEngine.Debug.Log(transition + "容器内没有该转换条件，无法获取状态");
            return FSMStateID.NullFSMStateID;
        }
        return mFSMStateIdDic.FirstOrDefault(q => q.Key == transition).Value;
    }

    public virtual void StateStart() { _HoldTime = 0; _MousePosOnScreen = Vector3.zero; }
    public virtual void StateUpdate() { }
    public virtual void StateEnd() { }
    //转化状态条件
    public abstract void TransitionReason();
    /// <summary>
    /// 判断是否点击的是UI，有效应对安卓没有反应的情况，true为UI
    /// </summary>
    /// <returns></returns>
    protected bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}