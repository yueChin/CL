using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartOneUI : MonoBehaviour {

    private AudioSource mAudioSource;
    private Image mImage;
    [SerializeField]
    private List<Sprite> animationSprites = new List<Sprite>();
    private int mAnimationAmount { get { return animationSprites.Count; } }
    public delegate void DoHeartBrokenComplete();
    public event DoHeartBrokenComplete DoHeartBrokenCompleteEventHandle;
    // Use this for initialization
    void Awake () {
        mImage = this.GetComponent<Image>();
        mAudioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 心脏破碎
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayAnimationForwardIEnum()
    {
        this.gameObject.SetActive(true);
        int index = 0;//可以用来控制起始播放的动画帧索引
        mAudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/HeartBroke");
        mAudioSource.Play();
        while (index < mAnimationAmount)
        {
            mImage.sprite = animationSprites[index];
            index++;
            yield return new WaitForSeconds(0.15f);//等待间隔  控制动画播放速度
        }
        if (index >= mAnimationAmount)
        {
            DoHeartBrokenCompleteEventHandle();
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 心脏聚合
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayAnimationBackwardIEnum()
    {
        mAudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/HeartAdd");
        mAudioSource.Play();
        int index = 0;//可以用来控制起始播放的动画帧索引
        while (index < mAnimationAmount)
        {
            mImage.sprite = animationSprites[mAnimationAmount - 1 - index];
            index++;
            yield return new WaitForSeconds(0.15f);//等待间隔  控制动画播放速度           
        }
    }
}
