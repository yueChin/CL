using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerSocreTimeUI : MonoBehaviour {
    private Text mPreTimeScore;
    // Use this for initialization
    void Start () {
        mPreTimeScore = this.transform.Find("Field").GetComponent<Text>();
        EventCenter.AddListener<float>(EventType.PreScoreTime,ShowPreScoreTime);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<float>(EventType.PreScoreTime, ShowPreScoreTime);
    }
    /// <summary>
    /// 显示当前每分时间
    /// </summary>
    /// <param name="preScoreTime"></param>
    public void ShowPreScoreTime(float preScoreTime)
    {
        mPreTimeScore.text = preScoreTime.ToString();
    }
}
