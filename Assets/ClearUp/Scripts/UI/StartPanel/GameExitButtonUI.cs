
using UnityEngine;
using UnityEngine.UI;

public class GameExitButtonUI : MonoBehaviour {
    private Button mExitButton;
    // Use this for initialization
    void Start()
    {
        mExitButton = this.GetComponent<Button>();
        mExitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnExitButtonClick()
    {
        GameControl.GetGameControl.GameExit();
    }
}
