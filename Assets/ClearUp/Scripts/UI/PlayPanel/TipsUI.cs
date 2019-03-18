using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TipsUI : MonoBehaviour {
    private CanvasGroup mCanvasGroup;
    private Button mButton;
    private float mCountDown;
    private bool mIsTiming;
    private float mTimer;
    private Tween mTween;
    // Use this for initialization
    void Awake()
    {
        mCanvasGroup = this.GetComponentInChildren<CanvasGroup>();
        mButton = this.GetComponent<Button>();       
    }

    private void OnEnable()
    {
        mCountDown = 0;
        mIsTiming = false;
        Show();
    }

    private void Start()
    {
        mButton.onClick.AddListener(Click);
    }

    private void Update()
    {
        EixtDetection();
        Hide();
    }

    private void Click()
    {
        if (mCountDown == 0) //当倒计时时间等于0的时候 
        {
            mCountDown = Time.time; //把游戏开始时间，赋值给 CountDown
            mIsTiming = true; //开始计时  
        }
        else
        {
            mCountDown = 0; //倒计时时间归零 
            mIsTiming = false; //关闭倒计时 
            Show();           
        }
}

    /// <summary> 
    /// 双击检测超时 
    /// </summary> 
    private void EixtDetection()
    {
        if (mIsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - mCountDown) > 2.0) //如果 两次点击时间间隔大于2秒 
            {
                mCountDown = 0; //倒计时时间归零 
                mIsTiming = false; //关闭倒计时 
            }
        }
    }

    private void Show()
    {
        mTimer = Time.time;
        if (!mCanvasGroup.gameObject.activeSelf)
        {
            mCanvasGroup.gameObject.SetActive(true);
        }
        else
        {
            if (mTween != null)
                mTween.Pause();
            mTween = null;
            mCanvasGroup.alpha = 1;
        }
    }

    private void Hide()
    {
        if (Time.time - mTimer > 20 && mTween == null)
        {
            mTween = mCanvasGroup.DOFade(0, 5f).OnComplete(() => { mCanvasGroup.gameObject.SetActive(false); });
        }
    }

    /// <summary>
    /// 判断是否点击的是UI，有效应对安卓没有反应的情况，true为UI
    /// </summary>
    /// <returns></returns>
    protected bool IsPointerOverTips()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        for (int i=0;i< results.Count;i++)
        {
            if (results[i].gameObject.CompareTag("Tips")) { return true; }
        }
        return false;
    }
}
