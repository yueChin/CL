using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButtonUI : MonoBehaviour {
    private AudioSource mAudioSource;
    private Button mStartButton;
    // Use this for initialization
    void Start () {
        mStartButton = this.GetComponent<Button>();
        mAudioSource = this.GetComponent<AudioSource>();
        mStartButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        mAudioSource.Play();
        GameControl.GetGameControl.ChangeState();
    }
}
