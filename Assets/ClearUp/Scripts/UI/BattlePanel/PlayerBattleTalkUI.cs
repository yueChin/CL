
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleTalkUI : MonoBehaviour
{
    private Text mText;
    private CanvasGroup mCanvasGroup;
    // Use this for initialization
    void Start()
    {
        mText = this.gameObject.GetComponent<Text>();
        mCanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        mCanvasGroup.alpha = 0;
        EventCenter.AddListener<string>(EventType.PlayerSay,OnReleaseSkill);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.PlayerSay, OnReleaseSkill);
    }

    private void OnReleaseSkill(string context)
    {
        mCanvasGroup.alpha = 1;
        mText.text = context;
        mCanvasGroup.DOFade(0, 0.3f).SetDelay(0.5f);
    }
}