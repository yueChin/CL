
using UnityEngine;
using UnityEngine.UI;

public class RePlayButtonUI : MonoBehaviour {
    private AudioSource mRePlayBtnAudioSource;
    private Button mRePlayButton;
    // Use this for initialization
    void Start () {
        mRePlayButton = this.GetComponent<Button>();
        mRePlayBtnAudioSource = mRePlayButton.GetComponent<AudioSource>();
        mRePlayButton.onClick.AddListener(Replay);
    }

    private void Replay()
    {
        mRePlayBtnAudioSource.Play();
        //Debug.Log("重新开始");
        GameControl.GetGameControl.ChangeState();
    }

}
