using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTapTapUI : MonoBehaviour {
    private AudioSource mAudioSource;
    private Button mTapButton;
    private Image mAnimation;
    [SerializeField]
    private List<Sprite> sprites;
    private int mAnimationAmount { get { return sprites.Count; } }

    private Coroutine c;
    // Use this for initialization
    void Awake () {
        mAudioSource = this.GetComponent<AudioSource>();
        mTapButton = this.GetComponentInChildren<Button>();
        mAnimation = this.transform.Find("Animation").GetComponent<Image>();
        mTapButton.onClick.AddListener(Tap);       
	}

    void OnEnable()
    {
        c = StartCoroutine(Animation());
    }

    void OnDisable()
    {
        StopCoroutine(c);
    }

    public void Tap()
    {
        mAudioSource.Play();
        GameControl.GetGameControl.BattlePlayerTap();
    }

    private IEnumerator Animation()
    {
        int index = 0;//可以用来控制起始播放的动画帧索引
        while (this.gameObject.activeSelf)
        {
            if (index < mAnimationAmount)
            {
                mAnimation.sprite = sprites[index];
                index++;
            }
            else { index = 0; }
            yield return new WaitForSeconds(0.08f);//等待间隔  控制动画播放速度          
        }
    }
}
