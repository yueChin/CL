using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleTalkUI : MonoBehaviour {
    private Text mText;
    private CanvasGroup mCanvasGroup;
	// Use this for initialization
	void Start () {
        mText = this.gameObject.GetComponent<Text>();
        mCanvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        mCanvasGroup.alpha = 0;
        EventCenter.AddListener<string>(EventType.NPCSay, OnReleaseSkill);
	}

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventType.NPCSay, OnReleaseSkill);
    }

    private void OnReleaseSkill(string context)
    {
        mCanvasGroup.alpha = 1;
        mText.text = context;
        mCanvasGroup.DOFade(0,0.3f).SetDelay(1f);
    }
}
