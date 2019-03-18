
using UnityEngine;
using UnityEngine.UI;

public class HighestScoreUI : MonoBehaviour {
    private Text mHighestScore;
    private void Awake()
    {
        mHighestScore = this.GetComponent<Text>();
    }

    private void OnEnable()
    {
        ShowRecord(GameControl.GetGameControl.Record);
    }

    /// <summary>
    /// 显示记录
    /// </summary>
    private void ShowRecord(float record)
    {
        //Debug.Log("记录分：" + record.ToString());
        mHighestScore.text = record.ToString();
    }
}
