using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteelUI : MonoBehaviour {

    private AudioSource mAudioSource;
    private Text mText;
	// Use this for initialization
	void Start () {
        mAudioSource = this.gameObject.GetComponent<AudioSource>();
        mText = this.gameObject.GetComponentInChildren<Text>();
        mText.text = "0";
        EventCenter.AddListener(EventType.StartGaming,GameStart);
        EventCenter.AddListener<int>(EventType.ChangeSteel, SteelChange);
    }

    private void SteelChange(int steel)
    {
        mText.text = steel.ToString();
        mAudioSource.Play();
    }

    private void GameStart()
    {
        mText.text = "0";
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.StartGaming, GameStart);
        EventCenter.RemoveListener<int>(EventType.ChangeSteel, SteelChange);
    }
}
