using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour {

    private AudioSource mAudioSource;
    private Text mText;
    // Use this for initialization
    void Start()
    {
        mAudioSource = this.gameObject.GetComponent<AudioSource>();
        mText = this.gameObject.GetComponentInChildren<Text>();
        mText.text = "0";
        EventCenter.AddListener(EventType.StartGaming,GameStart);
        EventCenter.AddListener<int>(EventType.ChangeCoin, CoinChange);
    }

    /// <summary>
    /// 当金币改变时
    /// </summary>
    /// <param name="coins">当前金币数</param>
    private void CoinChange(int coins)
    {
        mText.text = coins.ToString();
        mAudioSource.Play();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.StartGaming, GameStart);
        EventCenter.RemoveListener<int>(EventType.ChangeCoin, CoinChange);
    }

    private void GameStart()
    {
        mText.text = "0";
    }
}
