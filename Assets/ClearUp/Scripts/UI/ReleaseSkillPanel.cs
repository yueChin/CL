
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseSkillPanel : BasePanel {
    private Text mTimer;
    private AudioSource mAudioSource;
    private CanvasGroup mCanvasGroup;
	// Use this for initialization
	private void Start () {
        mAudioSource = this.GetComponent<AudioSource>();
        mCanvasGroup = this.GetComponent<CanvasGroup>();
        mTimer = this.GetComponentInChildren<Text>();
        EventCenter.AddListener<float>(EventType.ShowRemainTime,ShowTimer);
	}

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float>(EventType.ShowRemainTime, ShowTimer);
    }

    public override void EnterPanel()
    {
        mAudioSource.Play();
        EnterAnimation();
    }

    public override void ExitPanel()
    {
        ExitAnimation();
    }

    private void ShowTimer(float timer)
    {
        if (timer <= 0) { ExitPanel(); return; }
        mTimer.text = timer.ToString();
    }

    #region 各类动画
    /// <summary>
    /// 进入面板的动画
    /// </summary>
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        mCanvasGroup.alpha = 0;
        mCanvasGroup.DOFade(1, 0.35f);
    }

    /// <summary>
    /// 离开面板的动画
    /// </summary>
    private void ExitAnimation()
    {
        mCanvasGroup.DOFade(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
    #endregion
}
