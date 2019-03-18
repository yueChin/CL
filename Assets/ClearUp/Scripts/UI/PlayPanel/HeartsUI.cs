using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeartsUI : MonoBehaviour {
    private Image[] mHeartImage = new Image[2];
    private HeartOneUI mHeartOneUI;
    private HeartTwoUI mHeartTwoUI;
    private int mRemainHearts;

    void Awake ()
    {
        mHeartImage[0] = this.transform.Find("OneHeart").GetComponent<Image>();
        mHeartImage[1] = this.transform.Find("TwoHeart").GetComponent<Image>();
        mHeartOneUI = this.GetComponentInChildren<HeartOneUI>();
        mHeartTwoUI = this.GetComponentInChildren<HeartTwoUI>();
        EventCenter.AddListener(EventType.AddHeart,AddHeart);
        EventCenter.AddListener<int>(EventType.LoseHeart,LoseHeart);
        mHeartOneUI.DoHeartBrokenCompleteEventHandle += new HeartOneUI.DoHeartBrokenComplete(HeartOneBrokenComplete);
        mHeartTwoUI.DoHeartBrokenCompleteEventHandle += new HeartTwoUI.DoHeartBrokenComplete(HeartTwoBrokenComplete);
        //添加事件
        for (int i = 0; i < mHeartImage.Length; i++)
        {
            mHeartImage[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(Rnumerator());
    }

    private IEnumerator Rnumerator()
    {
        AddHeart();
        yield return new WaitForSeconds(1f);//等待间隔  控制动画播放速度  
        //AddHeart();
        //LoseHeart(3);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.AddHeart, AddHeart);
        EventCenter.RemoveListener<int>(EventType.LoseHeart, LoseHeart);
        mHeartOneUI.DoHeartBrokenCompleteEventHandle -= new HeartOneUI.DoHeartBrokenComplete(HeartOneBrokenComplete);
        mHeartTwoUI.DoHeartBrokenCompleteEventHandle -= new HeartTwoUI.DoHeartBrokenComplete(HeartTwoBrokenComplete);
    }

    /// <summary>
    /// 增加生命，这里的生命是备用生命，也就说说两个都是空格的话 也还可以游玩一次
    /// </summary>
    private void AddHeart()
    {
        for (int i = 0; i < mHeartImage.Length; i++)
        {
            if (!mHeartImage[i].gameObject.activeSelf)
            {
                mHeartImage[i].gameObject.SetActive(true);
                if (i == 0)
                {
                    StartCoroutine(mHeartOneUI.PlayAnimationBackwardIEnum());
                }
                else
                {
                    StartCoroutine(mHeartTwoUI.PlayAnimationBackwardIEnum());
                }
                return;
            }
        }
    }

    private void LoseHeart(int remainheart)
    {
        mRemainHearts = remainheart;
        StartCoroutine(mHeartOneUI.PlayAnimationForwardIEnum());
    }

    private void HeartOneBrokenComplete()
    {
        if (mRemainHearts > 1)
        {
            StartCoroutine(mHeartTwoUI.PlayAnimationForwardIEnum());
        }
    }

    private void HeartTwoBrokenComplete()
    {
        mHeartOneUI.gameObject.SetActive(true);
        StartCoroutine(mHeartOneUI.PlayAnimationBackwardIEnum());
        if (mRemainHearts > 2)
        {
            mHeartTwoUI.gameObject.SetActive(true);
            StartCoroutine(mHeartTwoUI.PlayAnimationBackwardIEnum());
        }
    }

    ///// <summary>
    ///// 丢失生命，把大心置非，再把小心（如果有的话）置非，一条命没有
    ///// </summary>
    ///// <param name="remainHeart"></param>
    //private void LoseTweenHeart(int remainHeart)
    //{
    //    if (mHeartImage[0].gameObject.activeSelf)
    //    {
    //        mHeartImage[0].transform.DOScale(0, 0.5f).OnComplete(() => { mHeartImage[0].gameObject.SetActive(false);
    //            if (mHeartImage[1].gameObject.activeSelf)
    //            {
    //                mHeartImage[1].transform.DOScale(0, 0.5f).OnComplete(() => 
    //                    {
    //                        mHeartImage[1].gameObject.SetActive(false);
    //                        mHeartImage[0].transform.DOScale(1, 0.5f).OnComplete(()=> 
    //                            {
    //                                if (remainHeart > 1)
    //                                {
    //                                    mHeartImage[1].gameObject.SetActive(true);
    //                                    mHeartImage[1].transform.DOScale(1, 0.5f);
    //                                }
    //                            });
    //                    });
    //            }
    //        });
    //    }       
    //}  
}
