
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusicButtonUI : MonoBehaviour {
    private Button mRandomMusicButton;
    // Use this for initialization
    void Start () {
        mRandomMusicButton = this.GetComponent<Button>();
        mRandomMusicButton.onClick.AddListener(OnMusicChangeClick);
    }

    private void OnMusicChangeClick()
    {
        GameControl.GetGameControl.ChangeMusic();
    }
}
