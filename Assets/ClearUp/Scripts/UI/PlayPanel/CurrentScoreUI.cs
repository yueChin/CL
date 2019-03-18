
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScoreUI : MonoBehaviour {
    private Text mScore;
    private List<GameObject> mLChangeScoreGO;
    private GameObject mChangeScoreGO;
    // Use this for initialization
    void Start () {
        mScore = UnityTool.FindOneOfAllChild(this.transform, "Field").GetComponent<Text>();
        mChangeScoreGO = UnityTool.FindOneOfAllChild(this.transform, "ChangingScore").gameObject; 
        mChangeScoreGO.gameObject.SetActive(false);
        EventCenter.AddListener<float,float,float,Color>(EventType.SetScore,ShowScore);
    }

    private void OnDestroy()
    {       
        EventCenter.RemoveListener<float, float, float, Color>(EventType.SetScore, ShowScore);
    }
    /// <summary>
    /// 显示当前分数动画
    /// </summary>
    /// <param name="score"></param>
    public void ShowScore(float score, float maxScore, float cubeScore, Color cubeColor)
    {
        ShowScoreAnimation(cubeScore, cubeColor);
        mScore.text = string.Format("{0}/{1}", score.ToString(), maxScore.ToString());
        //Debug.Log(string.Format("{0}/{1}", score.ToString(), maxScore.ToString()));
    }

    /// <summary>
    /// 加分动画
    /// </summary>
    /// <param name="changeScore"></param>
    public void ShowScoreAnimation(float changeScore, Color cubeColor)
    {
        GameObject scoreGameObject = null;
        if (mLChangeScoreGO == null)
        {
            mLChangeScoreGO = new List<GameObject>();
        }
        if (mLChangeScoreGO.Count > 0)
        {
            foreach (GameObject go in mLChangeScoreGO) ///加分对象池
            {
                if (!go.activeSelf) { scoreGameObject = go; }
            }
        }
        if (scoreGameObject == null)
        {
            scoreGameObject = GameObject.Instantiate<GameObject>(mChangeScoreGO);
            mLChangeScoreGO.Add(scoreGameObject);
        }
        scoreGameObject.SetActive(true);
        scoreGameObject.GetComponent<AudioSource>().Play();
        scoreGameObject.transform.SetParent(mChangeScoreGO.transform.parent);
        //Debug.Log(scoreGameObject + "加分" + mChangeScoreGO);
        Text text = scoreGameObject.GetComponent<Text>();
        text.text = changeScore.ToString();
        text.color = Color.black;
        text.DOColor(cubeColor, 0.8f);
        scoreGameObject.transform.localPosition = mChangeScoreGO.gameObject.transform.localPosition;
        scoreGameObject.GetComponent<RectTransform>()
            .DOAnchorPos(mChangeScoreGO.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 10), 0.8f).SetEase(Ease.InOutSine)
            .OnComplete(() => { scoreGameObject.SetActive(false); });
    }
}
