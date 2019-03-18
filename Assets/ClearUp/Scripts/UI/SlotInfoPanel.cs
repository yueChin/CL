using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SlotInfoPanel : BasePanel {
    private ContentSizeFitter mContentSizeFitter;
    private RectTransform mRectTransform;
    private Vector2 mOriginRect;
    private Vector2 mHideRect;
    private Text mDescription;
    private float mDeltaTime;
    private Button mButton;
    private void Awake()
    {
        mDescription = this.GetComponent<Text>();
        mRectTransform = this.GetComponent<RectTransform>();
        mContentSizeFitter = this.GetComponent<ContentSizeFitter>();
        mButton = this.GetComponent<Button>();
        mButton.onClick.AddListener(HideInfo);
        mOriginRect = mRectTransform.sizeDelta;
        mHideRect = mOriginRect;
        mHideRect.y = 0;      
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (this.gameObject.activeSelf &&UnityEngine.Time.time - mDeltaTime > 10)
        {
            HideInfo();
        }
        if (mRectTransform.rect.width / 2 + Mathf.Abs(mRectTransform.anchoredPosition.x) > Screen.width / 2)
        {
            Vector2 vector2 = mRectTransform.anchoredPosition;
            if (vector2.x > 0) { vector2.x = Screen.width / 2 - mRectTransform.rect.width / 2 - 20; }
            else { vector2.x = -Screen.width / 2 + mRectTransform.rect.width / 2 + 20; }
            mRectTransform.anchoredPosition = vector2;
        }
    }

    public void ShowItemInfo(Vector3 pos,string desc)
    {
        mDeltaTime = UnityEngine.Time.time;
        ShowAnimation(pos, desc);
    }

    public void ShowSkillInfo(Vector3 pos, string desc)
    {
        mDeltaTime = UnityEngine.Time.time;
        ShowAnimation(pos, desc);
    }

    public void ShowGearInfo(Vector3 pos, string desc)
    {
        mDeltaTime = UnityEngine.Time.time;
        ShowAnimation(pos, desc);        
    }

    public void HideInfo()
    {
        HideAnimation();
    }

    #region 动画相关
    private void ShowAnimation(Vector3 pos, string desc)
    {
        this.transform.SetSiblingIndex(this.transform.parent.childCount - 1);
        this.mRectTransform.position = pos + new Vector3(80, 160, 0);
        mDescription.text = string.Empty;
        mRectTransform.sizeDelta = mHideRect;
        mRectTransform.DOSizeDelta(mOriginRect, 0.5f).OnComplete(()=> { ShowDesc(desc); });
    }

    private void HideAnimation()
    {
        mContentSizeFitter.enabled = false;
        mRectTransform.DOSizeDelta(mHideRect, 0.5f).OnComplete(() => {
            mDescription.gameObject.SetActive(false);
            this.gameObject.SetActive(false); });
    }

    private void ShowDesc(string desc)
    {
        mDescription.gameObject.SetActive(true);
        mContentSizeFitter.enabled = true;
        mDescription.text = desc;       
        //if (mRectTransform.rect.width / 2+ Mathf.Abs(mRectTransform.anchoredPosition.x) > Screen.width / 2)
        //{
        //    Vector2 vector2 = mRectTransform.anchoredPosition;
        //    if (vector2.x > 0) { vector2.x = Screen.width / 2 - mRectTransform.rect.width / 2 - 20; }
        //    else { vector2.x = - Screen.width / 2 + mRectTransform.rect.width / 2 + 20; }
        //    mRectTransform.anchoredPosition = vector2;            
        //}            
    }
    #endregion
}
