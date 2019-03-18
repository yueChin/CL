
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour {
    private Button mPauseButton;
    private AudioSource mPauseBtnAudioSource;
    // Use this for initialization
    void Start () {
        mPauseButton = UnityTool.FindOneOfAllChild(this.transform, "PauseButton").GetComponent<Button>();
        mPauseBtnAudioSource = mPauseButton.GetComponent<AudioSource>();
        mPauseButton.GetComponent<Button>().onClick.AddListener(OnPauseGame);
    }

    private void OnPauseGame()
    {
        GameControl.GetGameControl.GameFrezze();
        mPauseBtnAudioSource.Play();
        UIManager.GetUIManager.PushPanel(UIPanelType.Pause);
    }
}
