using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboUI : MonoBehaviour {

    private RectTransform mComBoRect;
    [SerializeField]
    private Color[] mColors;
    private Text mComBoNumber;
    private AudioSource mComboAudioSource;
    // Use this for initialization
    void Start ()
    {
        mComBoRect = this.transform.GetComponent<RectTransform>();
        mComboAudioSource = mComBoRect.GetComponent<AudioSource>();
        mComBoNumber = mComBoRect.Find("Field").GetComponent<Text>();
        mComBoRect.gameObject.SetActive(false);
        EventCenter.AddListener<float,bool>(EventType.SetCombo,ComboAnimation);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float, bool>(EventType.SetCombo, ComboAnimation);
    }

    /// <summary>
    /// 连击数动画
    /// </summary>
    /// <param name="combo"></param>
    /// <param name="isCombo"></param>
    public void ComboAnimation(float combo, bool isCombo)
    {
        if (isCombo)
        {
            ShowCombo(combo);
        }
        else
        {
            HideCombo();
        }
    }

    /// <summary>
    /// 显示连击数
    /// </summary>
    /// <param name="currentcombo"> 当前的连击数</param>
    public void ShowCombo(float currentcombo)
    {
        //Debug.Log("连击数:" + currentcombo);
        mComBoNumber.color = mColors[(int)currentcombo / 10];
        mComBoNumber.text = currentcombo.ToString();
        if (!mComBoRect.gameObject.activeSelf) { mComBoRect.gameObject.SetActive(true); }
        mComboAudioSource.Play();
        mComBoRect.DOJumpAnchorPos(mComBoRect.anchoredPosition, 10, 1, 0.3f);
        mComBoRect.DOScale((1f + currentcombo / 20), 0.2f).OnComplete(() => { mComBoRect.DOScale(1, 0.2f); });
    }

    /// <summary>
    /// 隐藏连击数
    /// </summary>
    public void HideCombo()
    {
        if (mComBoRect.gameObject.activeSelf) { mComBoRect.gameObject.SetActive(false); }
    }
}
