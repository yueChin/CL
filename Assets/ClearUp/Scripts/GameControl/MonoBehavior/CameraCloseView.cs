using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraCloseView : MonoBehaviour {

    private Vector3 mBeginPos;
    private Vector3 mBeginDir;
    private Tween mTweenRota;
    private Tween mTweenPos;
    private bool mIsReturnComplete;
    private void OnEnable()
    {
        mIsReturnComplete = true;
    }

    private void Start()
    {
        //注册接近的方法和返回的方法
        EventCenter.AddListener<Vector3>(EventType.CloseView,CloseUpView);
        EventCenter.AddListener(EventType.ReturnView,ReturnView);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector3>(EventType.CloseView, CloseUpView);
        EventCenter.RemoveListener(EventType.ReturnView, ReturnView);
    }

    /// <summary>
    /// 特写镜头
    /// </summary>
    /// <param name="eventPos">事件人物的世界坐标</param>
    private void CloseUpView(Vector3 eventPos)
    {
        //Debug.Log("进入特写");
        if(mIsReturnComplete == true)
        {
            mBeginDir = this.transform.localRotation.eulerAngles;           
            mBeginPos = this.transform.position;
            //Debug.Log(mBeginDir + ""+ mBeginPos);
        }
        mIsReturnComplete = false;
        mTweenRota = this.transform.DOLookAt(eventPos,0.5f).SetEase(Ease.InSine);
        mTweenPos = this.transform.DOMove(this.transform.position + (eventPos - this.transform.position) * 0.6f, 0.5f).SetEase(Ease.InSine);        
    }

    /// <summary>
    /// 返回原来的位置和方向
    /// </summary>
    private void ReturnView()
    {
        //Debug.Log("返回特写");
        //Debug.Log(mBeginDir + "" + mBeginPos);
        if (mTweenRota != null)
            mTweenRota.Pause();
        mTweenRota = this.transform.DORotate(mBeginDir, 0.5f).SetEase(Ease.InSine);
        if (mTweenPos != null)
            mTweenPos.Pause();
        mTweenPos = this.transform.DOMove(mBeginPos, 0.5f).SetEase(Ease.InSine).OnComplete(()=> { mIsReturnComplete = true; });
    }
}
