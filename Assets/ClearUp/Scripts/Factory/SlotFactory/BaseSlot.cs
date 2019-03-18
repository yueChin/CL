
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public abstract class BaseSlot :MonoBehaviour,ISlot,IPointerDownHandler, IPointerUpHandler
{
    protected Button _SlotButton;
    protected Image _Image;
    protected BaseGoods _BaseGood;
    protected AudioSource _AudioSource;
    private float mCountDown;
    private bool mIsTiming;
    private bool mIsDown;
    private float mLastDownTime;

    protected virtual void Awake()
    {
        _AudioSource = this.transform.GetComponent<AudioSource>();
        _SlotButton = this.transform.GetComponent<Button>();
        _Image = UITool.FindChild<Image>(this.transform,"Image");
        EventCenter.AddListener(EventType.OverGaming,Destroy);
    }

    protected virtual void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.OverGaming, Destroy);
    }

    protected virtual void OnEnable()
    {
        mCountDown = 0;
        mIsTiming = false;
        mIsDown = false;
        mLastDownTime = 0;        
    }

    protected virtual void Start()
    {               
        _SlotButton.onClick.AddListener(OnButtonClick);
    }

    protected virtual void Update()
    {
        if (mIsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - mCountDown) > 1.0) //如果 两次点击时间间隔大于1秒 
            {
                mCountDown = 0; //倒计时时间归零 
                mIsTiming = false; //关闭倒计时 
            }
        }
        if (mIsDown)
        {
            if (Time.time - mLastDownTime > 1.5)
            {
                OnLongClick();
            }
        }
    }

    public virtual void SetSlotGood(BaseGoods baseGoods)
    {
        _BaseGood = baseGoods;
        _Image.sprite = baseGoods.GetSprite;
    }

    [Obsolete("由于背包物品移除后整个slot都会activeFalse，所以暂时用不到")]
    public void ExchangeGood(BaseGoods baseGoods)
    {
        _BaseGood = baseGoods;
    }

    protected virtual void OnButtonClick()
    {
        if (mCountDown == 0) //当倒计时时间等于0的时候 
        {
            mCountDown = Time.time; //把游戏开始时间，赋值给 CountDown
            mIsTiming = true; //开始计时  
            OnSingleClick();
        }
        else
        {
            mCountDown = 0; //倒计时时间归零 
            mIsTiming = false; //关闭倒计时 
            OnDoubleClick();
        }       
    }

    // 当按钮被按下后系统自动调用此方法
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        mIsDown = true;
        mLastDownTime = Time.time;
    }

    // 当按钮抬起的时候自动调用此方法
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        mIsDown = false;
    }

    protected abstract void Destroy();
    protected virtual void OnSingleClick() { }
    protected virtual void OnDoubleClick() { }
    protected virtual void OnLongClick() { }
    public virtual void OnSlotMoveAnimation(PlayerPanel playerPanel)
    {
        //移动之前先跳出Layout
        this.transform.SetParent(UIManager.GetUIManager.GetCanvasTransform);
    }
}
